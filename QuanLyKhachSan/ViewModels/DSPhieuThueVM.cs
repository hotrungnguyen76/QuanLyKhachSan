using QuanLyKhachSan.Models;
using QuanLyKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModels
{
    public class DSPhieuThueVM : BaseViewModel
    {

        public ICommand FilterCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand DetailCommand { get; set; }

        private ObservableCollection<phieuthue> _ListPhieuThue;

        public ObservableCollection<phieuthue> ListPhieuThue { get => _ListPhieuThue; set { _ListPhieuThue = value; OnPropertyChanged(); } }

        private phieuthue _SelectedItem;
        public phieuthue SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private String _FilterText;
        public String FilterText { get => _FilterText; set { _FilterText = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _ListFilterProperty = new ObservableCollection<String>() { "Tên KH", "Số DT" };

        public ObservableCollection<String> ListFilterProperty { get => _ListFilterProperty; set { _ListFilterProperty = value; OnPropertyChanged(); } }

        public DSPhieuThueVM()
        {
            ListPhieuThue = new ObservableCollection<phieuthue>(DataProvider.Ins.DB.phieuthue);

            FilterCommand = new RelayCommand<ComboBox>((p) => { return p.SelectedValue == null ? false : true; }, (p) =>
            {
                if (p.SelectedValue.ToString() == "Tên KH")
                {
                    ListPhieuThue = new ObservableCollection<phieuthue>(DataProvider.Ins.DB.phieuthue.Where(pt=>pt.khachhang.TenKH == FilterText));
                }
                else if (p.SelectedValue.ToString() == "Số DT")
                {
                    ListPhieuThue = new ObservableCollection<phieuthue>(DataProvider.Ins.DB.phieuthue.Where(pt => pt.khachhang.SDT == FilterText));
                }
            }
           );

            DetailCommand = new RelayCommand<object>((p) => {
                if (SelectedItem != null) return true;
                return false;
            }, (p) =>
            {
                ChiTietPhieuThue DetailWindow = new ChiTietPhieuThue(SelectedItem);
                DetailWindow.ShowDialog();
            }
           );

        }

    }
}
