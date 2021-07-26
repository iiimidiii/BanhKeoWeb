using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: NhaCungCap
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            return View(data.Nhasanxuats.ToList());
        }
        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhasanxuat = from nsx in data.Nhasanxuats where nsx.manhasx == id select nsx;
                return View(nhasanxuat.Single());
            }
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
        public ActionResult Create(Nhasanxuat nhasanxuat)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.Nhasanxuats.InsertOnSubmit(nhasanxuat);
                data.SubmitChanges();
                return RedirectToAction("Index","NhaCungCap");
            }
                
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhasanxuat = from nsx in data.Nhasanxuats where nsx.manhasx == id select nsx;
                return View(nhasanxuat.Single());
            }

        }
        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Nhasanxuat nhasanxuat = data.Nhasanxuats.SingleOrDefault(n => n.manhasx == id);
                UpdateModel(nhasanxuat);
                data.SubmitChanges();
                return RedirectToAction("Index","NhaCungCap");
            }
                
        }
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhasanxuat = from nsx in data.Nhasanxuats where nsx.manhasx == id select nsx;
                return View(nhasanxuat.Single());
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Nhasanxuat nhasanxuat = data.Nhasanxuats.SingleOrDefault(n => n.manhasx == id);
                data.Nhasanxuats.DeleteOnSubmit(nhasanxuat);
                data.SubmitChanges();
                return RedirectToAction("Index", "NhaCungCap");
            }

        }
    }
}