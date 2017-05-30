﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PixelShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            this.HINHANHs = new HashSet<HINHANH>();
        }

        

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        private Nullable<int> _gia;

        public Nullable<int> GiaBan
        {
            get
            {
                return _gia;
            }
            set
            {
                _gia = value;
            }
        }

        private string _giaStr;
        public string giaStr
        {
            get
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                int value = Int32.Parse(_gia.ToString());
                //_giaStr = ((double)value / 100).ToString("C");
                _giaStr = String.Format(elGR, "{0:0,0}", _gia) + " VNÐ";
                return _giaStr;
            }
            set
            {

            }
        }
        public string HinhHienThi { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public string MoTa { get; set; }
        public string DanhMuc { get; set; }
        public string NhaSanXuat { get; set; }
        public Nullable<int> SoLuongBan { get; set; }
        public Nullable<int> SoLuotXem { get; set; }
        public Nullable<int> BiXoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual DANHMUC DANHMUC1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANH> HINHANHs { get; set; }
        public virtual NHASANXUAT NHASANXUAT1 { get; set; }
    }
}
