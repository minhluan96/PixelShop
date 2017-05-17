using PixelShop.Models;
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
        public ActionResult Order()
        {
            

            return View(@"~/Views/Admin/Order.cshtml");
        }
        public ActionResult ViewOrder()
        {
            return View(@"~/Views/Admin/ViewOrder.cshtml");
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
        public ActionResult Customer()
        {
            return View(@"~/Views/Admin/Customer.cshtml");
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

        public ActionResult ProductUpdate(string masp, string tensp, string motasp, string tendm, int slg, int giaban, 
                                    string[] imgSP, string imgKey, string maNSX, HttpPostedFileBase image)
        {
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = image.FileName;
                    var filePathOriginal = Server.MapPath("/images/");
                    string savedFileName = Path.Combine(filePathOriginal, fileName);
                    SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp)).Single();

                    sp.HINHANHs.Add(new HINHANH() { MaSP = sp.MaSP, PathHinhAnh = savedFileName });

                    if (!string.IsNullOrEmpty(tensp) && giaban >= 0 && slg >= 0)
                    {
                        
                        if (sp != null)
                        {
                            sp.TenSP = tensp;
                            sp.MoTa = motasp;
                            sp.GiaBan = giaban;
                            sp.DanhMuc = tendm;
                            sp.NhaSanXuat = maNSX;
                            sp.HinhHienThi = imgKey;
                            sp.SoLuongTon = slg;
                        }
                    }

                    db.SaveChanges();

                }
                return RedirectToAction("EditProduct", "Admin", masp);
            }
            else
            {
                if (!string.IsNullOrEmpty(tensp) && giaban >= 0 && slg >= 0)
                {
                    updateInfoProduct(masp, tensp, motasp, tendm, slg, giaban, imgSP, imgKey, maNSX);
                }
            }
            return RedirectToAction("Product", "Admin");
        }

        private void updateInfoProduct(string masp, string tensp, string motasp, string tendm, int slg, int giaban,
                                    string[] imgSP, string imgKey, string maNSX)
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
                sp.HINHANHs.Clear();
                for (int i = 0; i < imgSP.Count(); i++)
                {
                    sp.HINHANHs.Add(new HINHANH() { MaSP = sp.MaSP, PathHinhAnh = imgSP[i] });
                }
                sp.SoLuongTon = slg;

                db.SaveChanges();
            }
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

        public ActionResult ProductCreate(string tensp, string motasp, string tendm, int slg, int giaban,
                                    string[] imgSP, string imgKey, string maNSX)
        {
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