using FUnews_PRN_ver2.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUnews_PRN_ver2.Controllers
{
    public class LectureController : Controller
    {
        private FunewsManagementContext _context;




        // GET: LectureController
        public IActionResult Index()
        {


            return RedirectToAction("Index", "News");


        }

        //GET: LectureController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", "News");
        }
    }
}
