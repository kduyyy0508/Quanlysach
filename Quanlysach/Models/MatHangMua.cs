using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlysach.Models
{
    public class MatHangMua
    {
        QLBANSACHEntities1 db = new QLBANSACHEntities1();
        public int MaSach { get; set; } 
        public string TenSach { get; set; }
        public string AnhBia { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }

        //Thành tiền
        public double ThanhTien()
        {
            return SoLuong * DonGia;
        }
//Chỉnh sửa mua được số lượng nhiều
        public MatHangMua(int MaSach)
        {
            this.MaSach = MaSach;
            //Tìm sách trong csdl
            var sach = db.SACHes.Single(s => s.Masach == this.MaSach);
            this.TenSach = sach.Tensach;
            this.AnhBia= sach.Hinhminhhoa;
            this.DonGia = double.Parse(sach.Dongia.ToString());
            this.SoLuong = 1;
        }
    }
}