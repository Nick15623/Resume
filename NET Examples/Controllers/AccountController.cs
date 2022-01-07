using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schooldesk.Helpers;
using Schooldesk.Models;

namespace Schooldesk.Controllers
{
    public class AccountController : ControllerWrapper
    {
        private UsersHelper _helper;

        public AccountController()
        {
            _helper = new UsersHelper(CurrentContext, CurrentUser);
        }

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrWhiteSpace(ReturnUrl) == false)
            {
                var vm = new LoginPageViewModel()
                {
                    ReturnUrl = ReturnUrl
                };
                return View("login", vm);
            }
            return View("login");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated == true)
            {
                _helper.Logout();
            } 
            return Redirect("/");
        }
    }
}