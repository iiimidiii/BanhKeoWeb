using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class DonHangController : Controller
    {
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();

        // GET: DonHang
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.Donhangs.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var donhang = from dh in data.Donhangs where dh.Madonhang == id select dh;
                return View(donhang.Single());
            }

        }
        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Donhang donhang= data.Donhangs.SingleOrDefault(n => n.Madonhang == id);
                UpdateModel(donhang);
                data.SubmitChanges();
                return RedirectToAction("Index", "DonHang");
            }

        }
    }
}