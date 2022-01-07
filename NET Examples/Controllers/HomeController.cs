using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Schooldesk.Helpers;
using Schooldesk.Models;
using Schooldesk.ViewModels;

namespace Schooldesk.Controllers
{
    public class HomeController : ControllerWrapper
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var vm = new PageViewModel();
            var helper = new ContentHelper(CurrentContext, CurrentUser);
            var response = await helper.GetPageForUserAsync("index", "", isDefault: true);
            if (response.IsSuccess)
            {
                var contentPage = response.Data;
                if (contentPage != null)
                {
                    vm.ContentPageId = contentPage.Id;
                    ViewBag.Title = contentPage.Title;
                    ViewBag.Author = contentPage.Author;
                    ViewBag.MetaKeywords = contentPage.MetaKeywords;
                    ViewBag.MetaDescription = contentPage.MetaDescription;
                }
            }

            return View("Default/Page", vm);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}