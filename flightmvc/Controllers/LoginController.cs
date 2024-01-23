using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace flightmvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly Ace52024Context ctx;
        private readonly ISession session;
        //Dependency Injection  in constructor
        public LoginController(Ace52024Context _ctx, IHttpContextAccessor HttpContextAccessor)
        {
            ctx=_ctx;
            session=HttpContextAccessor.HttpContext.Session;
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
            if(result!=null && result.Email!="admin@fareportal.com")
            {
                HttpContext.Session.SetString("Username", result.Username);
                return RedirectToAction("AddBooking","Booking");
            } 
            // else if(result.Email!="admin@fareportal.com")
            // {
            //     ViewBag.ShowAlert = true;
            //     return View();
            // }
            else if(u.Email=="admin@fareportal.com" && u.Password=="admin")
            {
                return RedirectToAction("ShowBookings","Booking");
            }
            else{
                ViewBag.ShowAlert = true;
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult ShowUsers()
        {
            List<HetalUsertable> users = [..ctx.HetalUsertables.Where(h => true)];
            return View(users);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(HetalUsertable u)
        {
            if(ModelState.IsValid)
            {
                ctx.HetalUsertables.Add(u);
                ctx.SaveChanges();
                return RedirectToAction("Login");
            }
            else{
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}