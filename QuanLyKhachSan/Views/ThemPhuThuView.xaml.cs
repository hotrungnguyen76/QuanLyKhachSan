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
    public partial class ThemPhuThuView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String _LoaiPhuThu;
        public String LoaiPhuThu { get => _LoaiPhuThu; set { _LoaiPhuThu = value; OnPropertyChanged(); } }

        public String _TyLe;
        public String TyLe { get => _TyLe; set { _TyLe = value; OnPropertyChanged(); } }

        public String _warning;
        public String warning { get => _warning; set { _warning = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ThemPhuThuView()
        {
            InitializeComponent();
            this.DataContext = this;

            SaveCommand = new RelayCommand<Window>((p) =>
            {
                if (LoaiPhuThu == null || TyLe == null || LoaiPhuThu == "" || TyLe == "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var checkExist = DataProvider.Ins.DB.phuthu.Where(e => e.LoaiPhuThu == LoaiPhuThu).Count();
                if (!TyLe.All(char.IsDigit))
                {
                    warning = "Tỷ lệ phải là một số nguyên!";
                }
                else if (checkExist > 0)
                {
                    warning = "Loại phụ thu này đã tồn tại!";
                }
                else
                {
                    phuthu newObj = new phuthu();
                    newObj.LoaiPhuThu = LoaiPhuThu;
                    newObj.MucPhuThu = Int32.Parse(TyLe);
                    DataProvider.Ins.DB.phuthu.Add(newObj);
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
