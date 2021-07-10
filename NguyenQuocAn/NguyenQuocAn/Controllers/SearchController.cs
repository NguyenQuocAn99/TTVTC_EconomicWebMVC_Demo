using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NguyenQuocAn.Models;

namespace NguyenQuocAn.Controllers
{
    public class SearchController : Controller
    {
        QuanLyBanHangEntities1 ds = new QuanLyBanHangEntities1();
        // GET: Search
        [HttpGet]
        public ActionResult SearchProduct(string keySearch, int? page)
        {
            var lst = ds.SanPhams.Where(n=>n.TenSP.Contains(keySearch));
            if(lst==null)
            {
                ViewBag.CheckFind = false;
            }
            ViewBag.key = keySearch;
            ViewBag.CheckFind = true;
            int PageSize = 6;
            int PageNumber = (page ?? 1);
            return View(lst.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }
        [HttpPost]
        public ActionResult Search(string keySearch)
        {
            return RedirectToAction("SearchProduct", new { @keySearch = keySearch });
        }
    }
}