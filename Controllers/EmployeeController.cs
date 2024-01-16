using firstmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstmvc.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult GetAllEmployees()
        {
            List<Employee> emps = new List<Employee>();
            Employee e = new Employee();
            e.Eid = 100;
            e.Name = "Hetal";
            e.Salary = 100.0F;
            Employee e1 = new Employee();
            e1.Eid = 101;
            e1.Name = "Golu";
            e1.Salary = 101.0F;
            emps.Add(e);
            emps.Add(e1);
            return View(emps);
        }
    }
}