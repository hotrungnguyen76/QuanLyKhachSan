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
    
    public partial class phieuthue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public phieuthue()
        {
            this.chitietphieuthue = new HashSet<chitietphieuthue>();
            this.hoadon = new HashSet<hoadon>();
        }
    
        public int MaPhieuThue { get; set; }
        public System.DateTime NgayThue { get; set; }
        public int MaKH { get; set; }
        public string NVTao { get; set; }
        public System.DateTime NgayTraPhong { get; set; }
        public string TinhTrang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietphieuthue> chitietphieuthue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoadon> hoadon { get; set; }
        public virtual khachhang khachhang { get; set; }
        public virtual taikhoan taikhoan { get; set; }
    }
}