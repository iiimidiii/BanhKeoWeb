using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using PagedList;
using PagedList.Mvc;
namespace DoAnWeb.Controllers
{
    public class AdminController : Controller
    {
        QLBanhKeoDataContext data = new QLBanhKeoDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var taikhoan = collection["txtusername"];
            var matkhau = collection["txtpassword"];
            if (String.IsNullOrEmpty(taikhoan))
            {
                ViewData["loi1"] = "Tên tài khoản không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Mật Khẩu không được để trống";
            }
            else
            {
                Admin kh = data.Admins.SingleOrDefault(n => n.UserAdmin == taikhoan && n.PassAdmin == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoanadmin"] = kh;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng";
            }
            return View();

        }
        public ActionResult SanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.SanPhams.ToList().OrderBy(n => n.Masp).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult CreateSanPham()
        {

            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.manhasx = new SelectList(data.Nhasanxuats.ToList().OrderBy(n => n.Tennsx), "manhasx", "Tennsx");
                ViewBag.Maloaihang = new SelectList(data.LoaiHangs.ToList().OrderBy(n => n.tenloaihang), "Maloaihang", "tenloaihang");
                return View();
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSanPham(SanPham sanPham, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình Ảnh Đã Tồn Tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanPham.Anh = fileName;
                data.SanPhams.InsertOnSubmit(sanPham);
                data.SubmitChanges();
                return RedirectToAction("SanPham", "Admin");
            }

        }
        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var sanpham = from sp in data.SanPhams where sp.Masp == id select sp;
                return View(sanpham.SingleOrDefault());
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var sanpham = from sp in data.SanPhams where sp.Masp == id select sp;
                return View(sanpham.SingleOrDefault());
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                SanPham sanPham = data.SanPhams.SingleOrDefault(n => n.Masp == id);
                data.SanPhams.DeleteOnSubmit(sanPham);
                data.SubmitChanges();
                return RedirectToAction("SanPham", "Admin");
            }

        }
        public ActionResult Edit(int id)
        {
            SanPham sanPham = data.SanPhams.SingleOrDefault(n => n.Masp == id);
            ViewBag.sanpham = sanPham.Masp;
            if (sanPham == null)
            {
                return null;
            }
            ViewBag.manhasx = new SelectList(data.Nhasanxuats.ToList().OrderBy(n => n.Tennsx), "manhasx", "Tennsx", sanPham.manhasx);
            ViewBag.Maloaihang = new SelectList(data.LoaiHangs.ToList().OrderBy(n => n.tenloaihang), "Maloaihang", "tenloaihang", sanPham.manhasx);
            return View(sanPham);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult CapNhat(SanPham sanPham, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sanPham.Anh = fileName;
                    //Luu vao CSDL   
                    UpdateModel(sanPham);
                   data.SubmitChanges();

                }
                return RedirectToAction("SanPham");
            }
        }
    

    }
}