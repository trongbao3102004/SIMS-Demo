using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIMS_Demo.Models;
using System.Text.Json;

namespace SIMS_Demo.Controllers
{
	public class AuthenticationController : Controller
	{

        [HttpPost]
        public IActionResult Login(User user)
        {
            // Đọc thông tin người dùng từ file users.json
            List<User> users = LoadUsersFromFile("users.json");
            var result = users.Find(u => u.UserName == user.UserName && u.Pass == user.Pass);

            if (result != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserName", result.UserName);
                HttpContext.Session.SetString("Role", result.Role);

                // Chuyển hướng đến trang tương ứng với vai trò của người dùng
                switch (result.Role)
                {
                    case "teacher":
                        return RedirectToAction("Index", "Teacher");
                    case "student":
                        return RedirectToAction("Index", "Student");
                    default:
                        return RedirectToAction("Index", "Home"); // Chuyển hướng mặc định nếu không phân quyền
                }
            }
            else
            {
                // Thông báo lỗi nếu tài khoản không hợp lệ
                ViewBag.error = "Invalid user!";
                return View("Login");
            }
        }
        [HttpGet]
        public IActionResult Login()
		{
			return View();
		}
        public List<User> LoadUsersFromFile(string filename)
        {
            string readText = System.IO.File.ReadAllText("users.json");
            return JsonSerializer.Deserialize<List<User>>(readText);
        }
        public ActionResult Index()
		{
			return View();
		}

		// GET: Authentication/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Authentication/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Authentication/Create
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

		// GET: Authentication/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Authentication/Edit/5
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

		// GET: Authentication/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Authentication/Delete/5
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
