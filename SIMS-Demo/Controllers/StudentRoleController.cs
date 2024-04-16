using Microsoft.AspNetCore.Mvc;
using SIMS_Demo.Models;
using System.Text.Json;

namespace SIMS_Demo.Controllers
{
    public class StudentRoleController : Controller
    {
        static List<Student> students = new List<Student>();
        static List<Course> courses = new List<Course>();
        static List<Class> classes = new List<Class>();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewStudent()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            students = LoadStudentFromFile("studentdata.json");
            return View(students);
        }
        public IActionResult ViewCourse()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            courses = LoadCourseFromFile("course.json");
            return View(courses);
            
        }
        public IActionResult ViewClass()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            classes = LoadClassFromFile("class.json");
            return View(classes);
        }

        public List<Class>? LoadClassFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("class.json");
            return JsonSerializer.Deserialize<List<Class>>(readText);
        }
        public List<Student>? LoadStudentFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("studentdata.json");
            return JsonSerializer.Deserialize<List<Student>>(readText);
        }
        public List<Course>? LoadCourseFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("course.json");
            return JsonSerializer.Deserialize<List<Course>>(readText);
        }
    }
}
