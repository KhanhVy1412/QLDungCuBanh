using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDungCuBanh.Models;
using System.Net.Mail;
using System.Net;

namespace doan.Controllers
{
    public class GioHangController : Controller
    {
        private QLDungCuBanhEntities db = new QLDungCuBanhEntities();
        public ActionResult Index()
        {
            List<Card_Item> giohang = Session["giohang"] as List<Card_Item>;
            return View(giohang);
        }

        public RedirectToRouteResult AddToCart(int Masp)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<Card_Item>();
            }
            List<Card_Item> giohang = Session["giohang"] as List<Card_Item>;
            if (giohang.FirstOrDefault(m => m.MaSP == Masp) == null)
            {
                SanPham sp = db.SanPhams.Find(Masp);
                Card_Item newItem = new Card_Item();
                newItem.MaSP = Masp;
                newItem.TenSP = sp.TenSP;
                newItem.SoLuong = 1;
                newItem.DonGia = Convert.ToInt32(sp.DonGia);
                giohang.Add(newItem);
            }
            else
            {
                Card_Item cardItem = giohang.FirstOrDefault(m => m.MaSP == Masp);
                cardItem.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Update(int MaSP, int txtSoluong)
        {
            List<Card_Item> giohang = Session["giohang"] as List<Card_Item>;
            Card_Item itemSua = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (itemSua != null)
            {
                itemSua.SoLuong = txtSoluong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(int MaSP)
        {
            List<Card_Item> giohang = Session["giohang"] as List<Card_Item>;
            Card_Item itemXoa = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Order(string Email, string Phone)
        {
            List<Card_Item> giohang = Session["giohang"] as List<Card_Item>;
            string sMsg = "<html><body><table border='1'><tr><th>STT</th><th>Tên hàng</th><th>Sốlượng </ th >< th > Đơn giá </ th >< th > Thành tiền </ th ></ tr > ";
            int i = 0;
            double tongtien = 0;
            foreach (Card_Item item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.TenSP + "</td>";
                sMsg += "<td>" + item.SoLuong.ToString() + "</td>";
                sMsg += "<td>" + item.DonGia.ToString() + "</td>";
                sMsg += "<td>" + String.Format("{0:0,000}", item.SoLuong * item.DonGia) + "</td>";
                sMsg += "<tr>";
                tongtien += (item.SoLuong * item.DonGia);
            }
            sMsg += "<tr><th colspan='5'>Tổng cộng: " + String.Format("{0:0,000 vnđ}", tongtien)
            + "</th></tr></table>";
            MailMessage mail = new MailMessage("email@gmail.com", Email, "Thông tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("username", "password");
            mail.IsBodyHtml = true;
            Session["GioHang"] = null;
            return View("Finish");
        }
        [HttpPost]
        public ActionResult Finish()
        {
            Session["GioHang"] = null;
            return RedirectToAction("Index");
        }
    }

}

