using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

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
        public ActionResult ShowBookings()
        {
            List<HetalBooking> bookings = [..ctx.HetalBookings.Where(h => true)];
            var result = ctx.HetalBookings.Include(x => x.Flight);
            return View(result);
            //return View(bookings);
        }
        [HttpGet]
        public ActionResult AddBooking()
        {
            ViewBag.src = new SelectList(ctx.HetalFlights,"FlightId","Source");
            ViewBag.dest = new SelectList(ctx.HetalFlights,"FlightId","Destination");
            return View();
        }
        [HttpPost] //button click logic 
        public ActionResult AddBooking(HetalBooking booking)
        {
            var var1 = Convert.ToInt32(booking.Source);
            string src = ctx.HetalFlights.Where(x => x.FlightId==var1).Select(x => x.Source).First();
            var var2 = Convert.ToInt32(booking.Destination);
            string dest = ctx.HetalFlights.Where(x => x.FlightId==var2).Select(x => x.Destination).First();
            
            TempData["Source"] = src;
            TempData["Destination"] = dest;
            return RedirectToAction("ShowRelBookings");
        }
        public ActionResult ShowRelBookings()
        {
            string src = TempData["Source"] as string;
            string dest = TempData["Destination"] as string;
            
            ViewData["Source"] = src;
            ViewData["Destination"] = dest;
            var allFlights = ctx.HetalBookings.Include(b => b.Flight).Where(b => (b.Flight.Source == ViewData["Source"] && b.Flight.Destination==ViewData["Destination"])).ToList();

            return View(allFlights);
        }
        public ActionResult Test(List<HetalFlight> allFlights)
        {
            return View(allFlights);
        }
        [HttpPost]
        public ActionResult SaveBooking(HetalBooking booking)
        {
            ctx.HetalBookings.Add(booking);
            ctx.SaveChanges(); 
            return View();
        }
        [HttpGet]
        public ActionResult EditBooking(int id)
        {
            HetalBooking booking = ctx.HetalBookings.Where(x => x.BookingId == id).SingleOrDefault();
            TempData["Bookingid"] = id;
            return View(booking);
        }
        [HttpPost]
        public ActionResult EditBooking(HetalBooking booking)
        {
            booking.BookingId = Convert.ToInt32(TempData["Bookingid"]);
            ctx.HetalBookings.Update(booking);
            ctx.SaveChanges();
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
        public ActionResult DeleteBooking(int id)
        {
            HetalBooking booking = ctx.HetalBookings.Where(x => x.BookingId == id).SingleOrDefault();
            TempData["Bookingid"] = id;
            return View(booking);
        }
        [HttpPost]
        public ActionResult DeleteBooking(HetalBooking booking)
        {
            booking.BookingId = Convert.ToInt32(TempData["Bookingid"]);
            ctx.HetalBookings.Remove(booking);
            ctx.SaveChanges();
            return RedirectToAction("ShowBookings");
        }
    }
}