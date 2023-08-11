using QuanLyKhachSan.Models;
using QuanLyKhachSan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for HoaDonView.xaml
    /// </summary>
    public partial class ThanhToanView : Window
    {
        public ThanhToanView()
        {
            InitializeComponent();
        }

        public ThanhToanView(phieuthue pt)
        {
            InitializeComponent();
            this.DataContext = new ThanhToanVM(pt);
        }
    }
}
