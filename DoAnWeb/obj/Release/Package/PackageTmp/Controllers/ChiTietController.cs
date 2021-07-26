using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class ChiTietController : Controller
    {
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();

        // GET: DonHang
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.Chitietdonhangs.ToList());
        }
    }
}