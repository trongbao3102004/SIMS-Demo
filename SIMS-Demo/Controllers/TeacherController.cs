using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIMS_Demo.Models;
using System.Text.Json;

namespace SIMS_Demo.Controllers
{
    public class TeacherController : Controller
    {
        static List<Teacher> teachers = new List<Teacher>();
        public IActionResult Delete(int Id)
        {
            var teacher = LoadTeacherFromFile("data.json");
            var searchTeacher = teacher.FirstOrDefault(t => t.Id == Id);
            teacher.Remove(searchTeacher);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(teachers, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("data.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult NewTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
            var option = new JsonSerializerOptions { WriteIndented= true };
            string jsonString = JsonSerializer.Serialize(teachers, option);
            
            using (StreamWriter writer = new StreamWriter("data.json"))
            {
                writer.Write(jsonString);
            }
            return Content(jsonString);
        }

        [HttpGet]
        public ActionResult NewTeacher()
        {
            return View();
        }
        public List<Teacher> LoadTeacherFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("data.json");
            return JsonSerializer.Deserialize<List<Teacher>>(readText);
        }
        public ActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            teachers = LoadTeacherFromFile("data.json");
            return View(teachers);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

 

        // POST: TeacherController/Delete/5
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
