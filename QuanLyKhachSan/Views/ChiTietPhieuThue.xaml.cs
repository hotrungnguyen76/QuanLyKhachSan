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
    /// Interaction logic for ChiTietPhieuThue.xaml
    /// </summary>
    public partial class ChiTietPhieuThue : Window, INotifyPropertyChanged
    {
        private phieuthue _PhieuThue;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public phieuthue PhieuThue { get => _PhieuThue; set { _PhieuThue = value; OnPropertyChanged(); } }

        private String _CheckInDate;
        public String CheckInDate { get => _CheckInDate; set { _CheckInDate = value; OnPropertyChanged(); } }

        private String _CheckOutDate;
        public String CheckOutDate { get => _CheckOutDate; set { _CheckOutDate = value; OnPropertyChanged(); } }

        private ObservableCollection<chitietphieuthue> _RentingRoomInfoList;

        public ObservableCollection<chitietphieuthue> RentingRoomInfoList { get => _RentingRoomInfoList; set { _RentingRoomInfoList = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ChiTietPhieuThue()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ChiTietPhieuThue(phieuthue pt)
        {
            InitializeComponent();
            this.DataContext = this;
            PhieuThue = pt;
            CheckInDate = PhieuThue.NgayThue.ToString("dd/MM/yyyy");
            CheckOutDate = PhieuThue.NgayTraPhong.ToString("dd/MM/yyyy");
            RentingRoomInfoList = new ObservableCollection<chitietphieuthue>(DataProvider.Ins.DB.chitietphieuthue.Where(pt1 => pt1.MaPhieuThue == PhieuThue.MaPhieuThue));

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
