using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenQuocAn.Models;
namespace NguyenQuocAn.Controllers
{
    public class GiohangController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        //Lấy giỏ hàng
        public List<ItemGioHang> LayGiohang()
        {
            List<ItemGioHang> lstGiohang = Session["Giohang"] as List<ItemGioHang>;
            if(lstGiohang==null)
            {
                lstGiohang = new List<ItemGioHang>();
                Session["Giohang"] = lstGiohang;
                return lstGiohang;
            }
            return lstGiohang;
        }
        //Thêm giỏ hàng = load lại trang
        public ActionResult ThemGiohang(int MaSP, string strURL)
        {
            //Kiểm tra
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng
            List<ItemGioHang> lstGiohang = LayGiohang();
            ItemGioHang spCheck = lstGiohang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck!=null)
            {
                if (sp.SoLuongTon <= spCheck.SoLuong)
                {
                    return View("ThongBaoHetHang");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBaoHetHang");
            }
            lstGiohang.Add(itemGH);
            return Redirect(strURL);
        }
        ///////Xóa hàng
        public ActionResult XoaHang(int MaSP, string strURL)
        {
            List<ItemGioHang> lstGiohang = LayGiohang();
            ItemGioHang spCheck = lstGiohang.SingleOrDefault(n => n.MaSP == MaSP);
            lstGiohang.Remove(spCheck);
            return Redirect(strURL);
        }
        /////Sửa giỏ hàng
        public ActionResult SuaHang(int MaSP, string strURL,bool sw)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            List<ItemGioHang> lstGiohang = LayGiohang();
            ItemGioHang spCheck = lstGiohang.SingleOrDefault(n => n.MaSP == MaSP);
            if(sw)
            {
                if (sp.SoLuongTon <= spCheck.SoLuong)
                {
                    return View("ThongBaoHetHang");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }else
            {
                if(spCheck.SoLuong==1)
                {
                    lstGiohang.Remove(spCheck);
                    return Redirect(strURL);
                }
                spCheck.SoLuong--;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }
        }
        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGiohang = Session["Giohang"] as List<ItemGioHang>;
            if(lstGiohang==null)
            {
                return 0;
            }
            return lstGiohang.Sum(n => n.SoLuong);
        }
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGiohang = Session["Giohang"] as List<ItemGioHang>;
            if (lstGiohang == null)
            {
                return 0;
            }
            return lstGiohang.Sum(n => n.ThanhTien);
        }
        // GET: Giohang
        public ActionResult XemGiohang()
        {
            List<ItemGioHang> lst = LayGiohang();
            return View(lst) ;
        }
        public ActionResult GiohangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong=0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult GhiNhanDonHang(KhachHang kh)
        {
            ThanhVien tv = (ThanhVien)Session["dangnhap"];
            if (Session["dangnhap"]!=null)
            {
                
                if(db.KhachHangs.SingleOrDefault(n=>n.MaThanhVien==tv.MaThanhVien)==null)
                {
                    KhachHang kh_tv = new KhachHang();
                    kh_tv.MaThanhVien = tv.MaThanhVien;
                    kh_tv.TenKH = tv.HoTen;
                    kh_tv.DiaChi = tv.DiaChi;
                    kh_tv.SoDienThoai = tv.SoDienThoai;
                    kh_tv.Email = tv.Email;
                    db.KhachHangs.Add(kh_tv);
                    db.SaveChanges();
                    kh.MaKH = db.KhachHangs.SingleOrDefault(n => n.MaThanhVien == tv.MaThanhVien).MaKH;
                }else
                {
                    kh.MaKH = db.KhachHangs.SingleOrDefault(n => n.MaThanhVien == tv.MaThanhVien).MaKH;
                }
            }else
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            foreach(ItemGioHang item in LayGiohang())
            {
                ChiTietDonDatHang ctddh = new ChiTietDonDatHang();
                ctddh.MaDDH = ddh.MaDDH;
                ctddh.MaSP = item.MaSP;
                ctddh.TenSP = item.TenSP;
                ctddh.SoLuong = item.SoLuong;
                ctddh.DonGia = item.ThanhTien;
                db.ChiTietDonDatHangs.Add(ctddh);
                db.SaveChanges();
            }
            Session["Giohang"] = null;
            return RedirectToAction("Index","Home");
        }

    }
}