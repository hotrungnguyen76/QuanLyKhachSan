using QuanLyKhachSan.Models;
using QuanLyKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModels
{

    public class PhongVM : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private ObservableCollection<phong> _PhongList;
        public ObservableCollection<phong> PhongList { get => _PhongList; set { _PhongList = value; OnPropertyChanged(); } }

        private phong _SelectedRoom;
        public phong SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; OnPropertyChanged(); } }

        private String _SearchedRoom;
        public String SearchedRoom
        {
            get => _SearchedRoom;
            set
            {
                _SearchedRoom = value;
                OnPropertyChanged();
                if (SearchedRoom == "")
                {
                    PhongList = new ObservableCollection<phong>(DataProvider.Ins.DB.phong);
                }
                else
                {
                    int searchRoomId = Int32.Parse(value);
                    var NewList = from p in DataProvider.Ins.DB.phong where p.MaPhong == searchRoomId select p;
                    PhongList = new ObservableCollection<phong>(NewList);
                }
            }
        }
        public PhongVM()
        {
            PhongList = new ObservableCollection<phong>(from p in DataProvider.Ins.DB.phong orderby p.MaPhong select p);
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

            EditCommand = new RelayCommand<phong>((p) => { return p == null ? false : true; }, (p) =>
            {
                SuaPhongView EditWindow = new SuaPhongView(p);
                EditWindow.ShowDialog();
                PhongList = new ObservableCollection<phong>(DataProvider.Ins.DB.phong);
                //OnPropertyChanged("PhongList");
            }
            );

            AddCommand = new RelayCommand<phong>((p) => { return true; }, (p) =>
            {
                ThemPhongView AddWindow = new ThemPhongView();
                AddWindow.ShowDialog();
                PhongList = new ObservableCollection<phong>(DataProvider.Ins.DB.phong);
                //OnPropertyChanged("PhongList");
            }
            );

        }
        
    }
}
