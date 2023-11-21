using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;

namespace Highland.Controllers
{
    public class CustomerController : Controller
    {
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        // GET: Customer
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create( Customer cus)
        {

            try
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                database.Customers.Add(cus);
                database.SaveChanges();
                int cusID = cus.ID;
                Session["IDCus"] = cusID;
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            catch
            {
                return Content("Error with saving data.");
            }
        }
        public ActionResult CustomerList()
        {
            var cus = database.Customers.ToList();
            return View(cus);
        }  
    }
}