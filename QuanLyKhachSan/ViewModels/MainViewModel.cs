using QuanLyKhachSan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyKhachSan.UserControls;
using System.Collections.ObjectModel;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Views;

namespace QuanLyKhachSan.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region commands
        public ICommand LoadDashboard { get; set; }
        public ICommand LoadCustomer { get; set; }
        public ICommand LoadHistory{ get; set; }
        public ICommand LoadRoom{ get; set; }
        public ICommand LoadCustom { get; set; }
        public ICommand LoadReport{ get; set; }

        public ICommand LoadPhieuThue { get; set; }

        public ICommand LoadDatPhong { get; set; }

        public ICommand DatPhongCommand { get; set; }





        #endregion

        private UserControl _selectedContentVM;
        public UserControl SelectedContentVM
        {
            get { return _selectedContentVM; }
            set
            {
                _selectedContentVM = value;
                OnPropertyChanged(nameof(SelectedContentVM));
            }
        }



        public MainViewModel() {
            SelectedContentVM = new TrangChuUC();
            LoadDashboard = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                SelectedContentVM = new TrangChuUC();
            }
            );
            LoadCustomer = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {

                SelectedContentVM = new KhachHangUC();
            }
            );
            LoadRoom = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {

                SelectedContentVM = new PhongUC();
            }
            );
            LoadCustom = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {

                SelectedContentVM = new TuyChinhUC();
            }
            );
            LoadPhieuThue = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {

                SelectedContentVM = new DSPhieuThueUC();
            });

            LoadDatPhong = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {

                SelectedContentVM = new DatPhongUC();
            }
            );

            DatPhongCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                TaoPhieuThueView RentingWindow = new TaoPhieuThueView();
                RentingWindow.ShowDialog();
            }
            );

            var PhongList = new ObservableCollection<phong>(from p in DataProvider.Ins.DB.phong orderby p.MaPhong select p);
            DateTime Today = DateTime.Today;
            foreach (var Phong in PhongList)
            {
                String TinhTrangPhong;
                var checkTinhTrangPhong = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue < Today && pt.NgayTraPhong > Today);
                if (checkTinhTrangPhong.Count() > 0)
                {
                    TinhTrangPhong = "Đang có khách";

                }
                else
                {
                    DateTime CurrentTime = DateTime.Now;
                    DateTime CompareTime = DateTime.Today.AddHours(12D);
                    if (CurrentTime >= CompareTime) // Nếu đã qua 12h
                    {
                        var CheckInToday = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue == Today);
                        if (CheckInToday.Count() > 0) // Đã qua 12h, nếu có khách đặt phòng hôm nay...
                        {
                            TinhTrangPhong = "Đang có khách";
                        }
                        else // Nếu không có khách nào đặt hôm nay
                        {
                            TinhTrangPhong = "Sẵn sàng";
                        }
                    }
                    else // Nếu chưa qua 12h
                    {
                        var CheckOutToday = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayTraPhong == Today);
                        if (CheckOutToday.Count() > 0) // Khách cũ checkout hôm nay, chưa qua 12h, khách vẫn ở trong phòng
                        {
                            TinhTrangPhong = "Đang có khách";
                        }
                        else
                        {
                            var CheckInToday = DataProvider.Ins.DB.chitietphieuthue.Where(pt => pt.MaPhong == Phong.MaPhong && pt.NgayThue == Today);
                            if (CheckInToday.Count() > 0) // Nếu phòng có khách đặt hôm nay
                            {
                                TinhTrangPhong = "Đặt trước";
                            }
                            else
                            {
                                TinhTrangPhong = "Sẵn sàng";
                            }

                        }
                    }
                }
                DataProvider.Ins.DB.phong.Where(p => p.MaPhong == Phong.MaPhong).First().TinhTrang = TinhTrangPhong;
            }
            DataProvider.Ins.DB.SaveChanges();
        }    
    }
}
