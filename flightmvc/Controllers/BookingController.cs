using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace flightmvc.Controllers
{
    public class BookingController : Controller
    {
        public static Ace52024Context ctx;
        //Dependency Injection  in constructor
        public BookingController(Ace52024Context _ctx)
        {
            ctx=_ctx;
        }
        public static List<HetalBooking> bookings = new List<HetalBooking>();
        public async Task<ActionResult> ShowBookings()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            HttpResponseMessage Res = await client.GetAsync("http://localhost:5197/api/Booking");
            HetalFlight flightrec = new HetalFlight();
            if (Res.IsSuccessStatusCode)
            {
                var response = Res.Content.ReadAsStringAsync().Result;
                bookings = JsonConvert.DeserializeObject<List<HetalBooking>>(response);
                foreach(var item in bookings)
                {
                    HttpResponseMessage ResFlight = await client.GetAsync("http://localhost:5197/api/Flight/"+item.FlightId);
                    //Console.WriteLine(item.FlightId);
                    if(ResFlight.IsSuccessStatusCode)
                    {
                        var res = ResFlight.Content.ReadAsStringAsync().Result;
                        // Console.WriteLine(res);
                        flightrec = JsonConvert.DeserializeObject<HetalFlight>(res);
                        item.Source = flightrec.Source;
                        item.Destination = flightrec.Destination;
                        item.Arrival = flightrec.Arrival;
                        item.Departure = flightrec.Departure;
                    }
                }
            }
            return View(bookings);
        }
        [HttpGet]
        public ActionResult AddBooking()
        {
            var distinctsrc = ctx.HetalFlights.Select(x => x.Source).Distinct().ToList();
            ViewBag.src = new SelectList(distinctsrc,"Source");
            var distinctdest = ctx.HetalFlights.Select(x => x.Destination).Distinct().ToList();
            ViewBag.dest = new SelectList(distinctdest,"Destination");
            return View();
        }
        [HttpPost] //button click logic 
        public ActionResult AddBooking(HetalBooking booking)
        {
            var var1 = booking.Source;
            string src = ctx.HetalFlights.Where(x => x.Source==var1).Select(x => x.Source).First();
            var var2 = booking.Destination;
            string dest = ctx.HetalFlights.Where(x => x.Destination==var2).Select(x => x.Destination).First();
            
            TempData["Source"] = src;
            TempData["Destination"] = dest;
            return RedirectToAction("ShowRelBookings","Flight");
        }
        
        public ActionResult Test()
        {
            var allFlights = TempData["bookings"];
            return View(allFlights);
        }
        [HttpGet]
        public ActionResult SaveBooking(HetalFlight flight)
        {
            TempData["id"] = flight.FlightId;
            return View();
        }
        [HttpPost]
        public ActionResult SaveBooking(HetalBooking booking)
        {
            string currUrl = Request.GetDisplayUrl();
            string[] segments = currUrl.Split('/');
            booking.FlightId = Convert.ToInt32(segments.LastOrDefault());
            booking.CustomerId = 1;
            double cost = ctx.HetalFlights.Where(x => x.FlightId==booking.FlightId).Select(x => x.Rate).First();
            if(booking.Discount=="1")
            booking.TotalCost = (double)(booking.NoOfPassengers*cost);
            else
            booking.TotalCost = (double)(booking.NoOfPassengers*cost - 500);
            ctx.HetalBookings.Add(booking);
            ctx.SaveChanges();
            TempData["cost"] = booking.TotalCost.ToString();
            return RedirectToAction("Successful");         
        }
        [HttpGet]
        public ActionResult Successful()
        {
            ViewBag.cost = TempData["cost"];
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> EditBooking(int id)
        {
            HetalBooking booking = new HetalBooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5197/api/Booking/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<HetalBooking>(apiResponse);
                }
            }
            TempData["Bookingid"] = id;
            return View(booking);
        }
        [HttpPost]
        public async Task<ActionResult> EditBooking(HetalBooking booking)
        {
            booking.BookingId = Convert.ToInt32(TempData["Bookingid"]);
            using (var httpClient = new HttpClient())
            {
                int id = Convert.ToInt32(TempData["Bookingid"]);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5197/api/Booking/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    
                    booking = JsonConvert.DeserializeObject<HetalBooking>(apiResponse);
                }
            }
            return RedirectToAction("ShowBookings");
        }
        [HttpGet]
        public ActionResult BookingDetails(int id)
        {
            HetalBooking booking = ctx.HetalBookings.Where(x => x.BookingId == id).SingleOrDefault();
            TempData["Bookingid"] = id;
            return View(booking);
        }
        [HttpPost]
        public ActionResult BookingDetails(HetalBooking booking)
        {
            return View(booking);
        }
        [HttpGet]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            HetalBooking booking = new HetalBooking();
            TempData["Bookingid"] = id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5197/api/Booking/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<HetalBooking>(apiResponse);
                }
            }
            return View(booking);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteBooking(HetalBooking booking)
        {
            booking.BookingId = Convert.ToInt32(TempData["Bookingid"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5197/api/Flight/" + booking.BookingId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("ShowBookings");
        }
    }
}