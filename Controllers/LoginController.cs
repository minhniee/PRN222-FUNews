using FUnews_PRN_ver2.Extension;
using FUnews_PRN_ver2.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUnews_PRN_ver2.Controllers
{
	public class LoginController : Controller
	{
		private FunewsManagementContext _context;
		private readonly IConfiguration _configuration;

		public LoginController(IConfiguration configuration, FunewsManagementContext funewsManagementContext) // function nay goi chung la gi 
		{
			_context = funewsManagementContext;
			_configuration = configuration;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string accountEmail, string accountPassword)
		{
			if (!ModelState.IsValid)
			{
				return View(); // Nếu ModelState không hợp lệ
			}
			// get admin account
			var configUsername = _configuration["LoginCredentials:AccountEmail"];
			var configPassword = _configuration["LoginCredentials:AccountPassword"];

			// piority admin account
			if (accountEmail.Equals(configUsername) &&
				accountPassword.Equals(configPassword))
			{
				HttpContext.Session.SetObject("UserSession", new SystemAccount()
				{
					AccountEmail = configUsername,
					AccountPassword = configPassword,
					AccountRole = 0,
					AccountName = "Admin"
				});
				return RedirectToAction("Index", "News");
			}


			var account = _context.SystemAccounts.FirstOrDefault(a =>
				a.AccountEmail == accountEmail &&
				a.AccountPassword == accountPassword);

			HttpContext.Session.SetObject("UserSession", account);

			if (account == null)
			{
				ModelState.AddModelError("", "Email or Password is wrong");
				return View();
			}

			return account.AccountRole switch
			{
				2 => RedirectToAction("Index", "Lecture"),
				1 => RedirectToAction("Index", "Lecture"),
				_ => View()
			};
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear(); // Xóa tất cả session khi đăng xuất
			return RedirectToAction("Login");
		}
	}
}
