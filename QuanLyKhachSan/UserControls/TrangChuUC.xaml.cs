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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyKhachSan.UserControls;
using QuanLyKhachSan.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace QuanLyKhachSan.UserControls
{
    /// <summary>
    /// Interaction logic for TrangChuUC.xaml
    /// </summary>
    /// 
   
    public partial class TrangChuUC : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<phong> _ListPhong;

        public ObservableCollection<phong> ListPhong
        {
            get => _ListPhong;
            set
            {
                _ListPhong = value;
                OnPropertyChanged();
                //LoadTrangChu();

            }
        }

        void LoadTrangChu()
        {
            foreach (TabItem item in TabList.Items)
            {
                //var ListLoaiPhong = DataProvider.Ins.DB.loaiphong.Where(p => true);
                ListPhong = new ObservableCollection<phong>(DataProvider.Ins.DB.phong);
                var ListLoaiPhong = from p in DataProvider.Ins.DB.loaiphong orderby p.DonGia select p;

                ScrollViewer sv = new ScrollViewer();
                StackPanel container = new StackPanel();

                // Thêm các phòng vào expander
                foreach (var LoaiPhong in ListLoaiPhong)
                {
                    Expander ep = new Expander();
                    ep.Header = LoaiPhong.LoaiPhong1;

                    ep.IsExpanded = true;

                    WrapPanel wp = new WrapPanel();
                    ObservableCollection<phong> PhongTungLoai = new ObservableCollection<phong>();
                    if (item.Header.ToString() != "Tất cả")
                    {
                        for (int i = 0; i < ListPhong.Count; i++)
                        {
                            if (ListPhong[i].LoaiPhong == (String)ep.Header && ListPhong[i].TinhTrang == (String)item.Header)
                            {
                                PhongTungLoai.Add(ListPhong[i]);
                            }
                        }
                        //PhongTungLoai = DataProvider.Ins.DB.phong.Where(p => p.LoaiPhong == (String)ep.Header && (String)p.TinhTrang == (String)item.Header);
                    }
                    else
                    {
                        for (int i = 0; i < ListPhong.Count; i++)
                            if (ListPhong[i].LoaiPhong == (String)ep.Header)
                            {
                                PhongTungLoai.Add(ListPhong[i]);
                            }
                        //ListPhong = DataProvider.Ins.DB.phong.Where(p => p.LoaiPhong == ep.Header);
                    }

                    if (PhongTungLoai != null)
                    {
                        foreach (var i in PhongTungLoai)
                        {
                            PhongTrangChuUC phongUC = new PhongTrangChuUC(i);
                            phongUC.Margin = new Thickness(3, 0, 0, 3);
                            wp.Children.Add(phongUC);

                        }
                    }
                    ep.Content = wp;
                    container.Children.Add(ep);
                }
                sv.Content = container;
                item.Content = sv;
            }

        }

        public TrangChuUC()
        {
            InitializeComponent();
            //this.DataContext = this;
            //var TabControl = (TabControl)this.FindName("TabList");

            // Thêm các expander các loại phòng trong từng tab
            LoadTrangChu();

        }
    }
}
