using Microsoft.AspNetCore.Mvc;
using SIMS_Demo.Models;
using System.Text.Json;

namespace SIMS_Demo.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> students = new List<Student>();
        public IActionResult Delete(int Id)
        {
            var students = LoadStudentFromFile("studentdata.json");
            var searchStudent = students.FirstOrDefault(t => t.Id == Id);
            students.Remove(searchStudent);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("studentdata.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult NewStudent(Student student)
        {
            students.Add(student);
            var option = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, option);

            using (StreamWriter writer = new StreamWriter("studentdata.json"))
            {
                writer.Write(jsonString);
            }
            return Content(jsonString);
        }
 
        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewStudent()
        {
            return View();
        }
        public List<Student>? LoadStudentFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("studentdata.json");
            return JsonSerializer.Deserialize<List<Student>>(readText);
        }
        public ActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            students = LoadStudentFromFile("studentdata.json");
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult ManageStudent(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            students = LoadStudentFromFile("student.json");
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
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

        // GET: StudentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            var existingStudent = students.FirstOrDefault(t => t.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Id = student.Id;
            existingStudent.Name = student.Name;
            existingStudent.DoB = student.DoB;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            System.IO.File.WriteAllText("studentdata.json", jsonString);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id, IFormCollection collection)
        {

            {
                return View();
            }
        }
        // POST: StudentController/Delete/5
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
