using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class LoaisanphamController : Controller
    {
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();
        // GET: Loaisanpham
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.LoaiHangs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(LoaiHang loaiHang)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.LoaiHangs.InsertOnSubmit(loaiHang);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loaisanpham");
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var loaihang = from lh in data.LoaiHangs where lh.Maloaihang == id select lh;
                return View(loaihang.Single());
            }

        }
        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                LoaiHang loaiHang= data.LoaiHangs.SingleOrDefault(n => n.Maloaihang == id);
                UpdateModel(loaiHang);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loaisanpham");
            }

        }
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            { 
                var loaihang = from lh in data.LoaiHangs where lh.Maloaihang == id select lh;
                return View(loaihang.Single());
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                LoaiHang loaiHang = data.LoaiHangs.SingleOrDefault(n => n.Maloaihang == id);
                data.LoaiHangs.DeleteOnSubmit(loaiHang);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loaisanpham");
            }

        }
    }
}