using QuanLyKhachSan.Models;
using QuanLyKhachSan.ViewModels;
using QuanLyKhachSan.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKhachSan.UserControls
{
    /// <summary>
    /// Interaction logic for PhongTrangChuUC.xaml
    /// </summary>
    public partial class PhongTrangChuUC : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RentingCommand { get; set; }
        public ICommand PaymentCommand { get; set; }


        private phong _Phong;
        public phong Phong { get => _Phong; set { _Phong = value; OnPropertyChanged(); } }

        public PhongTrangChuUC() 
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public PhongTrangChuUC(phong p)
        {
            InitializeComponent();
            this.DataContext = this;
            DateTime Today = DateTime.Today;
            Phong = p;
            int MaPhieuThue = 0;
           
            //String TinhTrangPhong ;

            //var checkTinhTrangPhong = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue < Today && pt.NgayTraPhong > Today);
            //if (checkTinhTrangPhong.Count() > 0)
            //{
            //    TinhTrangPhong = "Đang có khách";
            //    MaPhieuThue = checkTinhTrangPhong.First().MaPhieuThue;
            //    TinhTrangThanhToan = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == checkTinhTrangPhong.FirstOrDefault().MaPhieuThue).First().TinhTrang;
            //}
            //else
            //{
            //    DateTime CurrentTime = DateTime.Now;
            //    DateTime CompareTime = DateTime.Today.AddHours(12D);
            //    if (CurrentTime >= CompareTime) // Nếu đã qua 12h
            //    {
            //        var CheckInToday = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue == Today);
            //        if (CheckInToday.Count() > 0)
            //        {
            //            TinhTrangPhong = "Đang có khách";
            //            MaPhieuThue = CheckInToday.First().MaPhieuThue;
            //            TinhTrangThanhToan = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == CheckInToday.FirstOrDefault().MaPhieuThue).First().TinhTrang;
            //        }
            //        else
            //        {
            //            TinhTrangPhong = "Sẵn sàng";
            //            TinhTrangThanhToan = "";
            //        }
            //    }
            //    else // Nếu chưa qua 12h
            //    {
            //        var CheckOutToday = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayTraPhong == Today);
            //        if (CheckOutToday.Count() > 0)
            //        {
            //            TinhTrangPhong = "Đang có khách";
            //            MaPhieuThue = CheckOutToday.First().MaPhieuThue;
            //            TinhTrangThanhToan = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == CheckOutToday.First().MaPhieuThue).First().TinhTrang;
            //        }
            //        else
            //        {
            //            TinhTrangPhong = "Sẵn sàng";
            //            TinhTrangThanhToan = "";
            //        }
            //    }
            //}

            if (Phong.TinhTrang == "Đang có khách")
            {
                
                var tmp1 = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue < Today && pt.NgayTraPhong > Today);
                if (tmp1.Count() > 0)
                {
                    MaPhieuThue = tmp1.First().MaPhieuThue;
                }
                else
                {
                    DateTime CurrentTime = DateTime.Now;
                    DateTime CompareTime = DateTime.Today.AddHours(12D);
                    if (CurrentTime > CompareTime) // Nếu đã qua 12h
                    {
                        var tmp2 = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue == Today);
                        MaPhieuThue = tmp2.First().MaPhieuThue;
                    }
                    else
                    {
                        var tmp3 = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayTraPhong == Today);
                        if (tmp3.Count() > 0) MaPhieuThue = tmp3.First().MaPhieuThue;
                    }
                }
                
            }
            else if(Phong.TinhTrang == "Đặt trước")
            {
                var PhieuThue = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue == Today);
                MaPhieuThue = PhieuThue.First().MaPhieuThue;
            }

            TinhTrangPhongTB.Text = Phong.TinhTrang;
            if (Phong.TinhTrang == "Đang có khách" || Phong.TinhTrang == "Đặt trước")
            {
                var tmpp = Phong.MaPhong;
                TinhTrangThanhToanTB.Text = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == MaPhieuThue).First().TinhTrang;

            }

            var bc = new BrushConverter();
            if (Phong.TinhTrang == "Sẵn sàng") InfoArea.Background = (Brush)bc.ConvertFrom("#27cf6f");
            else if (Phong.TinhTrang == "Đang có khách") InfoArea.Background = (Brush)bc.ConvertFrom("#d6413e");
            else if (Phong.TinhTrang == "Đặt trước") InfoArea.Background = (Brush)bc.ConvertFrom("#a142f5");

            RentingCommand = new RelayCommand<object>((m) =>
            {
                if (Phong.TinhTrang == "Sẵn sàng") return true;
                return false;
            }, (m) =>
            {
                TaoPhieuThueView RentingWindow = new TaoPhieuThueView(Phong);
                RentingWindow.ShowDialog();
            });

            PaymentCommand = new RelayCommand<object>((m) => {
                if ((Phong.TinhTrang == "Đang có khách" || Phong.TinhTrang == "Đặt trước") ) {
                    String TinhTrangThanhToan = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == MaPhieuThue).First().TinhTrang;
                    if (TinhTrangThanhToan == "Chưa thanh toán") return true;
                } 
                return false;
            }, (m) =>
            {
              
                phieuthue PhieuThue = DataProvider.Ins.DB.phieuthue.Where(pt => pt.MaPhieuThue == MaPhieuThue).First();
                ThanhToanView PaymentWindow = new ThanhToanView(PhieuThue);
                PaymentWindow.ShowDialog();
            }
            );

        }
    }
}
