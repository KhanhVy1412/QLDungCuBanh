using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDungCuBanh.Models;
using QLDungCuBanh.Models.ViewModels;
using System.IO;

using PagedList;
using PagedList.Mvc;

namespace QLDungCuLamBanh.Controllers
{
    public class SanPhamController : Controller
    {
        private QLDungCuBanhEntities db = new QLDungCuBanhEntities();
        // GET: Phim
        private List<SanPham> LaySanPham(int count)
        {
            return db.SanPhams.OrderByDescending(s => s.NamSanXuat).Take(count).ToList();
        }

        public ActionResult Index(int? page)
        {
            /*List<Phim> dsp = db.Phims.ToList();*/
            int pageSize =6;
            int pageNum = (page ?? 1);
            var sanphammoi = (from l in db.SanPhams
                              select l).OrderByDescending(s => s.NamSanXuat);
            //var phimmoi = Layphimmoi(3);
            return View(sanphammoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Create()
        {
            //Lấy ra ds loại sp
            SanPhamViewModels pModel = new SanPhamViewModels();
            pModel.ListPhanLoai = db.PhanLoais.ToList();
            return View(pModel);
        }
        [HttpPost]
        public ActionResult Create(SanPham p, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham SanPhamM = new SanPham();
            SanPhamM.TenSP = p.TenSP;
            SanPhamM.QuocGia = p.QuocGia;
            SanPhamM.DonGia = p.DonGia;
            SanPhamM.DonViTinh = p.DonViTinh;
            SanPhamM.NamSanXuat = p.NamSanXuat;
            SanPhamM.MaPhanLoai = p.MaPhanLoai;
            //lưu đường dẫn vào database
            SanPhamM.HinhSP = "Content/image/" + fileName;
            //sanPhamMoi.HinhSP = sp.HinhSP;
            db.SanPhams.Add(SanPhamM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)//truyền id
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaPhanLoai = new SelectList
                (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham p = db.SanPhams.Where(s => s.MaSP == sanpham.MaSP).FirstOrDefault();
            p.TenSP = sanpham.TenSP;
            p.QuocGia = sanpham.QuocGia;
            p.DonGia = sanpham.DonGia;
            p.DonViTinh = sanpham.DonViTinh;
            p.MaPhanLoai = sanpham.MaPhanLoai;
            p.NamSanXuat = sanpham.NamSanXuat;
            p.HinhSP = "Content/image/" + fileName; ;
           
            /* db.SanPhams.Add(sp);*///Thêm sp
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            SanPham p = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaLoaiPhim = new SelectList
               (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(SanPham sanpham, int id)
        {
            SanPham p = db.SanPhams.Single(s => s.MaSP == id);
            db.SanPhams.Remove(p);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult PhoiLong()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Phới đánh trứng-Cán bột").ToList();

            return View(p);
        }
        public ActionResult RayBot()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Rây bột-Ca đong-Muỗng đo lường").ToList();

            return View(p);
        }
        public ActionResult TuiBatKem()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Đuôi bắt kem-Túi bắt kem").ToList();

            return View(p);
        }
        public ActionResult KhuonNhom()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Khuôn Nhôm").ToList();

            return View(p);
        }
        public ActionResult KhuonSilicon()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Khuôn silicon").ToList();

            return View(p);
        }
        public ActionResult KhuonGiay()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Khuôn giấy").ToList();

            return View(p);
        }
        public ActionResult DuongBot()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Đường-Bột").ToList();

            return View(p);
        }
        public ActionResult Whipping()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Whipping-Topping kem bơ sữa").ToList();

            return View(p);
        }
        public ActionResult HuongLieu()
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Hương liệu-Rượu").ToList();

            return View(p);
        }
        public ActionResult SearchByName(string name)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.TenSP.Contains(name)).ToList();

            ViewBag.keyword = name;

            return View(p);
        }
    }
}