using FUnews_PRN_ver2.Extension;
using FUnews_PRN_ver2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace FUnews_PRN_ver2.Controllers
{
	public class StaffController : Controller
	{
		private FunewsManagementContext _context;

		public StaffController(FunewsManagementContext funewsManagementContext)
		{
			_context = funewsManagementContext;
		}
		public ActionResult ManageCategory()
		{
			return RedirectToAction("Index", "Categories");
		}

		public ActionResult ManageNewArticle()
		{
			// show list article
			// edit 
			// change tag

			return View();
		}

		public IActionResult Profile()
		{
			var account = HttpContext.Session.GetObject<SystemAccount>("UserSession");
			return View(account);
		}

		[HttpPost]
		public IActionResult Profile(SystemAccount model, string newPassword, string reNewPassword)
		{
			if (reNewPassword != newPassword)
			{
				TempData["Error"] = "New password and re-new password do not match";
				return View(model);
			}

			var account = HttpContext.Session.GetObject<SystemAccount>("UserSession");

			if (!string.IsNullOrEmpty(model.AccountName))
			{
				account.AccountName = model.AccountName;
			}


			if (!string.IsNullOrEmpty(newPassword) && newPassword == reNewPassword)
			{
				account.AccountPassword = newPassword;
			}


			HttpContext.Session.SetObject("UserSession", account);
			_context.SystemAccounts.Update(account);
			_context.SaveChanges();
			TempData["Success"] = "Profile updated successfully!";
			return RedirectToAction("Profile");
		}


		public IActionResult HistoryCreateNew()
		{
			var account = HttpContext.Session.GetObject<SystemAccount>("UserSession");

			if (account == null)
			{
				return RedirectToAction("Login", "Login");
			}

			var newActive = _context.NewsArticles
				.Include(n => n.Category) // Load dữ liệu Category
				.Include(n => n.CreatedBy) // Load dữ liệu CreatedBy
				.Where(n => n.CreatedBy.AccountId == account.AccountId)
				.ToImmutableList();
			return View(newActive);
		}
	}
}
