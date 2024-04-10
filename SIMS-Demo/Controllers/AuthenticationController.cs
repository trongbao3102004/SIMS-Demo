using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIMS_Demo.Models;
using System.Text.Json;

namespace SIMS_Demo.Controllers
{
	public class AuthenticationController : Controller
	{

		[HttpPost]
		public IActionResult Login (User user)
		{
			List<User>? users = LoadUsersFromFile("users.json");
			var result = users.FirstOrDefault(u => u.UserName == user.UserName && u.Pass == user.Pass);
			if (result != null)
			{
				HttpContext.Session.SetString("UserName", result.UserName);
				HttpContext.Session.SetString("Role", result.Role);
				return RedirectToAction("Index", "Teacher");
			}
			else
			{
				ViewBag.error = "Invalid user";
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
