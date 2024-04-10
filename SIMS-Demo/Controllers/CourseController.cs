using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_Demo.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIMS_IT0602.Controllers
{
    public class CourseController : Controller
    {
        static List<Course> courses = new List<Course>();
        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            courses.Add(course);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("course.json"))
            {
                writer.Write(jsonString);
            }

            return Content(jsonString);
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            List<Teacher>? teachers = LoadTeacherFromFile("data.json");

            ViewBag.SelectTeacher = teachers;
            return View();
        }

        public List<Teacher>? LoadTeacherFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}