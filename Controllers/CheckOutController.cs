using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;

namespace Highland.Controllers
{
    public class CheckOutController : Controller
    {
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }
    }
}