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
        public List<Course>? LoadCourseFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("course.json");
            return JsonSerializer.Deserialize<List<Course>>(readText);
        }
        public ActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            courses = LoadCourseFromFile("course.json");
            return View(courses);
        }
        public ActionResult ManageCourse(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            courses = LoadCourseFromFile("course.json");
            return View(courses);
        }
        public List<Teacher>? LoadTeacherFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            var existingCourse = courses.FirstOrDefault(t => t.Id == course.Id);
            if (existingCourse == null)
            {
                return NotFound();
            }
            existingCourse.Id = course.Id;
            existingCourse.Name = course.Name;
            existingCourse.Lecturer = course.Lecturer;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);
            System.IO.File.WriteAllText("course", jsonString);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id, IFormCollection collection)
        {

            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult EditCourse()
        {
            List<Teacher>? teachers = LoadTeacherFromFile("data.json");

            ViewBag.SelectTeachers = teachers;
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var courses = LoadCourseFromFile("course.json");
            var searchCourse = courses.FirstOrDefault(t => t.Id == Id);
            courses.Remove(searchCourse);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(courses, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("course.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}