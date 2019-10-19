using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrescriptionCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrescriptionCapstone.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userAuthentication = User.Identity.IsAuthenticated;

            if(!userAuthentication)
            {
                return View();
            }

            try
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var currentUser = UserManager.FindById(User.Identity.GetUserId());
                var role = UserManager.GetRoles(User.Identity.GetUserId());

                if (!currentUser.PreviousLogIn)
                {
                    if (role[0].ToString() == RoleName.Doctor)
                    {
                        currentUser.PreviousLogIn = true;
                        context.SaveChanges();
                        return RedirectToAction("Create", "Doctors");
                    }
                    else 
                    {
                        currentUser.PreviousLogIn = true;
                        context.SaveChanges();
                        return RedirectToAction("Create", "Patients");
                    }
                }
                else 
                {
                    if (role[0].ToString() == RoleName.Doctor)
                    {
                        return RedirectToAction("Index", "Doctors");
                    }
                    else 
                    {
                        return RedirectToAction("Details", "Patients");
                    }
                }
            }
            catch 
            {
                return RedirectToAction("Login", "Account");
            }
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