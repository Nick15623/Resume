using Schooldesk.Helpers;
using Schooldesk.Models;
using Schooldesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schooldesk.Controllers
{
    public class DefaultController : ControllerWrapper
    {
        private ApplicationDbContext _db;

        public DefaultController()
        {
            _db = new ApplicationDbContext();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Page(bool isDefault = false)
        {
            var vm = new PageViewModel();
            var dirName = this.RouteData.Values["dir"]?.ToString();
            var pageName = this.RouteData.Values["page"]?.ToString();

            var helper = new ContentHelper(CurrentContext, CurrentUser);
            var response = await helper.GetPageForUserAsync(dirName, pageName, false, isDefault);
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

            return View("Default/page", vm);
        }

        [Authorize]
        public async Task<ActionResult> Edit(bool isDefault = false)
        {
            var vm = new PageViewModel();
            var dirName = this.RouteData.Values["dir"]?.ToString();
            var pageName = this.RouteData.Values["page"]?.ToString();

            var helper = new ContentHelper(CurrentContext, CurrentUser);
            var response = await helper.GetPageForUserAsync(dirName, pageName, true, isDefault);
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
                    
                    return View("Default/edit", vm);
                }
            }

            return Redirect("/");
        }
    }
}