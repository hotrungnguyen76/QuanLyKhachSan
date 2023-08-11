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
    /// Interaction logic for ThemKHView.xaml
    /// </summary>
    public partial class ThemKHView : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public List<String> LoaiPhongList;

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private ObservableCollection<String> _LoaiKhachList;

        public ObservableCollection<String> LoaiKhachList { get => _LoaiKhachList; set { _LoaiKhachList = value; OnPropertyChanged(); } }

        private String _TenKH;
        public String TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }

        private String _SDT;
        public String SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private String _CMND;
        public String CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }

        private String _DiaChi;
        public String DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private String _LoaiKH;
        public String LoaiKH { get => _LoaiKH; set { _LoaiKH = value; OnPropertyChanged(); } }
        public ThemKHView()
        {
            InitializeComponent();
            this.DataContext = this;
            LoaiKhachList = new ObservableCollection<String>(from p in DataProvider.Ins.DB.loaikhach select p.LoaiKhach1);

            AddCommand = new RelayCommand<khachhang>((p) =>
            {
                if (TenKH != "") TenKHtxt.BorderBrush = Brushes.Black;
                else TenKHtxt.BorderBrush = Brushes.Red;

                if (SDT != "") SDTtxt.BorderBrush = Brushes.Black;
                else SDTtxt.BorderBrush = Brushes.Red;

                if (CMND != "") CMNDtxt.BorderBrush = Brushes.Black;
                else CMNDtxt.BorderBrush = Brushes.Red;

                if (DiaChi != "") DiaChitxt.BorderBrush = Brushes.Black;
                else DiaChitxt.BorderBrush = Brushes.Red;

                if (LoaiKH != null) LoaiKHcb.BorderBrush = Brushes.Black;
                else LoaiKHcb.BorderBrush = Brushes.Red;

                if (TenKH == "" || SDT == "" || DiaChi == "" || LoaiKH == null) return false;

                return true;
            }, (p) =>
            {
                int CheckExist = DataProvider.Ins.DB.khachhang.Where(x => x.TenKH == TenKH && x.CMND == CMND).Count();

                //if (!CMND.All(char.IsDigit))
                //{
                //    warning.Text = "Số CCCD không được chứa chữ";
                //} 

                if (CheckExist == 0)
                {
                    khachhang newObj = new khachhang();
                    newObj.TenKH = TenKH;
                    newObj.SDT = SDT;
                    newObj.CMND =  CMND;
                    newObj.LoaiKhach = LoaiKH;
                    newObj.DiaChi = DiaChi;

                    DataProvider.Ins.DB.khachhang.Add(newObj);
                    DataProvider.Ins.DB.SaveChanges();
                    ThemKHWindow.Close();
                }
                else if (CheckExist > 0)
                {
                    warning.Text = "Khách hàng đã tồn tại";
                }
            });

            CloseCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ThemKHWindow.Close();
            });
        }

        
    }
}
