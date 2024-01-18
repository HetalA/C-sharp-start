using flightmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace flightmvc.Controllers
{
    public class CustomerController : Controller
    {
        public static Ace52024Context ctx;
        //Dependency Injection  in constructor
        public CustomerController(Ace52024Context _ctx)
        {
            ctx=_ctx;
        }
        public ActionResult ShowCustomers()
        {
            List<HetalCustomer> customers = [..ctx.HetalCustomers.Where(h => true)];
            return View(customers);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost] //button click logic 
        public ActionResult AddCustomer(HetalCustomer customer)
        {
            ctx.HetalCustomers.Add(customer);
            ctx.SaveChanges();  
            return RedirectToAction("ShowCustomers");
        }
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            HetalCustomer cust = ctx.HetalCustomers.Where(x => x.CustomerId == id).SingleOrDefault();
            TempData["custid"] = id;
            return View(cust);
        }
        [HttpPost]
        public ActionResult EditCustomer(HetalCustomer customer)
        {
            customer.CustomerId = Convert.ToInt32(TempData["custid"]);
            ctx.HetalCustomers.Update(customer);
            ctx.SaveChanges();
            return RedirectToAction("ShowCustomers");
        }
        [HttpGet]
        public ActionResult CustomerDetails(int id)
        {
            HetalCustomer customer = ctx.HetalCustomers.Where(x => x.CustomerId == id).SingleOrDefault();
            TempData["custid"] = id;
            return View(customer);
        }
        [HttpPost]
        public ActionResult CustomerDetails(HetalCustomer customer)
        {
            return View(customer);
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            HetalCustomer customer = ctx.HetalCustomers.Where(x => x.CustomerId == id).SingleOrDefault();
            TempData["custid"] = id;
            return View(customer);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(HetalCustomer customer)
        {
            customer.CustomerId = Convert.ToInt32(TempData["custid"]);
            ctx.HetalCustomers.Remove(customer);
            ctx.SaveChanges();
            return RedirectToAction("ShowCustomers");
        }
    }
}