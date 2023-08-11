using QuanLyKhachSan.Models;
using QuanLyKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModels
{
    class KhachHangVM : BaseViewModel
    {
       
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private ObservableCollection<khachhang> _khachHangList;

        public ObservableCollection<khachhang> KhachHangList { get=> _khachHangList; set { _khachHangList = value; OnPropertyChanged(); } }

        private khachhang _SelectedItem;

        public khachhang SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private String _SearchedCustomer;

        public String SearchedCustomer { get => _SearchedCustomer; set { 
                _SearchedCustomer = value; 
                OnPropertyChanged();
                if (_SearchedCustomer == "")
                {
                    KhachHangList = new ObservableCollection<khachhang>(DataProvider.Ins.DB.khachhang);
                }
                else
                {
                    
                    var NewList = from p in DataProvider.Ins.DB.khachhang where p.TenKH == SearchedCustomer select p;
                    KhachHangList = new ObservableCollection<khachhang>(NewList);
                }
            } 
        }

        public KhachHangVM()
        {
            KhachHangList = new ObservableCollection<khachhang>(DataProvider.Ins.DB.khachhang);

            DeleteCommand = new RelayCommand<khachhang>((p) => { return p == null ? false : true; }, (p) =>
            {
                DataProvider.Ins.DB.khachhang.Remove(p);
                DataProvider.Ins.DB.SaveChanges();
                KhachHangList.Remove(p);
            }
            );

            AddCommand = new RelayCommand<khachhang>((p) => { return true; }, (p) =>
            {
                ThemKHView AddWindow = new ThemKHView();
                AddWindow.ShowDialog();

                KhachHangList = new ObservableCollection<khachhang>(DataProvider.Ins.DB.khachhang);
            }
            );
        }
    }

    
}
