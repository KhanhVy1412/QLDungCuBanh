using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDungCuBanh.Models
{
    public class Card_Item
    {
        public int MaSP { get; set; }
        public String TenSP { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }
    }
}