using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MVCAppWithDb.Models;
using MyApp.Db;
using MyApp.Db.Model;
using MyApp.Models;

namespace MVCAppWithDb.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserModel user)
        {
            bool Status = false;
            string message = "";
            
            // Model Validation 
            if (ModelState.IsValid)
            {
                #region Email is already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion  

                #region  Password Hashing 
                //user.Password = MyApp.Db.Crypto.Hash(user.Password);
                //user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); 
                #endregion

                #region Save to Database
                using (var dc = new DatabaseContext())
                {
                    User u = new User() { 
                        FullName = user.FullName,
                        Email = user.Email,
                        Password = user.Password,
                    };
                    dc.User.Add(u);
                    dc.SaveChanges();
                    Status = true;
                }
                return RedirectToAction("Login", "Account");
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            using (var dc = new DatabaseContext())
            {
                HttpContext.Session["UserEmailId"] = login.Email;
                //login.Password = MyApp.Db.Crypto.Hash(login.Password);
                bool isValid = dc.User.Any(x => x.Email == login.Email && x.Password == login.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Username and Password");
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (var dc = new DatabaseContext())
            {
                var v = dc.User.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }
    }
}