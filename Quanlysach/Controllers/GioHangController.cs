using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlysach.Models;

namespace Quanlysach.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public List<MatHangMua> LayGioHang()
        {
            List<MatHangMua> gioHang= Session["GioHang"] as List<MatHangMua>;
            if(gioHang== null)
            {
                gioHang = new List<MatHangMua>();
                Session["GioHang"] = gioHang;
            }    
            return gioHang;
        }
        public ActionResult ThemSanPhamVaoGio(int MaSP)
        {
            //Lấy giỏ hàng hiện tại
            List<MatHangMua> gioHang = LayGioHang();
            //Nếu sp đã tồn tại thì tăng lên 1 ngược lại thêm mới vào giỏ hàng
            MatHangMua spNew = gioHang.FirstOrDefault(s => s.MaSach == MaSP);
            //chưa có sp
            if (spNew == null)
            {
                spNew = new MatHangMua(MaSP);
                gioHang.Add(spNew);
            }
            else
                spNew.SoLuong++;
            return RedirectToAction("Details", "Bookstore", new { id = MaSP });
        }
        private int TinhTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang!=null)
            {
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            }
            return tongSL;
        }
        private double TinhTongTien()
        {
            double tongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang!= null)
            {
                tongTien= gioHang.Sum(sp => sp.ThanhTien());
            }
            return tongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<MatHangMua> gioHang= LayGioHang();
            //Nếu giỏ hàng trống thì trả về trang chủ
            if (gioHang == null ||gioHang.Count==0)
            {
                return RedirectToAction("Index", "Bookstore");
                
            }
            ViewBag.TongSL= TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            // trả giỏ hàng về view
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();

        }
    }
}