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
    /// Interaction logic for TaoPhieuThueView.xaml
    /// </summary>
    public partial class TaoPhieuThueView : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public ICommand Command { get; set; }


        private ObservableCollection<String> _ListKhachHang;

        public ObservableCollection<String> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }

        private String _InputKhachHang;

        public String InputKhachHang
        {
            get => _InputKhachHang;
            set
            {
                _InputKhachHang = value;
                OnPropertyChanged();


            }
        }
        public TaoPhieuThueView()
        {
            InitializeComponent();

            this.DataContext = new TaoPhieuThueVM();
        }

        public TaoPhieuThueView(phong p)
        {
            InitializeComponent();

            this.DataContext = new TaoPhieuThueVM(p);
        }
    }
}
