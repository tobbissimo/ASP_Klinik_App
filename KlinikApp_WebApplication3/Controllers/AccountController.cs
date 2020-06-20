using KlinikApp_WebApplication3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KlinikApp_WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            Employee elogin;

            if (ModelState.IsValid)
            {
                using (var db = new KlinikDbEntities())
                {
                    var erg = from e in db.Employees
                              where e.Emp_Lastname == model.UserName && e.Emp_Pwd == model.Password
                              select e;
                    elogin = erg.FirstOrDefault();

                }

                if (elogin != null)
                {

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    Session["emp_id"] = elogin.Emp_Id;
                    return RedirectToLocal(returnUrl);  // Url  ?? "~/Home/Index"
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();   
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Examinations");
            }
        }
    }
}