using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class KhachHangController : Controller
    {
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();
        // GET: Loaisanpham
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.KhachHangs.ToList());
        }
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var khachhang = from kh in data.KhachHangs where kh.MaKH == id select kh;
                return View(khachhang.Single());
            }

        }
        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                KhachHang khachHang = data.KhachHangs.SingleOrDefault(n => n.MaKH == id);
                UpdateModel(khachHang);
                data.SubmitChanges();
                return RedirectToAction("Index", "KhachHang");
            }

        }
    }
}