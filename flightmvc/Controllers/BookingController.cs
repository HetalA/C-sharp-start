using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.Extensions;
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