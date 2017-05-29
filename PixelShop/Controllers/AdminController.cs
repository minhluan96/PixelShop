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
            List<DONHANG> dsDH = db.DONHANGs.Where(d => d.TinhTrang == 0).ToList();
            double? tongDT = 0;
            foreach(DONHANG dh in dsDH)
            {
                foreach(CHITIETDONHANG ct in dh.CHITIETDONHANGs)
                {
                    tongDT += ct.SoLuongDat * ct.GiaBan * 1.0;
                }
            }

            int? tongdh = db.DONHANGs.Where(d => d.TinhTrang != -1).ToList().Count;
            ViewData["tongdh"] = tongdh;
            ViewData["doanhthu"] = tongDT;

            return View();
        }

        public ActionResult Order(int ?page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            List<TINHTRANGDONHANG> lstTT = db.TINHTRANGDONHANGs.ToList();
            ViewData["lstTT"] = lstTT;
           if (TempData["dsdh"] == null)
            {
                List<DONHANG> lstDH = db.DONHANGs.OrderBy(c => c.TinhTrang).Select(c => c).ToList<DONHANG>();
                return View(@"~/Views/Admin/Order.cshtml", lstDH.ToPagedList(pageNumber, pageSize));
            }
            List<DONHANG> lstTimKiemDH = TempData["dsdh"] as List<DONHANG>;
            var temp = lstTimKiemDH;
            TempData.Keep();
            return View(@"~/Views/Admin/Order.cshtml", lstTimKiemDH.ToPagedList(pageNumber, pageSize));
            
            
        }


        [HttpPost]
        public ActionResult CapNhatDonHang(FormCollection frm)
        {
            if (!String.IsNullOrEmpty(frm["tinhtrang"]))
            {
                int matinhtrang = Int32.Parse(frm["tinhtrang"]);
                string madh = frm["madh"];
                DONHANG dh = db.DONHANGs.Where(d => d.MaDH.Equals(madh)).SingleOrDefault();
                if(dh != null)
                {
                    dh.TinhTrang = matinhtrang;
                    if(matinhtrang == -1)
                    {
                        List<CHITIETDONHANG> lstCT = dh.CHITIETDONHANGs.ToList();
                        for(int i = 0; i < lstCT.Count; i++)
                        {
                            CHITIETDONHANG ct = lstCT.ElementAt(i);
                            SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(ct.MaSP)).SingleOrDefault();
                            if(sp != null)
                            {
                                sp.SoLuongTon += lstCT.ElementAt(i).SoLuongDat;
                                sp.SoLuongBan -= lstCT.ElementAt(i).SoLuongDat;
                            }
                        }

                    }
                    if(matinhtrang == 2)
                    {
                        List<CHITIETDONHANG> lstCT = dh.CHITIETDONHANGs.ToList();
                        for (int i = 0; i < lstCT.Count; i++)
                        {
                            CHITIETDONHANG ct = lstCT.ElementAt(i);
                            SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(ct.MaSP)).SingleOrDefault();
                            if (sp != null)
                            {
                                sp.SoLuongTon -= lstCT.ElementAt(i).SoLuongDat;
                                sp.SoLuongBan += lstCT.ElementAt(i).SoLuongDat;
                            }
                        }
                    }

                    if(matinhtrang == 0)
                    {
                        dh.NgayGiao = DateTime.Now;
                    }
                    db.SaveChanges();
                }
            }            
            return RedirectToAction("Order", "Admin");
        }
        public ActionResult ViewOrder(string madh)
        {
            List<TINHTRANGDONHANG> lstTT = db.TINHTRANGDONHANGs.ToList();
            ViewData["lstTT"] = lstTT;

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
            TempData["UserMessage"] = new Message { CssClassName = "alert-success", Title = "Thành công!", MessageAlert = "Đã thực hiện thành công." };
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (TempData["dstk"] == null)
            {
                List<TAIKHOAN> lstTK = db.TAIKHOANs.OrderBy(c => c.BiXoa).Select(c => c).ToList<TAIKHOAN>();
                return View(@"~/Views/Admin/Customer.cshtml", lstTK.ToPagedList(pageNumber, pageSize));
            }

            List<TAIKHOAN> lstTimKiemTK = TempData["dstk"] as List<TAIKHOAN>;
            TempData.Keep();
            return View(@"~/Views/Admin/Customer.cshtml", lstTimKiemTK.ToPagedList(pageNumber, pageSize)); 
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
            TempData["UserMessage"] = new Message { CssClassName = "alert-danger", Title = "Thất bại!", MessageAlert = "Vui lòng kiểm tra lại." };
            List<DANHMUC> lstDM = db.DANHMUCs.Where(c => c.BiXoa == 0).Select(c => c).ToList<DANHMUC>();
            List<NHASANXUAT> lstNSX = db.NHASANXUATs.Where(m => m.BiXoa == 0).Select(m => m).ToList<NHASANXUAT>();

            ViewData["dsNhaSanXuat"] = lstNSX;
            ViewData["dsDanhMuc"] = lstDM;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (TempData["dssp"] == null)
            {
                List<SANPHAM> lstSP = db.SANPHAMs.OrderBy(p => p.BiXoa).ThenBy(p => p.DANHMUC1.BiXoa).ThenBy(p => p.NHASANXUAT1.BiXoa).Select(p => p).ToList<SANPHAM>();
                return View(@"~/Views/Admin/Product.cshtml", lstSP.ToPagedList(pageNumber, pageSize));
            }
            List<SANPHAM> lstTimKiemSP = TempData["dssp"] as List<SANPHAM>;
            TempData.Keep();
            return View(@"~/Views/Admin/Product.cshtml", lstTimKiemSP.ToPagedList(pageNumber, pageSize));
        }

      
        public ActionResult Category(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (TempData["dsdm"] == null)
            {
                List<DANHMUC> lstDM = db.DANHMUCs.OrderBy(c => c.BiXoa).Select(c => c).ToList<DANHMUC>();  
                return View(@"~/Views/Admin/Category.cshtml", lstDM.ToPagedList(pageNumber, pageSize));
            }
            List<DANHMUC> lstTimKiemDM = TempData["dsdm"] as List<DANHMUC>;
            TempData.Keep();
            return View(@"~/Views/Admin/Category.cshtml", lstTimKiemDM.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Manufacturer(int ?page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

           

            if (TempData["dsnsx"] == null)
            {

                List<NHASANXUAT> lstNSX = db.NHASANXUATs.OrderBy(n => n.BiXoa).Select(n => n).ToList<NHASANXUAT>();
                return View(@"~/Views/Admin/Manufacturer.cshtml", lstNSX.ToPagedList(pageNumber, pageSize));
            }
            List<NHASANXUAT> lstTimKiemNSX = TempData["dsnsx"] as List<NHASANXUAT>;
            TempData.Keep();
            return View(@"~/Views/Admin/Manufacturer.cshtml", lstTimKiemNSX.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult TimKiemSP(FormCollection fm)
        {
            IQueryable<SANPHAM> q = db.SANPHAMs;
            
            if (!string.IsNullOrEmpty(fm["product_name"].ToString()))
            {
                string email = fm["product_name"].ToLower();
                q = q.Where(d => d.TenSP.ToLower().Contains(email));
            }
            int product_price_from;
            int product_quantity_to;
            int product_price_to;
            int product_quantity_from;
            if (!string.IsNullOrEmpty(fm["product_price_from"].ToString()) && !string.IsNullOrEmpty(fm["product_price_to"].ToString()))
            {
                product_price_from = Int32.Parse(fm["product_price_from"].ToString());
                product_price_to = Int32.Parse(fm["product_price_to"].ToString());
                q = q.Where(d => d.GiaBan<=product_price_to && d.GiaBan>=product_price_from);
            }
            if (!string.IsNullOrEmpty(fm["product_quantity_to"].ToString()) && !string.IsNullOrEmpty(fm["product_quantity_from"].ToString()))
            {
                product_quantity_to = Int32.Parse(fm["product_quantity_to"].ToString());
                product_quantity_from = Int32.Parse(fm["product_quantity_from"].ToString());
                q = q.Where(d => d.SoLuongTon<= product_quantity_to && d.SoLuongTon>= product_quantity_from);
            }
            if (!string.IsNullOrEmpty(fm["product_producer"].ToString()))
            {
                string value = fm["product_producer"].ToString();
                if (!value.Equals("-1") )
                {
                    q = q.Where(d => d.NhaSanXuat.Equals(value));
                }
            }
            if (!string.IsNullOrEmpty(fm["product_category"].ToString()))
            {
                string value = fm["product_category"].ToString();
                if (!value.Equals("-1"))
                {
                    q = q.Where(d => d.DanhMuc.Equals(value));
                }
            }
            if (!string.IsNullOrEmpty(fm["product_status"].ToString()))
            {
                int value = Int32.Parse(fm["product_status"].ToString());
                if (value != 2)
                {
                    q = q.Where(d => d.BiXoa == value);
                }
            }

            List<SANPHAM> lstSP = q.ToList();
            TempData["dssp"] = lstSP;

            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public ActionResult TimKiemKH(FormCollection fm)
        {
            IQueryable<TAIKHOAN> q = db.TAIKHOANs;
            if (!string.IsNullOrEmpty(fm["email"].ToString()))
            {
                string email = fm["email"].ToLower();
                q = q.Where(d => d.Email.ToLower().Contains(email));
            }

            if (!string.IsNullOrEmpty(fm["hoten"].ToString()))
            {
                string hoten = fm["hoten"].ToLower();
                q = q.Where(d => d.HoTen.ToLower().Contains(hoten));
            }
            if (!string.IsNullOrEmpty(fm["quyenhan"].ToString()))
            {
                int value = Int32.Parse(fm["quyenhan"].ToString());
                if (value != 2)
                {
                    q = q.Where(d => d.QuyenHan == value);
                }
            }
            if (!string.IsNullOrEmpty(fm["trangthai"].ToString()))
            {
                int value = Int32.Parse(fm["trangthai"].ToString());
                if (value != 2)
                {
                    q = q.Where(d => d.BiXoa == value);
                }
            }

            List<TAIKHOAN> lstTK = q.ToList();
            TempData["dstk"] = lstTK;

            return RedirectToAction("Customer", "Admin");
        }

        [HttpPost]
        public ActionResult TimKiemDM(FormCollection fm)
        {
            IQueryable<DANHMUC> q = db.DANHMUCs;
            if (!string.IsNullOrEmpty(fm["madm"].ToString()))
            {
                string madm = fm["madm"];
                q = q.Where(d => d.MaDanhMuc.Equals(madm));
            }

            if (!string.IsNullOrEmpty(fm["tendm"].ToString()))
            {
                string tendm = fm["tendm"].ToLower();
                q = q.Where(d => d.TenDanhMuc.ToLower().Contains(tendm));
            }
            if (!string.IsNullOrEmpty(fm["nhomdm"].ToString()))
            {
                int value = Int32.Parse(fm["nhomdm"].ToString());
                if (value != 0)
                {
                    q = q.Where(d => d.NhomDanhMuc == value);
                }
            }
            if (!string.IsNullOrEmpty(fm["trangthai"].ToString()))
            {
                int value = Int32.Parse(fm["trangthai"].ToString());
                if (value != 2)
                {
                    q = q.Where(d => d.BiXoa == value);
                }
            }

            List<DANHMUC> lstDM = q.ToList();
            TempData["dsdm"] = lstDM;

            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public ActionResult TimKiemNSX(FormCollection fm)
        {
            IQueryable<NHASANXUAT> q = db.NHASANXUATs;
            if (!string.IsNullOrEmpty(fm["mansx"].ToString()))
            {
                string maNSX = fm["mansx"];
                q = q.Where(d => d.MaNSX.Equals(maNSX));
            }

            if (!string.IsNullOrEmpty(fm["tennsx"].ToString()))
            {
                string tennsx = fm["tennsx"].ToLower();
                q = q.Where(d => d.TenNSX.ToLower().Contains(tennsx));
            }
            if (!string.IsNullOrEmpty(fm["trangthai"].ToString()))
            {
                int value = Int32.Parse(fm["trangthai"].ToString());
                if(value != 2)
                {
                    q = q.Where(d => d.BiXoa == value);
                }
            }

            List<NHASANXUAT> lstNSX = q.ToList();
            TempData["dsnsx"] = lstNSX;

            return RedirectToAction("Manufacturer", "Admin");
        }

        [HttpPost]
        public ActionResult TimKiemDH(FormCollection frmC)
        {
            IQueryable<DONHANG> q = db.DONHANGs;
            if(!String.IsNullOrEmpty(frmC["madh"].ToString()))
            {
                String madh = frmC["madh"];

                q = q.Where(d => d.MaDH == madh);
            }
            if (!String.IsNullOrEmpty(frmC["tenkh"].ToString()))
            {
                String tenkh = frmC["tenkh"];

                q = q.Where(d => d.EmailDat.Contains(tenkh));
            }
            if(!String.IsNullOrEmpty(frmC["diachigiao"].ToString()))
            {
                String diachigiao = frmC["diachigiao"];

                q = q.Where(d => d.DiaChiGiao.Contains(diachigiao));
            }
            if(!String.IsNullOrEmpty(frmC["ngaydat"].ToString()))
            {
                String ngayDat = frmC["ngaydat"];
                DateTime ngayDatDate;
                if(DateTime.TryParse(ngayDat, out ngayDatDate))
                {
                    ngayDat = ngayDatDate.ToShortDateString();
                }
                if (ngayDatDate != null)
                {
                    q = q.Where(d => d.NgayDate.ToString().Equals(ngayDat));
                }
            }
            
            if(!String.IsNullOrEmpty(frmC["sdt"].ToString()))
            {
                String sdt = frmC["sdt"];

                q = q.Where(d => d.SDTNhan.Equals(sdt));
            }
            if(!String.IsNullOrEmpty(frmC["trangthai"].ToString()))
            {
                String trangthai = frmC["trangthai"];
                if (!trangthai.Equals("Tất cả"))
                {
                    
                    q = q.Where(d => d.TINHTRANGDONHANG.MaTinhTrang.Equals(trangthai));
                }
                
            }

            List<DONHANG> dsDH = q.ToList<DONHANG>();
            TempData["dsdh"] = dsDH;

            return RedirectToAction("Order", "Admin");
        }
        public ActionResult CustomerUpdate(string email,string hoten, int quyenhan)
        {
            if (!string.IsNullOrEmpty(hoten))
            {
                TAIKHOAN tk = db.TAIKHOANs.Where(n => n.Email.Equals(email)).Single();
                if (tk != null)
                {
                    tk.HoTen = hoten;
                    tk.QuyenHan = quyenhan;
                    //update mo ta - db dang thieu

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Customer", "Admin");
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
        public JsonResult UpdateImageProduct()
        {
            List<String> imgpaths = new List<string>();
            db.Configuration.ProxyCreationEnabled = false;
            if (Request.Files.Count > 0)
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        var file = Request.Files[i];
                        Random rand = new Random();
                        int randNumb = rand.Next(10, 100);
                        var code = DateTime.Now.ToString("ddMMyyyyhhmmss") + randNumb;
                        var fileName = code + "." + Request.Files[i].FileName.Split('.')[1];
                        imgpaths.Add(fileName);
                        var path = Path.Combine(Server.MapPath("~/ImgProduct/"), fileName);
                        file.SaveAs(path);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return Json(imgpaths,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProductUpdate(string masp, string tensp, string gioithieusp, string tinhnangsp, string tendm, int slg, int giaban,
                                    string[] imgSP, string imgKey, string maNSX)
        {

                SANPHAM sp = db.SANPHAMs.Where(p => p.MaSP.Equals(masp) && p.BiXoa == 0).Single();
                if (sp != null)
                {
                    sp.TenSP = tensp;
                    sp.MoTa = gioithieusp + "&" + tinhnangsp;
                    sp.GiaBan = giaban;
                    sp.DanhMuc = tendm;
                    sp.NhaSanXuat = maNSX;
                    sp.HinhHienThi = imgKey;
                    sp.SoLuongTon = slg;
                }
                sp.HINHANHs.Clear();
                if (imgSP!=null)
                {
                    for (int i = 0; i < imgSP.Count(); i++)
                    {
                        sp.HINHANHs.Add(new HINHANH() { MaSP = sp.MaSP, PathHinhAnh = imgSP[i] });
                    }
                }

                db.SaveChanges();
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

        public ActionResult ProductCreate(string masp, string tensp, string gioithieusp, string tinhnangsp, string tendm, int slg, int giaban,
                                    string[] imgSP, string imgKey, string maNSX)
        {
                string maSP = "SP" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                SANPHAM sp = new SANPHAM
                {
                    MaSP = maSP,
                    TenSP = tensp,
                    GiaBan = giaban,
                    HinhHienThi = imgKey,
                    SoLuongTon = slg,
                    MoTa = gioithieusp + "&" + tinhnangsp,
                    DanhMuc = tendm,
                    NhaSanXuat = maNSX,
                    SoLuongBan = 0,
                    SoLuotXem = 0,
                    BiXoa = 0
                };
                db.SANPHAMs.Add(sp);
                if (imgSP != null)
                {
                    for (int i = 0; i < imgSP.Count(); i++)
                    {
                        sp.HINHANHs.Add(new HINHANH() { MaSP = sp.MaSP, PathHinhAnh = imgSP[i] });
                    }
                }
                db.SaveChanges();

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