using QuanLyKhachSan.Models;
using QuanLyKhachSan.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ThemLoaiKhachHangView.xaml
    /// </summary>
    public partial class ThemLoaiKhachHangView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String _LoaiKhach;
        public String LoaiKhach { get => _LoaiKhach; set { _LoaiKhach = value; OnPropertyChanged(); } }

        public String _HeSo;
        public String HeSo { get => _HeSo; set { _HeSo = value; OnPropertyChanged(); } }

        public String _warning;
        public String warning { get => _warning; set { _warning = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ThemLoaiKhachHangView()
        {
            InitializeComponent();
            this.DataContext = this;

            SaveCommand = new RelayCommand<Window>((p) =>
            {
                if (LoaiKhach == null || HeSo == null || LoaiKhach == "" || HeSo == "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var checkExist = DataProvider.Ins.DB.loaikhach.Where(lk => lk.LoaiKhach1 == LoaiKhach).Count();
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
                    loaikhach newObj = new loaikhach();
                    newObj.LoaiKhach1 = LoaiKhach;
                    float.TryParse(HeSo, out HeSoFloat);
                    newObj.HeSo = HeSoFloat;
                    DataProvider.Ins.DB.loaikhach.Add(newObj);
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
    }
}
