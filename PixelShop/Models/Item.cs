using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelShop.Models
{
    public class Item
    {
        private SANPHAM sanpham = new SANPHAM();
        private int soluong;
        public Item()
        {

        }
        public Item(SANPHAM sanpham,int soluong)
        {
            this.Sanpham = sanpham;
            this.Soluong = soluong;
        }

        public SANPHAM Sanpham
        {
            get
            {
                return sanpham;
            }

            set
            {
                sanpham = value;
            }
        }

        public int Soluong
        {
            get
            {
                return soluong;
            }

            set
            {
                soluong = value;
            }
        }
    }
}