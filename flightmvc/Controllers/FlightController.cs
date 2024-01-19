using flightmvc.Models;
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
        public ActionResult ShowFlights()
        {
            List<HetalFlight> flights = [..ctx.HetalFlights.Where(h => true)];
            return View(flights);
        }
        
        [HttpGet]
        public ActionResult AddFlight()
        {
            return View();
        }
        [HttpPost] //button click logic 
        public ActionResult AddFlight(HetalFlight flight)
        {
            ctx.HetalFlights.Add(flight);
            ctx.SaveChanges();  
            return RedirectToAction("ShowFlights");
        }
        [HttpGet]
        public ActionResult EditFlight(int id)
        {
            HetalFlight flight = ctx.HetalFlights.Where(x => x.FlightId == id).SingleOrDefault();
            TempData["flightid"] = id;
            return View(flight);
        }
        [HttpPost]
        public ActionResult EditFlight(HetalFlight flight)
        {
            flight.FlightId = Convert.ToInt32(TempData["flightid"]);
            ctx.HetalFlights.Update(flight);
            ctx.SaveChanges();
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
        public ActionResult DeleteFlight(int id)
        {
            HetalFlight flight = ctx.HetalFlights.Where(x => x.FlightId == id).SingleOrDefault();
            TempData["flightid"] = id;
            return View(flight);
        }
        [HttpPost]
        public ActionResult DeleteFlight(HetalFlight flight)
        {
            flight.FlightId = Convert.ToInt32(TempData["flightid"]);
            ctx.HetalFlights.Remove(flight);
            ctx.SaveChanges();
            return RedirectToAction("ShowFlights");
        }
    }
}