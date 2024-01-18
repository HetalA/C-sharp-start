using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace flightmvc.Controllers
{
    public class LoginController : Controller
    {
        public static Ace52024Context ctx;
        //Dependency Injection  in constructor
        public LoginController(Ace52024Context _ctx)
        {
            ctx=_ctx;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(HetalUsertable u)
        {
            var result = (from i in ctx.HetalUsertables where i.Email==u.Email 
            && i.Password==u.Password select i).SingleOrDefault();  
            if(result!=null)
            {
                HttpContext.Session.SetString("uname", result.Username);
                return RedirectToAction("ShowBookings","Booking");
            } 
            else
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(HetalUsertable u)
        {
            ctx.HetalUsertables.Add(u);
            ctx.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}