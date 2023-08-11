using QuanLyKhachSan.Models;
using QuanLyKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhachSan.ViewModels
{
    public class PhongTrangChuVM : BaseViewModel
    {
        public ICommand Command { get; set; }


        private phong _Phong;
        public phong Phong { get => _Phong; set { _Phong = value; OnPropertyChanged(); } }

        private Brush _InfoAreaBackground;
        public Brush InfoAreaBackground { get { return _InfoAreaBackground; } set { _InfoAreaBackground = value; OnPropertyChanged(); } }

        PhongTrangChuVM(phong p)
        {
            Phong = p;

            var bc = new BrushConverter();

            if (Phong.TinhTrang == "Sẵn sàng") InfoAreaBackground = (Brush)bc.ConvertFrom("#27cf6f");
            else if (Phong.TinhTrang == "Đang có khách") InfoAreaBackground = (Brush)bc.ConvertFrom("#d6413e");

            //Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    String x = CommandsListbox.SelectedValue.ToString();
            //    if (x == "Thanh toán")
            //    {
            //        if (Phong.TinhTrang == "Đang có khách")
            //        {
            //            ThemPhongView PayWindow = new ThemPhongView();
            //            PayWindow.ShowDialog();
            //        }
            //        else if (Phong.TinhTrang == "Sẵn sàng")
            //        {
            //            ThemPhongView PayWindow = new ThemPhongView();
            //            PayWindow.ShowDialog();
            //        }
            //    }
            //}
           //);

        }
    }
}
