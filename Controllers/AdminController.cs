using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;

namespace Highland.Controllers
{
    public class AdminController : Controller
    {
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        // GET: Admin
        public ActionResult ShowListPro()
        {
            return View(database.Products.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product products)
        {
            database.Products.Add(products);
            database.SaveChanges();
            return RedirectToAction("ShowListPro");
        }
        public ActionResult Details(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product products)
        {
            database.Entry(products).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("ShowListPro");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product products)
        {
            products=database.Products.Where(s => s.ID == id).FirstOrDefault();
            database.Entry(products).State = System.Data.Entity.EntityState.Modified;
            database.Products.Remove(products);
            database.SaveChanges();
            return RedirectToAction("ShowListPro");
        }            
    }
}