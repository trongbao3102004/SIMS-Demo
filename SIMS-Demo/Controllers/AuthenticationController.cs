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
            // Kiểm tra xem tên người dùng có được nhập không
            if (string.IsNullOrEmpty(user.UserName))
            {
                // Thông báo lỗi nếu tên người dùng để trống
                ViewBag.error = "Username cannot be empty!";
                return View("Login");
            }

            // Kiểm tra xem mật khẩu có được nhập không
            if (string.IsNullOrEmpty(user.Pass))
            {
                // Thông báo lỗi nếu mật khẩu để trống
                ViewBag.error = "Password cannot be empty!";
                return View("Login");
            }

            // Đọc thông tin người dùng từ file users.json
            List<User> users = LoadUsersFromFile("users.json");
            var result = users.Find(u => u.UserName == user.UserName);

            if (result != null)
            {
                if (result.Pass == user.Pass)
                {
                    // Lưu thông tin người dùng vào session
                    HttpContext.Session.SetString("UserName", result.UserName);
                    HttpContext.Session.SetString("Role", result.Role);

                    // Chuyển hướng đến trang tương ứng với vai trò của người dùng
                    switch (result.Role)
                    {
                        case "Admin":
                            return RedirectToAction("Index", "Admin");
                        case "Teacher":
                            return RedirectToAction("Index", "Teacher");
                        case "Student":
                            return RedirectToAction("Index", "Student");
                        default:
                            return RedirectToAction("Index", "Home"); // Chuyển hướng mặc định nếu không phân quyền
                    }
                }
                else
                {
                    // Thông báo lỗi nếu mật khẩu không đúng
                    ViewBag.error = "Incorrect password!";
                    return View("Login");
                }
            }
            else
            {
                // Thông báo lỗi nếu tên người dùng không tồn tại
                ViewBag.error = "User not found!";
                return View("Login");
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public List<User>? LoadUsersFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<User>>(readText);
        }

        [HttpGet] //click hyperlink
        public IActionResult Register()
        {
            return View();
        }

    }
}
