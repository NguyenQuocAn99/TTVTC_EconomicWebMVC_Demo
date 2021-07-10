using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenQuocAn.Models;
using PagedList;
namespace NguyenQuocAn.Controllers
{
    public class ProductController : Controller
    {
        QuanLyBanHangEntities1 db = new QuanLyBanHangEntities1();
        // GET: Product
        public ActionResult SanPham(int? MaLoaiSP, int MaNSX, int? page)
        {
            if(MaLoaiSP==null || MaNSX == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var listSP = db.SanPhams.Where(n=>n.MaLoaiSP == MaLoaiSP&&n.MaNSX==MaNSX);
            ViewBag.loaiSP = db.LoaiSanPhams.Find(MaLoaiSP).TenLoai;
            ViewBag.tenNSX = db.NhaSanXuats.Find(MaNSX).TenNSX;
            if(listSP.Count()==0)
            {
                return HttpNotFound();
            }
            ///////Phân trang
            int PageSize = 3;
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(listSP.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }
        public ActionResult ChiTietSanPham(int MaSP)
        {
            ViewBag.MaSP = db.SanPhams.Find(MaSP);
            return View();
        }
    }
}