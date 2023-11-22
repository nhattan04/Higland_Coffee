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
        public ActionResult CustomerList()
        {
            var cus = database.Customers.ToList();
            return View(cus);
        }  
    }
}