using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelShop.Models
{
    public class UserOrder
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime? dateOrder { get; set; }
        public string sum { get; set; }

        public int? idStatus { get; set; }
        public string status { get; set; }
    }
}