using firstmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvc.Controllers
{
    public class BankController : Controller
    {
        public static Ace52024Context ctx = new Ace52024Context();
        public ActionResult ShowAccounts()
        {
            List<HetalSbaccount> acc = [..ctx.HetalSbaccounts.Where(h => true)];
            return View(acc);
        }
       public ActionResult ShowTransactions()
        {
            List<HetalSbtransaction> trans = [..ctx.HetalSbtransactions.Where(h => true)];
            return View(trans);
        }
    }
}