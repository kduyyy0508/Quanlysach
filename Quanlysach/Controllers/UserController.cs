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
        [HttpPost]
        public ActionResult DangKy(KHACHHANG u)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(u.HoTenKH))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(u.TenDN))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(u.Matkhau))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(u.Email))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                if (string.IsNullOrEmpty(u.DienthoaiKH))
                    ModelState.AddModelError(string.Empty, "Còn trống");
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == u.TenDN);
                var email_khachhang = database.KHACHHANGs.FirstOrDefault(k => k.Email == u.Email);

                if (khachhang != null)
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập đã được sử dụng");
                if (email_khachhang != null)
                    ModelState.AddModelError(String.Empty, "Email đã được sử dụng");
                if (ModelState.IsValid)
                {
                    database.KHACHHANGs.Add(u);
                    database.SaveChanges();
                }
                else
                    return View();
            }    
            return RedirectToAction("DangNhap");
        }
        [HttpPost]
        public ActionResult DangNhap(KHACHHANG u)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(u.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(u.Matkhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var kh= database.KHACHHANGs.FirstOrDefault(k=>k.TenDN==u.TenDN && k.Matkhau==u.Matkhau);
                    if (kh!=null)
                    {
                        ViewBag.ThongBao = "Đã đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("DangKy");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                        return RedirectToAction("DangKy");
                    }
                }

            }
            return RedirectToAction("DangKy");
        }
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
    }
}