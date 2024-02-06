using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentApp.Models;

namespace StudentApp.Controllers;

public class StudentController : Controller
{
    private static List<Student> st = new List<Student>();
    public ActionResult AddStudent()
    {
        // Student studentnew = new Student();
        // int id = Convert.ToInt32(Console.ReadLine());
        // studentnew.Id = id;
        // string nm = Console.ReadLine();
        // studentnew.Name = nm;
        // studentnew.Enrolldate = DateTime.Now;
        // st.Add(studentnew);
        return View();
    }
    [HttpPost]
    public ActionResult AddStudent(Student entry)
    {
        st.Add(entry);
        return RedirectToAction("ShowStudents");
    }
    public ActionResult ShowStudents(String sortOrder, String searchString)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["CurrentFilter"] = searchString;
        if (!String.IsNullOrEmpty(searchString))
        {
            st = st.Where(s => s.Course.Contains(searchString)).ToList();
        }
        switch (sortOrder)
        {
            case "name_desc":
                st.Sort((x, y) => y.Name.CompareTo(x.Name));
                break;
            default:
                st.Sort((x, y) => x.Id.CompareTo(y.Id));
                break;
        }
        return View(st);
    }
}