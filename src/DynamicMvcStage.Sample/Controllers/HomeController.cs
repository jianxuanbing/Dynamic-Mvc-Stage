using DynamicMvcStage.Service;
using DynamicMvcStage.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicMvcStage.Sample.Controllers
{
    [TestFilter(Action = "Home")]
    public class HomeController : Controller
    {
 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}