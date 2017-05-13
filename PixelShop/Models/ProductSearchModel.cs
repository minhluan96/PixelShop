using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelShop.Models
{
    public class ProductSearchModel
    {
        public string DanhMuc { get; set; }
        public string NhaSanXuat { get; set; }
        public int? GiaTu { get; set; }
        public int? GiaDen { get; set; }
        public string Ten { get; set; }
    }
}