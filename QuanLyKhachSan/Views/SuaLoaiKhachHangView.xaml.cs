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
    public partial class SuaLoaiKhachHangView : Window, INotifyPropertyChanged
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

        private String _LoaiKhach;
        public String LoaiKhach { get => _LoaiKhach; set { _LoaiKhach = value; OnPropertyChanged(); } }

        public String _HeSo;
        public String HeSo { get => _HeSo; set { _HeSo = value; OnPropertyChanged(); } }

        public String _warning;
        public String warning { get => _warning; set { _warning = value; OnPropertyChanged(); } }

        public SuaLoaiKhachHangView()
        {
            InitializeComponent();
        }
            public SuaLoaiKhachHangView(loaikhach lk)
        {
            InitializeComponent();
            this.DataContext = this;
            var key = lk;
            LoaiKhach = key.LoaiKhach1;
            HeSo = key.HeSo.ToString();
            SaveCommand = new RelayCommand<Window>((p) =>
            {
                if (LoaiKhach == null || HeSo == null || LoaiKhach == "" || HeSo == "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var checkExist = DataProvider.Ins.DB.loaikhach.Where(lk1 => lk1.LoaiKhach1 == LoaiKhach && lk1.LoaiKhach1 != key.LoaiKhach1).Count();
                float HeSoFloat = 0;
                if (!float.TryParse(HeSo, out HeSoFloat))
                {
                    warning = "Hệ số phải là một số!";
                }
                else if (checkExist > 0)
                {
                    warning = "Loại khách này đã tồn tại!";
                }
                else
                {
                    var tmp = DataProvider.Ins.DB.loaikhach.Where(lk1 => lk1.LoaiKhach1 == key.LoaiKhach1).First();
                    tmp.LoaiKhach1 = LoaiKhach;
                    float.TryParse(HeSo, out HeSoFloat);
                    tmp.HeSo = (float)HeSoFloat;

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
