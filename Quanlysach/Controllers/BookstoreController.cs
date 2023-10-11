using Quanlysach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quanlysach.Controllers
{
    public class BookstoreController : Controller
    {
        QLBANSACHEntities1 database= new QLBANSACHEntities1();
        private List<SACH> LaySachMoi(int soluong)
        {
            //Sx theo ngày cập nhập giảm dần, lấy theo biến soluong. Chuyen sang dang danh sach
            return database.SACHes.OrderByDescending(sach => sach.Ngaycapnhat).Take(soluong).ToList();
        }
        // GET: Bookstore
        public ActionResult Index()
        {
            var dsSachMoi = LaySachMoi(13);
            return View(dsSachMoi);
        }
        public ActionResult LayChuDe()
        {
            var dsChuDe = database.CHUDEs.ToList();
            return PartialView(dsChuDe);
        }
        public ActionResult LayNhaXuatBan()
        {
            var dsNhaXB = database.NHAXUATBANs.ToList();
            return PartialView(dsNhaXB);
        }
    }
}