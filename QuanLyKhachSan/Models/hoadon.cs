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
    
    public partial class hoadon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hoadon()
        {
            this.chitiethoadon = new HashSet<chitiethoadon>();
        }
    
        public int MaHoaDon { get; set; }
        public Nullable<int> MaPhieuThue { get; set; }
        public Nullable<int> SoNgayThue { get; set; }
        public Nullable<float> TongTien { get; set; }
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
        public string NVTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitiethoadon> chitiethoadon { get; set; }
        public virtual phieuthue phieuthue { get; set; }
        public virtual taikhoan taikhoan { get; set; }
    }
}
