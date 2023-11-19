using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Highland.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuanCaPhe()
        {
            return View();
        }
        public ActionResult ThucDon()
        {
            return View();
        }
        public ActionResult TinTuc()
        {
            return View();
        }
        public ActionResult CongDong()
        {
            return View();
        }
        public ActionResult VeChungToi()
        {
            return View();
        }
        public ActionResult NgheNghiep()
        {
            return View();
        }
        public ActionResult MuaNgay()
        {
            return View();
        }
    }
}