using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schooldesk.Controllers
{
    public class MyAdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public ActionResult ContentCreator()
        {
            return View();
        }

        [Authorize(Roles = "SS")]
        public ActionResult DefaultComponentEditor()
        {
            return View();
        }
    }
}