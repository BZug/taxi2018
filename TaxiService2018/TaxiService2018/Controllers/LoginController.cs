using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiService2018.Database;
using TaxiService2018.Models;
using TaxiService2018.ViewModels;

namespace TaxiService2018.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        

        public ActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            var user = (ApplicationUser)Session["User"];
            if (user != null)
            {
                return RedirectToAction("Home", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInForm form)
        {
            var user = (ApplicationUser)Session["User"];
            if (user != null)
            {
                return RedirectToAction("Home", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View("SignIn", form);
            }

            var dbUser = db.ApplicationUsers.SingleOrDefault(u => u.Username == form.Username && u.Password == form.Password);
            if (dbUser == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View("SignIn", form);
            }

            var loginUser = new ApplicationUser();
            //loginUser.GetLoginData(dbUser);
            //Session["User"] = loginUser;
            Session["User"] = dbUser;

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            var user = (ApplicationUser)Session["User"];
            if (user == null)
            {
                return RedirectToAction("SignIn");
            }

            Session.Abandon();
            Session["User"] = null;

            return RedirectToAction("SignIn");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}