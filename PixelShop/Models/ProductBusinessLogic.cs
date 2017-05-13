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
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Ten))
                    result = result.Where(x => x.TenSP.Contains(searchModel.Ten));
                if (!string.IsNullOrEmpty(searchModel.DanhMuc))
                    result = result.Where(x => x.DanhMuc==searchModel.DanhMuc);
                if (!string.IsNullOrEmpty(searchModel.NhaSanXuat))
                    result = result.Where(x => x.NhaSanXuat==searchModel.NhaSanXuat);
                if (searchModel.GiaTu.HasValue)
                    result = result.Where(x => x.GiaBan >= searchModel.GiaTu);
                if (searchModel.GiaDen.HasValue)
                    result = result.Where(x => x.GiaBan <= searchModel.GiaDen);
            }
            return result;
        }
    }
}