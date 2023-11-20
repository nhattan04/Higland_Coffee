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
            database.Customers.Add(cus);
            database.SaveChanges();
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
    }
}