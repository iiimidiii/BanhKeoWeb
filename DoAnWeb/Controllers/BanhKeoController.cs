using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

using PagedList;
using PagedList.Mvc;
namespace DoAnWeb.Controllers
{
    public class BanhKeoController : Controller
    {

        QLBanhKeoDataContext data = new QLBanhKeoDataContext();
       

        private List<SanPham>  layhangmoi(int count)
        {

            return data.SanPhams.OrderByDescending(a => a.Ngaynhaphang).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pageSize = 6;

            int pageNum = (page ?? 1);

            var hangmoi = layhangmoi(15);
            return View(hangmoi.ToPagedList(pageNum,pageSize));
        }
        //theo sản phẩm
        public ActionResult LoaiBanhKeo()
        {
            var LoaiHang = from tenloaihang in data.LoaiHangs select tenloaihang;
            return PartialView(LoaiHang);

        }


        public ActionResult SPTheoloai(int id, int ? page)
        {
            int pageSize = 3;

            int pageNum = (page ?? 1);

            var sanpham = from SanPham in data.SanPhams where SanPham.Maloaihang == id select SanPham;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }

        // nhà sản xuất
        public ActionResult NhaSanXuat()
        {
            var NhaSanXuat = from Tennsx in data.Nhasanxuats select Tennsx;
            return PartialView(NhaSanXuat);

        }

        public ActionResult SPTheoNhaSanXuat(int id , int? page)
        {
            int pageSize = 3;

            int pageNum = (page ?? 1);

            var sanpham = from SanPham in  data.SanPhams where SanPham.manhasx == id select SanPham;
            return View(sanpham.ToPagedList(pageNum, pageSize));

        }

        public ActionResult DetailsNhaSanXuat(int id)
        {
            var NhaSanXuat = from Nhasanxuat in data.Nhasanxuats
                             where Nhasanxuat.manhasx == id
                          select Nhasanxuat;
            return View(NhaSanXuat);
        }

        public ActionResult Details(int id)
        {
            var sanpham = from SanPham in data.SanPhams
                          where SanPham.Masp == id
                          select SanPham;
            return View(sanpham);
        }


        public ActionResult DangKy()
        {  
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["Hotenkh"];
            var taikhoan = collection["tenDn"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["Nhaplaimatkhau"];
            var emailkh = collection["email"];
            var diachikh = collection["Diachi"];
            var SDT = collection["Sdt"];
            var Ngay = string.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Họ tên không được để trống";
           
            }
            else if(String.IsNullOrEmpty(taikhoan))
            {
                ViewData["loi2"] = "Tên tai khoản không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Mật Khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["loi4"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(emailkh))
            {
                ViewData["loi5"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(SDT))
            {
                ViewData["loi6"] = "Số điện thoại không được để trống";
            }
            else
            {
                kh.Hoten = hoten;
                kh.TaiKhoan= taikhoan;
                kh.MatKhau = matkhau;
                kh.Email = emailkh;
                kh.Diachikh = diachikh;
                kh.sdt = SDT;
                kh.Ngaysinh = DateTime.Parse(Ngay);
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taikhoan = collection["tenDn"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(taikhoan))
            {
                ViewData["loi1"] = "Tên tai khoản không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Mật Khẩu không được để trống";
            }
            else
            {
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "BanhKeo");
                }
                else
                    ViewBag.Thongbao = "TÊN ĐĂNG NHẬP KHÔNG ĐÚNG";
            }
            return View();

        }
        public ActionResult LienLac()
        {
            return View();
        }
       

    }
}
