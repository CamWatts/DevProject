using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevProject.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Reports";

            return View();
        }
    }
}