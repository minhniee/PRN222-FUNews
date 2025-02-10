using Microsoft.AspNetCore.Mvc;

namespace FUnews_PRN_ver2.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return RedirectToAction("Index", "News");
        }

        public ActionResult AcountManager()
        {
            return RedirectToAction("Index", "SystemAccounts");
        }


        public ActionResult ReportStatistic()
        {
            return RedirectToAction("ReportStatisticNew", "News");
        }



    }
}
