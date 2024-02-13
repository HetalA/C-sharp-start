using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace flightmvc.Controllers
{
    public class FlightController : Controller
    {
        public static Ace52024Context ctx;
        //Dependency Injection  in constructor
        public FlightController(Ace52024Context _ctx)
        {
            ctx=_ctx;
        }
        public static List<HetalFlight> flights = new List<HetalFlight>();
        public async Task<ActionResult> ShowFlights(String sortOrder, String sortOrder2, String searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RateSortParm"] = String.IsNullOrEmpty(sortOrder2) ? "rate_asc" : "";
            ViewData["CurrentFilter"] = searchString;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5197/api/Flight");
            if (Res.IsSuccessStatusCode)
            {
                var response = Res.Content.ReadAsStringAsync().Result;
                flights = JsonConvert.DeserializeObject<List<HetalFlight>>(response);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    flights.Sort((x, y) => y.Airline.CompareTo(x.Airline));
                    break;
                case "rate_asc":
                    flights.Sort((x, y) => x.Rate.CompareTo(y.Rate));
                    break;
                default:
                    flights.Sort((x, y) => x.FlightId.CompareTo(y.FlightId));
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                flights = flights.Where(s => s.Airline.Contains(searchString)).ToList();
            }
            return View(flights);
        }
        public ActionResult ShowRelBookings()
        {
            string src = TempData["Source"] as string;
            string dest = TempData["Destination"] as string;
            
            ViewData["Source"] = src.GetType();
            ViewData["Destination"] = dest;
            Console.WriteLine("", src, dest);

            var allFlights = ctx.HetalFlights.Where(b => (b.Source == src && b.Destination == dest)).ToList();

            return View(allFlights);
        }
        [HttpPost]
        public ActionResult Book(HetalFlight flight)
        {
            TempData["flight"] = flight.FlightId;
            TempData["rate"] = flight.Rate.ToString();
            return RedirectToAction("SaveBooking","Booking");
        }
        public ActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFlight(HetalFlight flight)
        {
        
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(flight),Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync("http://localhost:5197/api/Flight", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    flight = JsonConvert.DeserializeObject<HetalFlight>(apiResponse);
                }
            }
            return RedirectToAction("ShowFlights");
        }
        
        [HttpGet]
        public async Task<ActionResult> EditFlight(int id)
        {
            HetalFlight objFlight = new HetalFlight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5197/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    objFlight = JsonConvert.DeserializeObject<HetalFlight>(apiResponse);
                }
            }
            Console.WriteLine("ID : " + id);
            TempData["fid"] = id;
            return View(objFlight);
        }
        [HttpPost]
        public async Task<ActionResult> EditFlight(HetalFlight flight)
        {
            HetalFlight objFlight = new HetalFlight();
            flight.FlightId = Convert.ToInt32(TempData["fid"]);
            using (var httpClient = new HttpClient())
            {
                int id = Convert.ToInt32(TempData["fid"]);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(flight), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5197/api/Flight/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    
                    objFlight = JsonConvert.DeserializeObject<HetalFlight>(apiResponse);
                }
            }
            return RedirectToAction("ShowFlights");
        }
        [HttpGet]
        public ActionResult FlightDetails(int id)
        {
            HetalFlight flight = ctx.HetalFlights.Where(x => x.FlightId == id).SingleOrDefault();
            TempData["flightid"] = id;
            return View(flight);
        }
        [HttpPost]
        public ActionResult FlightDetails(HetalFlight flight)
        {
            return View(flight);
        }
        [HttpGet]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            TempData["fid"] = id;
            HetalFlight objFlight = new HetalFlight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5197/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    objFlight = JsonConvert.DeserializeObject<HetalFlight>(apiResponse);
                }
            }
            return View(objFlight);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteFlight(HetalFlight flight)
        {
            int fid = Convert.ToInt32(TempData["fid"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5197/api/Flight/" + fid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("ShowFlights");
        }
    }
}