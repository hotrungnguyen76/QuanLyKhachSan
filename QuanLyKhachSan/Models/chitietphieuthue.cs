//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKhachSan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class chitietphieuthue
    {
        public int MaPhieuThue { get; set; }
        public int MaPhong { get; set; }
        public string PhuThu { get; set; }
        public Nullable<int> SLKhach { get; set; }
        public System.DateTime NgayThue { get; set; }
        public System.DateTime NgayTraPhong { get; set; }
    
        public virtual phieuthue phieuthue { get; set; }
        public virtual phong phong { get; set; }
        public virtual phuthu phuthu1 { get; set; }
    }
}
