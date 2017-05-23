﻿using PixelShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace PixelShop.Controllers
{
    public class AdminController : Controller
    {

        PixelShopEntities db = new PixelShopEntities();


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order(int ?page)
        {
            List<DONHANG> lstDH = db.DONHANGs.OrderBy(c => c.TinhTrang).Select(c => c).ToList<DONHANG>();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(@"~/Views/Admin/Order.cshtml", lstDH.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewOrder(string madh)
        {
            DONHANG dh = db.DONHANGs.Where(c => c.MaDH.Equals(madh)).Single();
            return View(@"~/Views/Admin/ViewOrder.cshtml",dh);
        }
        public ActionResult EditProduct(string masp)
        {
            List<DANHMUC> lstDM = db.DANHMUCs.Where(c => c.BiXoa == 0).Select(c => c).ToList<DANHMUC>();
            List<NHASANXUAT> lstNSX = db.NHASANXUATs.Where(m => m.BiXoa == 0).Select(m => m).ToList<NHASANXUAT>();

            ViewData["dsNhaSanXuat"] = lstNSX;
            ViewData["dsDanhMuc"] = lstDM;
            SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp)).Select(p => p).Single();
            
            return View(@"~/Views/Admin/EditProduct.cshtml", sp);
        }
        public ActionResult Customer(int ?page)
        {
            List<TAIKHOAN> lstDM = db.TAIKHOANs.OrderBy(c => c.BiXoa).Select(c => c).ToList<TAIKHOAN>();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(@"~/Views/Admin/Customer.cshtml", lstDM.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AddProduct()
        {
            List<DANHMUC> lstDM = db.DANHMUCs.Where(c => c.BiXoa == 0).Select(c => c).ToList<DANHMUC>();
            List<NHASANXUAT> lstNSX = db.NHASANXUATs.Where(m => m.BiXoa == 0).Select(m => m).ToList<NHASANXUAT>();

            ViewData["dsNhaSanXuat"] = lstNSX;
            ViewData["dsDanhMuc"] = lstDM;

            return View(@"~/Views/Admin/AddProduct.cshtml");
        }
        public ActionResult Product(int ?page)
        {
            List<SANPHAM> lstSP = db.SANPHAMs.OrderBy(p => p.BiXoa).ThenBy(p => p.DANHMUC1.BiXoa).ThenBy(p => p.NHASANXUAT1.BiXoa).Select(p => p).ToList<SANPHAM>();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(@"~/Views/Admin/Product.cshtml", lstSP.ToPagedList(pageNumber, pageSize));
        }

        
        public ActionResult Category(int? page)
        {
            List<DANHMUC> lstDM = db.DANHMUCs.OrderBy(c => c.BiXoa).Select(c => c).ToList<DANHMUC>();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(@"~/Views/Admin/Category.cshtml", lstDM.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Manufacturer(int ?page)
        {

            List<NHASANXUAT> lstNSX = db.NHASANXUATs.OrderBy(n => n.BiXoa).Select(n => n).ToList<NHASANXUAT>();
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(@"~/Views/Admin/Manufacturer.cshtml", lstNSX.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ManufacturerUpdate(string tennsx, string mansx)
        {

            if(!string.IsNullOrEmpty(tennsx))
            {
                NHASANXUAT nsx = db.NHASANXUATs.Where(n => n.MaNSX.Equals(mansx)).Single();
                if(nsx != null)
                {
                    nsx.TenNSX = tennsx;
                    //update mo ta - db dang thieu

                    db.SaveChanges();
                }
            }
            return RedirectToAction("Manufacturer", "Admin");
        }
        [HttpPost]
        public void UpdateImageProduct(string masp)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp)).Single();
                    for (int i = 0; i < files.Count; i++)
                    {
                        int count = sp.HINHANHs.Count();
                        var fileName = sp.MaSP + "_" + count + "." + files[i].FileName.Split('.')[1];
                        files[i].SaveAs(Server.MapPath("~/ImgProduct/" + fileName));
                        var filePathOriginal = "../ImgProduct/";
                        string savedFileName = Path.Combine(filePathOriginal, fileName);

                        sp.HINHANHs.Add(new HINHANH() { MaSP = sp.MaSP, PathHinhAnh = savedFileName });
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }
        }
        [HttpPost]
        public ActionResult ProductUpdate(string masp, string tensp, string motasp, string tendm, int slg, int giaban, 
                                    string[] imgSP, string[] statusimgSP, string imgKey, string maNSX)
        {

            if (!string.IsNullOrEmpty(tensp) && giaban >= 0 && slg >= 0)
           {
                SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp) && p.BiXoa == 0).Single();
                if (sp != null)
                {
                    sp.TenSP = tensp;
                    sp.MoTa = motasp;
                    sp.GiaBan = giaban;
                    sp.DanhMuc = tendm;
                    sp.NhaSanXuat = maNSX;
                    sp.HinhHienThi = imgKey;
                    sp.SoLuongTon = slg;

                    db.SaveChanges();
                }
            }
            if (Request.Files.Count > 0)
            {
                UpdateImageProduct(masp);
            }
            return RedirectToAction("Product", "Admin");
        }
      
        [HttpPost]
        public ActionResult CategoryUpdate(string madm, string tendm, int cbbMaNdm)
        {
            if (!string.IsNullOrEmpty(tendm))
            {
                DANHMUC dm = db.DANHMUCs.Where(n => n.MaDanhMuc.Equals(madm) && n.BiXoa == 0).Single();
                if (dm != null)
                {
                    dm.TenDanhMuc = tendm;
                    dm.NhomDanhMuc = cbbMaNdm;
                    //update mo ta - db dang thieu

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult ProductCreate(FormCollection frm)
        {
            //string tensp, string motasp, string tendm, int slg, int giaban,
            //                        string[] imgSP, string imgKey, string maNSX

            string tensp = Convert.ToString(frm["tensp"]);
            string mota = Convert.ToString(frm["motasp"]);
            string tendm = Convert.ToString(frm["tendm"]);
            int slg = Convert.ToInt32(frm["slg"]);
            int giaban = Convert.ToInt32(frm["giaban"]);
            string maNSX = Convert.ToString(frm["maNSX"]);

            if(tensp != null && slg >= 0 && giaban >= 0 && tendm != null) {
                int solgSP = db.SANPHAMs.Count() + 1;
                string maSP = "SP";
                if(solgSP < 10)
                {
                    maSP += "00" + solgSP;
                }
                else
                {
                    maSP += "0" + solgSP;
                }
                int n = db.SANPHAMs.Where(c => c.MaSP.Equals(maSP)).Count();
                SANPHAM sp = new SANPHAM
                {
                    MaSP = maSP,
                    TenSP = tensp,
                    GiaBan = giaban,
                    HinhHienThi = null,
                    SoLuongTon = solgSP,
                    MoTa = mota,
                    DanhMuc = tendm,
                    NhaSanXuat = maNSX,
                    SoLuongBan = 0,
                    SoLuotXem = 0,
                    BiXoa = 0
                };
                if(n <= 0)
                {
                    db.SANPHAMs.Add(sp);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult CategoryCreate(string tendm, int cbbMaNdm)
        {
            if (!string.IsNullOrEmpty(tendm))
            {
                int solg = db.DANHMUCs.Count() + 1;
                string madm = "DM";
                if(solg < 10)
                {
                    madm += "0" + solg;

                }
                else
                {
                    madm += solg;
                }
                int n = db.DANHMUCs.Where(c => c.MaDanhMuc.Equals(madm)).Count();
                DANHMUC dm = new DANHMUC { MaDanhMuc = madm, TenDanhMuc = tendm, BiXoa = 0, NhomDanhMuc = cbbMaNdm };
                if(n  <= 0)
                {
                    db.DANHMUCs.Add(dm);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Category", "Admin");
        }
        

        [HttpPost]
        public ActionResult ManufacturerCreate(string tennsx, string mota)
        {
            if (!string.IsNullOrEmpty(tennsx))
            {
                int solg = db.NHASANXUATs.Count() + 1;
                string mansx = "NSX";
                if(solg < 10)
                {
                    mansx += "0" + solg;
                }
                else
                {
                    mansx += solg;
                }
                int n = db.NHASANXUATs.Where(m => m.MaNSX.Equals(mansx)).Count();
                NHASANXUAT nsx = new NHASANXUAT { MaNSX = mansx, TenNSX = tennsx, BiXoa = 0 };
                if(n <= 0)
                {
                    db.NHASANXUATs.Add(nsx);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Manufacturer", "Admin");
        }

        public ActionResult ProductDelete(string masp)
        {
            int n = db.SANPHAMs.Where(p => p.MaSP.Equals(masp) && p.BiXoa == 0).Count();
            if(n  > 0)
            {
                SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp) && p.BiXoa == 0).Single();
                sp.BiXoa = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public ActionResult CategoryDelete(string madm)
        {
            int n = db.DANHMUCs.Where(c => c.MaDanhMuc.Equals(madm) && c.BiXoa == 0).Count();
            if(n > 0)
            {
                DANHMUC dm = db.DANHMUCs.Where(c => c.MaDanhMuc.Equals(madm) && c.BiXoa == 0).Single();
                dm.BiXoa = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Category", "Admin");
        }
        public ActionResult CustomerDelete(string taikhoan)
        {
            int n = db.TAIKHOANs.Where(c => c.Email.Equals(taikhoan) && c.BiXoa == 0).Count();
            if (n > 0)
            {
                TAIKHOAN tk = db.TAIKHOANs.Where(c => c.Email.Equals(taikhoan) && c.BiXoa == 0).Single();
                tk.BiXoa = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Customer", "Admin");
        }
        [HttpPost]
        public ActionResult ManufacturerDelete(string mansx)
        {
            int n = db.NHASANXUATs.Where(m => m.MaNSX.Equals(mansx) && m.BiXoa == 0).Count();
            if(n > 0)
            {
                NHASANXUAT nsx = db.NHASANXUATs.Where(m => m.MaNSX.Equals(mansx) && m.BiXoa == 0).Single();
                nsx.BiXoa = 1;
                db.SaveChanges();
            }

            return RedirectToAction("Manufacturer", "Admin");
        }

        


        public PartialViewResult Header()
        {
            return PartialView(@"~/Views/Admin/Header.cshtml");
        }
        public PartialViewResult Sidebar()
        {
            return PartialView(@"~/Views/Admin/Sidebar.cshtml");
        }
        public PartialViewResult Footer()
        {
            return PartialView(@"~/Views/Admin/Footer.cshtml");
        }
        
    }
}