using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModels
{

    public class ThanhToanVM : BaseViewModel
    {
        public class RoomPaymentInfo : BaseViewModel
        {
            public int SoPhong { get; set; }
            public String LoaiPhong { get; set; }
            public float DonGia { get; set; }
            public int SoNgayThue { get; set; }
            public float PhuThu { get; set; }

            private float _ThanhTien;
            public float ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

            private String _LoaiKhach = "Nội địa";
            public String LoaiKhach { get => _LoaiKhach; set { 
                    _LoaiKhach = value;
                    OnPropertyChanged();
                    float HeSo = (float)(from lk in DataProvider.Ins.DB.loaikhach where lk.LoaiKhach1 == LoaiKhach select lk.HeSo).First();
                    ThanhTien = (SoNgayThue * DonGia + PhuThu) * HeSo;
                } 
            }
            public RoomPaymentInfo(chitietphieuthue pt)
            {
                SoPhong = pt.MaPhong;
                //phong room = DataProvider.Ins.DB.phong.Where(p => p.MaPhong == pt.MaPhong).First();
                LoaiPhong = pt.phong.loaiphong1.LoaiPhong1;
                DonGia = (float)pt.phong.loaiphong1.DonGia;
                SoNgayThue = (int)(pt.NgayTraPhong - pt.NgayThue).TotalDays;
                PhuThu = 0;
                
                float HeSo = (float)(from lk in DataProvider.Ins.DB.loaikhach where lk.LoaiKhach1 == LoaiKhach select lk.HeSo).First();
                if (pt.PhuThu != null) PhuThu = (float)(pt.phuthu1.MucPhuThu / 100) * SoNgayThue * DonGia ;
                _ThanhTien = (SoNgayThue * DonGia + PhuThu)*HeSo;
            }
        }


        private String _TenKH;
        public String TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }

        private String _DiaChi;
        public String DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private DateTime _Today = DateTime.Now;
        public DateTime Today { get => _Today; set { _Today = value; OnPropertyChanged(); } }

        private float _TongTien = 0;
        public float TongTien { get => _TongTien; 
            set { 
                _TongTien = value; 
                OnPropertyChanged(); 
            } 
        }

        private ObservableCollection<RoomPaymentInfo> _RoomPaymentInfoList ;

        public ObservableCollection<RoomPaymentInfo> RoomPaymentInfoList
        {
            get => _RoomPaymentInfoList;
            set
            {
                _RoomPaymentInfoList = value;
                OnPropertyChanged();  
            }
        }

        private ObservableCollection<String> _LoaiKhachList = new ObservableCollection<String> (from lk in DataProvider.Ins.DB.loaikhach select lk.LoaiKhach1.ToString());

        public ObservableCollection<String> LoaiKhachList { get => _LoaiKhachList; set { _LoaiKhachList = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ThanhToanVM(phieuthue pt)
        {

            TenKH = pt.khachhang.TenKH;
            DiaChi = pt.khachhang.DiaChi;
            RoomPaymentInfoList = new ObservableCollection<RoomPaymentInfo>();
            var ListPhong = DataProvider.Ins.DB.chitietphieuthue.Where(p => p.MaPhieuThue == pt.MaPhieuThue);
            foreach (var phong in ListPhong)
            {
                var tmp = new RoomPaymentInfo(phong);
                RoomPaymentInfoList.Add(tmp);
                
            }
            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                TongTien = 0;
                foreach (var phong in RoomPaymentInfoList) TongTien += phong.ThanhTien;
                TongTien = (int)TongTien;
            });
            

            PaymentCommand = new RelayCommand<Window>((p) =>
            {
                if (TongTien > 0) return true;
                return false;
            }, (p) =>
            {
                hoadon newObj = new hoadon();
                newObj.MaPhieuThue = pt.MaPhieuThue;
                newObj.NgayThanhToan = Today;
                newObj.SoNgayThue = (int)(pt.NgayTraPhong - pt.NgayThue).TotalDays;
                newObj.TongTien = TongTien;
                DataProvider.Ins.DB.hoadon.Add(newObj);
                DataProvider.Ins.DB.phieuthue.Where(pt1 => pt1.MaPhieuThue == pt.MaPhieuThue).First().TinhTrang = "Đã thanh toán";
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });

            CloseCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }
    }
}
    
