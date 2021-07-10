using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenQuocAn.Models;

namespace NguyenQuocAn.Controllers
{
   
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: QuanLySanPham
        [HttpGet]
        public ActionResult Index()
        {
            var lst = db.SanPhams;

            return View(lst);
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX","TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp, HttpPostedFileBase HinhAnh)
        {
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.MaNCC), "MaNCC", "TenNCC");
            if(HinhAnh.ContentLength>0)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                if(System.IO.File.Exists(path))
                {
                    ViewBag.upload = "hinh da tồn tại";
                    return View();
                }
                else
                {
                    HinhAnh.SaveAs(path);
                    path =CutString(path);
                    sp.HinhAnh = path;
                }
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("TaoMoi");
        }
        public String CutString(String tem)
        {
            String kq = "";
            for(int i=tem.Length-1;i>=0;i--)
            {
                if(tem[i]=='\\')
                {
                    break;
                }
                else
                {
                    kq = tem[i] + kq;
                }
               
            }
            return kq;
        }
    }
}