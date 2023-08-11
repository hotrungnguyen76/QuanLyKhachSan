using QuanLyKhachSan.Models;
using QuanLyKhachSan.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace QuanLyKhachSan.Views
{
    /// <summary>
    /// Interaction logic for SuaPhongView.xaml
    /// </summary>
    public partial class ThemLoaiPhongView : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public List<String> LoaiPhongList;
        public phong Room{ get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private String _LoaiPhong;
        public String LoaiPhong { get => _LoaiPhong; set { _LoaiPhong = value; OnPropertyChanged(); } }

        public String _DonGia;
        public String DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        public String _SLKhach;
        public String SLKhach { get => _SLKhach; set { _SLKhach = value; OnPropertyChanged(); } }

        public String _warning;
        public String warning { get => _warning; set { _warning = value; OnPropertyChanged(); } }


        public ThemLoaiPhongView()
        {
            InitializeComponent();
            this.DataContext = this;
            SaveCommand = new RelayCommand<Window>((p) =>
            {
                if (LoaiPhong == null || DonGia == null || SLKhach == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var checkExist = DataProvider.Ins.DB.loaiphong.Where(lk => lk.LoaiPhong1 == LoaiPhong).Count();
                if (!DonGia.All(char.IsDigit) || !SLKhach.All(char.IsDigit))
                {
                    warning = "Đơn giá và Sl khách phải là một số!";
                }
                else if(checkExist > 0 )
                {
                    var tmp = LoaiPhong;
                    warning = "Loại phòng đã tồn tại!";
                }
                else
                {
                    loaiphong newObj = new loaiphong();
                    newObj.LoaiPhong1 = LoaiPhong;
                    newObj.DonGia = Int32.Parse(DonGia);
                    newObj.SLKhachToiDa = Int32.Parse(SLKhach);


                    DataProvider.Ins.DB.loaiphong.Add(newObj);
                    //DataProvider.Ins.DB.phong.Remove(SelectedRoom);

                    DataProvider.Ins.DB.SaveChanges();
                    p.Close();
                }
            });

            CloseCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }




        //public khachhang SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        //public SuaPhongView(phong SelectedRoom) : this()
        //{
        //    InitializeComponent();
        //    this.DataContext = this;

        //    MaPhong = SelectedRoom.MaPhong.ToString();
        //    LoaiPhong = SelectedRoom.LoaiPhong;


        //    LoaiPhongList = new ObservableCollection<String>(from p in DataProvider.Ins.DB.loaiphong select p.LoaiPhong1);

        //    SaveCommand = new RelayCommand<khachhang>((p) =>
        //    {
        //        if (MaPhong == "")
        //        {
        //            MaPhongtxt.BorderBrush = Brushes.Red;
        //            return false;
        //        }
        //        return true;
        //    }, (p) =>
        //    {
        //        //var room = DataProvider.Ins.DB.phong.Where(x => x.MaPhong == SelectedRoom.MaPhong).SingleOrDefault();
        //        if (SelectedRoom.MaPhong != Int32.Parse(MaPhong) || SelectedRoom.LoaiPhong != LoaiPhongcb.Text )
        //        {
        //            phong newObj = new phong();
        //            newObj.MaPhong = Int32.Parse(MaPhong);
        //            newObj.LoaiPhong = LoaiPhongcb.Text;


        //            DataProvider.Ins.DB.phong.Add(newObj);
        //            DataProvider.Ins.DB.phong.Remove(SelectedRoom);
        //            DataProvider.Ins.DB.SaveChanges();
        //        }

        //        SuaPhongWindow.Close();
        //    });

        //    CloseCommand = new RelayCommand<Window>((p) =>
        //    {
        //        return true;
        //    }, (p) =>
        //    {
        //        SuaPhongWindow.Close();
        //    });
        //}
    }
}
