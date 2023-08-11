using QuanLyKhachSan.Models;
using QuanLyKhachSan.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ThemPhongView : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public List<String> LoaiPhongList;

        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private ObservableCollection<String> _LoaiPhongList;

        public ObservableCollection<String> LoaiPhongList { get => _LoaiPhongList; set { _LoaiPhongList = value; OnPropertyChanged(); } }

        private phong _Phong;
        public phong Phong { get => _Phong; set { _Phong = value; OnPropertyChanged(); } }

        private String _MaPhong;
        public String MaPhong { get => _MaPhong; set { _MaPhong = value; OnPropertyChanged(); } }

        public String _LoaiPhong;
        public String LoaiPhong { 
            get => _LoaiPhong; 
            set { 
                _LoaiPhong = value; 
                
                if (_LoaiPhong != null)
                {
                    loaiphong RoomType = DataProvider.Ins.DB.loaiphong.Where(x => x.LoaiPhong1 == _LoaiPhong).First();
                    Pricetxt.Text = RoomType.DonGia.ToString();
                    CustomerCounttxt.Text = RoomType.SLKhachToiDa.ToString();
                }
                OnPropertyChanged();
            } 
        }

        public String _TinhTrang;
        public String TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }

        private int checkValidRoomId(String RoomID)
        {
            if (RoomID.All(char.IsDigit) == false) return 1; //1: Số phòng đã nhập không phải là INT 
            int IntRoomId = Int32.Parse(MaPhong);
            var CheckExist = DataProvider.Ins.DB.phong.Where(x => x.MaPhong == IntRoomId).Count();
            if (CheckExist > 0) return 2; // 2: Số phòng đã tồn tại
            
            return 0;
        }
        public ThemPhongView()
        {
            InitializeComponent();
            this.DataContext = this;
            LoaiPhongList = new ObservableCollection<String>(from p in DataProvider.Ins.DB.loaiphong select p.LoaiPhong1);

            AddCommand = new RelayCommand<phong>((p) =>
            {
                if (MaPhong != "") MaPhongtxt.BorderBrush = Brushes.Black;
                else
                {
                    MaPhongtxt.BorderBrush = Brushes.Red;
                   
                }

                if (LoaiPhong != null) LoaiPhongcb.BorderBrush = Brushes.Black;
                else
                {
                    LoaiPhongcb.BorderBrush = Brushes.Red;
                    return false;
                }

                return true;
            }, (p) =>
            {
                int CheckValidResult = checkValidRoomId(MaPhong);
                if (CheckValidResult == 0)
                {
                    phong newObj = new phong();
                    newObj.MaPhong = Int32.Parse(MaPhong);
                    newObj.LoaiPhong = LoaiPhongcb.Text;
                    newObj.TinhTrang = "Sẵn sàng";


                    DataProvider.Ins.DB.phong.Add(newObj);
                    DataProvider.Ins.DB.SaveChanges();
                    ThemPhongWindow.Close();
                }
                else if (CheckValidResult == 1)
                {
                    warning.Text = "Số phòng phải là một số!";
                }
                else if (CheckValidResult == 2)
                {
                    warning.Text = "Số phòng đã tồn tại! Hãy nhập số phòng khác.";
                }
                
       
            });

            CloseCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ThemPhongWindow.Close();
            });
        }

        
    }
}
