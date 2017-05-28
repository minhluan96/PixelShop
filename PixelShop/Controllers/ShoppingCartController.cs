using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        PixelShopEntities db = new PixelShopEntities();
        public ActionResult Index()
        {
            return View();
        }
        private int isExisting(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for(int i =0;i<cart.Count;i++)
            {
                if (cart[i].Sanpham.MaSP.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Delete(string id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            if (cart.Count() == 0)
                Session["cart"] = null;
        }
        public void Update(string id,int quantity)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            Item i = cart[index];
            i.Soluong = quantity; 
        }
        public void OrderNow(string id) {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.SANPHAMs.Find(id),1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if(index==-1)
                    cart.Add(new Item(db.SANPHAMs.Find(id), 1));
                else
                    cart[index].Soluong++;
                Session["cart"] = cart;
            }
        }
    }
}