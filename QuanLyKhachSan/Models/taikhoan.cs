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
    
    public partial class taikhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public taikhoan()
        {
            this.hoadon = new HashSet<hoadon>();
            this.phieuthue = new HashSet<phieuthue>();
        }
    
        public string Username { get; set; }
        public string Password { get; set; }
        public string TenNV { get; set; }
        public Nullable<long> CMND { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoadon> hoadon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<phieuthue> phieuthue { get; set; }
    }
}
