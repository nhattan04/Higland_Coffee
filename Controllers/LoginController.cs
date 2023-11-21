using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;


namespace Login.Controllers
{
    public class LoginController : Controller
    {
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = database.AdminUsers.Where(s => s.UserName == _user.UserName && s.UserPass == _user.UserPass).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.UserName;
                return RedirectToAction("Index", "Shopping");
            }
           
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]

        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    //Tao role mac dinh la member
                    _user.UserRole = "Member";
                    database.AdminUsers.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exist";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}