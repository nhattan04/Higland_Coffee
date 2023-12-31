﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;

namespace Highland.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        public ActionResult Index()
        {
            var list = database.Categories.ToList();
            ViewBag.ListStyle = list;
            return PartialView(list);
        }
    }
}