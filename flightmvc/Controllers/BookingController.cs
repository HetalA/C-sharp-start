using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ctx.HetalBookings.Add(booking);
            ctx.SaveChanges();  
            return RedirectToAction("ShowBookings");
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