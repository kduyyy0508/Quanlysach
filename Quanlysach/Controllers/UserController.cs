using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlysach.Models;

namespace Quanlysach.Controllers
{
    public class UserController : Controller
    {
        QLBANSACHEntities1 database = new QLBANSACHEntities1();

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.HoTenKH))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(kh.Matkhau))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(kh.DienthoaiKH))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN);
                var email_khachhang = database.KHACHHANGs.FirstOrDefault(k => k.Email == kh.Email);

                if (khachhang != null)
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập đã được sử dụng");
                if (email_khachhang != null)
                    ModelState.AddModelError(String.Empty, "Email đã được sử dụng");
                if (ModelState.IsValid)
                {
                    database.KHACHHANGs.Add(kh);
                    database.SaveChanges();
                }
                else
                    return View();
            }    
            return RedirectToAction("DangNhap");
        }
        [HttpPost]
        public ActionResult DangNhap(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.Matkhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN && k.Matkhau == kh.Matkhau);
                    if (khach !=null)
                    {
                        ViewBag.ThongBao = "Đã đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        Session["IsLoggedIn"] = true;
                    }
                    else                    
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";                   
                }
                
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("DangNhap");
        }
    }
}