using FUnews_PRN_ver2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace FUnews_PRN_ver2.Controllers
{
	public class NewsController : Controller
	{
		private FunewsManagementContext _context;

		public NewsController(FunewsManagementContext funewsManagementContext)
		{
			_context = funewsManagementContext;
		}
		public IActionResult Index()
		{
			var newActive = _context.NewsArticles
				.Include(n => n.Category) // Load dữ liệu Category
				.Include(n => n.CreatedBy) // Load dữ liệu CreatedBy
				.Where(n => n.NewsStatus == true)
				.ToImmutableList();

			return View(newActive.ToImmutableList());
		}

		public IActionResult Details(int id)
		{
			var article = _context.NewsArticles
				.Include(n => n.Category)
				.Include(n => n.CreatedBy)
				.FirstOrDefault(n => n.NewsStatus == true && n.NewsArticleId == id.ToString());

			if (article == null)
			{
				return NotFound(); // Return 404 if not found
			}

			return View(article);
		}




		// for admin 
		public IActionResult ReportStatisticNew(DateTime? sDate, DateTime? eDate)
		{

			if (!sDate.HasValue || !eDate.HasValue)
			{
				sDate = DateTime.Now.AddDays(-30);
				eDate = DateTime.Now;
			}


			var newActive = _context.NewsArticles
				.Include(n => n.Category) // Load dữ liệu Category
				.Include(n => n.CreatedBy) // Load dữ liệu CreatedBy
				.Where(n => n.CreatedDate >= sDate && n.CreatedDate <= eDate)
				.OrderByDescending(n => n.CreatedDate)
				.ToImmutableList();

			ViewBag.StartDate = sDate.Value.ToString("yyyy-MM-dd");
			ViewBag.EndDate = eDate.Value.ToString("yyyy-MM-dd");

			return View(newActive);
		}

		//public IActionResult ChangeTags()
		//{

		//}

	}
}
