using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenQuocAn.Models;

namespace NguyenQuocAn.Controllers
{
    public class HomeController : Controller
    {
            QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
     
        public ActionResult Index()
        {
            Session["language"] = "En";
            var listDTM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.DaXoa == false);
            var listTL = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.DaXoa == false);
            var listLT = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.DaXoa == false);
            ViewBag.LT = listLT;
            ViewBag.TL = listTL;
            ViewBag.DTM = listDTM;

            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult sanphampartialview1()
        {
            return PartialView();
        }
        public ActionResult SanPhamPartialView2()
        {

            return PartialView();
        }
        public ActionResult SanPhamPartialView3()
        {
            return PartialView();
        }
        public ActionResult DangkyPartialView()
        {
            return PartialView();
        }
        public ActionResult MenuPartialView()
        {

            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
        [HttpGet]
        public ActionResult DangKyThanhVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKyThanhVien(ThanhVien tv, FormCollection f)
        {
            if(ModelState.IsValid)
            {
                
                if (tv.MatKhau == f["confirmMK"])
                {
                    if (db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == tv.TaiKhoan) == null && db.ThanhViens.SingleOrDefault(n => n.SoDienThoai == tv.SoDienThoai) == null)
                    {
          
                        db.ThanhViens.Add(tv);
                        db.SaveChanges();
                        return Content("<script>window.location.reload();</script>");
                    }
                    else
                    {
                        return Content("UserName or phonenumber has already existed!");
                    }
                }
                else
                {
                    return Content("Confirm password fails!");
                }
            }

            return Content("Đăng ký thất bại");
        }

        public ActionResult DangNhap(FormCollection f)
        {
            string name = f["Name"];
            string pass = f["Password"];
            ThanhVien temp = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == name && n.MatKhau == pass);
            if (temp != null)
            {
                Session.Add("dangnhap", temp);
      
                    return RedirectToAction("Index");
              
            }
            return Content("Email or password is incorrect!");
        }
        public ActionResult DangXuat(string strURL)
        {
            Session["dangnhap"] = null;
            return Redirect(strURL);
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Test1()
        {
            return View();
        }
        public ActionResult SwLanguage(string strURL, string swToLanguage)
        {
            if(Session["language"]==null)
            {
                Session.Add("language", "En");
                return Redirect(strURL);
                
            }
            Session.Add("language", swToLanguage);
            return Redirect(strURL);
        }
    }
}