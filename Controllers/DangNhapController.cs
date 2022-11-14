using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDungCuBanh.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection fDN)
        {
            string DN = fDN["DN"];
            string MK = fDN["MK"];
            {
                if (DN == "Admin" && MK == "123")
                    return RedirectToAction("Index", "SanPham");
                else
                    //ViewBag.ThongBao = "Thất bại";
                    return RedirectToAction("DangNhap", "DangNhap");
            }
            return View();
        }
    }
}