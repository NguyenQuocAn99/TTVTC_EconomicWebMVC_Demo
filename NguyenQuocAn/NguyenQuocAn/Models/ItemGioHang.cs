﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenQuocAn.Models
{
    public class ItemGioHang
    {
        public string TenSP { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set;}
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public ItemGioHang(int iMaSP)
        {
            using (QuanLyBanHangEntities1 db= new QuanLyBanHangEntities1())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGIa.Value;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;

            }
            
        }
        public ItemGioHang(int iMaSP, int iSoLuong)
        {
            using (QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGIa.Value;
                this.SoLuong = iSoLuong;
                this.ThanhTien = DonGia * SoLuong;

            }

        }
    }
}