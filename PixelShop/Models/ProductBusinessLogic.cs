using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelShop.Models
{
    public class ProductBusinessLogic
    {
        private PixelShopEntities Context;
        public ProductBusinessLogic()
        {
            Context = new PixelShopEntities();
        }

        public IQueryable<SANPHAM> GetProducts(ProductSearchModel searchModel)
        {
            var result = Context.SANPHAMs.AsQueryable();
            result = result.Where(x => x.BiXoa == 0 && x.SoLuongTon > 0);
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Ten))
                    result = result.Where(x => x.TenSP.ToLower().Contains(searchModel.Ten.ToLower()));
                if (!string.IsNullOrEmpty(searchModel.DanhMuc))
                    result = result.Where(x => x.DanhMuc==searchModel.DanhMuc);
                if (!string.IsNullOrEmpty(searchModel.NhaSanXuat))
                    result = result.Where(x => x.NhaSanXuat==searchModel.NhaSanXuat);
                if (!string.IsNullOrEmpty(searchModel.Gia))
                {
                    string[] giaSearch = searchModel.Gia.Split('-');
                    int GiaTu = int.Parse(giaSearch[0]);
                    int GiaDen = int.Parse(giaSearch[1]);
                    result = result.Where(x => x.GiaBan >= GiaTu && x.GiaBan <= GiaDen);
                }
            }
            return result;
        }
    }
}