using Schooldesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Schooldesk.Controllers
{
    public class ControllerWrapper : Controller
    {
        public HttpContext CurrentContext { get; private set; }
        public ApplicationUser CurrentUser { get; private set; }
        public ControllerWrapper()
        {
            CurrentContext = System.Web.HttpContext.Current;

            var userId = CurrentContext.User.Identity.GetUserId();
            var db = new ApplicationDbContext();
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            CurrentUser = user != null ? user : new ApplicationUser();
        }
    }
}