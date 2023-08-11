using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyKhachSan.Views;

namespace QuanLyKhachSan.ViewModels
{
    public class TuyChinhVM : BaseViewModel
    {
        private ObservableCollection<loaikhach> _LoaiKhachList = new ObservableCollection<loaikhach>(from lk in DataProvider.Ins.DB.loaikhach select lk);
        public ObservableCollection<loaikhach> LoaiKhachList { get => _LoaiKhachList; set { _LoaiKhachList = value; OnPropertyChanged(); } }

        private ObservableCollection<loaiphong> _LoaiPhongList = new ObservableCollection<loaiphong>(from lp in DataProvider.Ins.DB.loaiphong orderby lp.DonGia select lp);
        public ObservableCollection<loaiphong> LoaiPhongList { get => _LoaiPhongList; set { _LoaiPhongList = value; OnPropertyChanged(); } }

        private ObservableCollection<phuthu> _PhuThuList = new ObservableCollection<phuthu>(from e in DataProvider.Ins.DB.phuthu  select e);
        public ObservableCollection<phuthu> PhuThuList { get => _PhuThuList; set { _PhuThuList = value; OnPropertyChanged(); } }

        private loaiphong _SelectedRoomType ;
        public loaiphong SelectedRoomType { get => _SelectedRoomType; set { _SelectedRoomType = value; OnPropertyChanged(); } }
        private loaikhach _SelectedCustomerType;
        public loaikhach SelectedCustomerType { get => _SelectedCustomerType; set { _SelectedCustomerType  = value; OnPropertyChanged(); } }

        private phuthu _SelectedExtra;
        public phuthu SelectedExtra { get => _SelectedExtra; set { _SelectedExtra = value; OnPropertyChanged(); } }


        public ICommand AddRoomType { get; set; }
        public ICommand EditRoomType { get; set; }

        public ICommand AddCustomerType { get; set; }
        public ICommand EditCustomerType { get; set; }
        public ICommand AddExtra { get; set; }
        public ICommand EditExtra { get; set; }
        public TuyChinhVM()
        {
            AddRoomType = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ThemLoaiPhongView AddRoomTypeWindow = new ThemLoaiPhongView();
                AddRoomTypeWindow.ShowDialog();
                LoaiPhongList = new ObservableCollection<loaiphong>(from lp in DataProvider.Ins.DB.loaiphong orderby lp.DonGia select lp);
            });

            EditRoomType = new RelayCommand<object>((p) =>
            {
                if (SelectedRoomType == null) return false;
                return true;
            }, (p) =>
            {
                SuaLoaiPhongView EditRoomTypeWindow = new SuaLoaiPhongView(SelectedRoomType);
                EditRoomTypeWindow.ShowDialog();
                LoaiPhongList = new ObservableCollection<loaiphong>(from lp in DataProvider.Ins.DB.loaiphong orderby lp.DonGia select lp);
            });

            AddCustomerType = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ThemLoaiKhachHangView AddCustomerTypeTypeWindow = new ThemLoaiKhachHangView();
                AddCustomerTypeTypeWindow.ShowDialog();
                LoaiKhachList = new ObservableCollection<loaikhach>(from lk in DataProvider.Ins.DB.loaikhach select lk);
            });

            EditCustomerType = new RelayCommand<object>((p) =>
            {
                if (SelectedCustomerType == null) return false;
                return true;
            }, (p) =>
            {
                SuaLoaiKhachHangView AddCustomerTypeTypeWindow = new SuaLoaiKhachHangView(SelectedCustomerType);
                AddCustomerTypeTypeWindow.ShowDialog();
                LoaiKhachList = new ObservableCollection<loaikhach>(from lk in DataProvider.Ins.DB.loaikhach select lk);
            });

            AddExtra = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ThemPhuThuView AddExtraWindow = new ThemPhuThuView();
                AddExtraWindow.ShowDialog();
                PhuThuList = new ObservableCollection<phuthu>(from e in DataProvider.Ins.DB.phuthu select e);
            });

            EditExtra = new RelayCommand<object>((p) =>
            {
                if (SelectedExtra == null) return false;
                return true;
            }, (p) =>
            {
                SuaPhuThuView AddExtraWindow = new SuaPhuThuView(SelectedExtra);
                AddExtraWindow.ShowDialog();
                PhuThuList = new ObservableCollection<phuthu>(from e in DataProvider.Ins.DB.phuthu select e);
            });

        }
    }
}
