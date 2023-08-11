using QuanLyKhachSan.Models;
using QuanLyKhachSan.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModels
{
    public class TaoPhieuThueVM : BaseViewModel
    {
       
        public class RentingRoomInfo<T1, T2, T3>
        {
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }
            public T3 Item3 { get; set; }
            public RentingRoomInfo(T1 i1, T2 i2, T3 i3)
            {
                Item1 = i1;
                Item2 = i2;
                Item3 = i3;
            }
        }

        private ObservableCollection<phong> _AvailableRoomList;

        public ObservableCollection<phong> AvailableRoomList { get => _AvailableRoomList; 
            set {
                _AvailableRoomList = value;
                OnPropertyChanged();
                _AvailableRoomList.OrderBy(p => p.MaPhong);
            } 
        }

        private phong _LeftSelectedRoom;
        public phong LeftSelectedRoom { get => _LeftSelectedRoom; set { _LeftSelectedRoom = value; OnPropertyChanged(); } }

        private ObservableCollection<RentingRoomInfo<phong, int, String>> _SelectedRoomList;

        public ObservableCollection<RentingRoomInfo<phong, int, String>> SelectedRoomList { get => _SelectedRoomList; set { _SelectedRoomList = value; OnPropertyChanged(); } }

        private RentingRoomInfo<phong, int, String> _RightSelectedRoom;

        public RentingRoomInfo<phong, int, String> RightSelectedRoom { get => _RightSelectedRoom; set { _RightSelectedRoom = value; OnPropertyChanged(); } }

        private List<int> _CustomerCountList;

        public List<int> CustomerCountList { get => _CustomerCountList; set { _CustomerCountList = value; OnPropertyChanged(); } }

        private DateTime _CheckInDate;

        public DateTime CheckInDate { get => _CheckInDate; set { 
                _CheckInDate = value; 
                OnPropertyChanged();
                LoadAvailableRooms();
            } }

        private DateTime _CheckOutDate;

        public DateTime CheckOutDate { 
            get => _CheckOutDate; 
            set { _CheckOutDate = value; 
                OnPropertyChanged(); 
                LoadAvailableRooms(); 
            } 
        }

        private String _TenKH;
        public String TenKH { get => _TenKH; set { _TenKH = value; OnPropertyChanged(); } }

        private String _SDT;
        public String SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private String _CMND;
        public String CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }

        private String _DiaChi;
        public String DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private String _LoaiKhach = "Nội địa";
        public String LoaiKhach { get => _LoaiKhach; set { _LoaiKhach = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _LoaiKhachList;

        public ObservableCollection<String> LoaiKhachList { get => _LoaiKhachList; set { _LoaiKhachList = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _ListPhuThu;

        public ObservableCollection<String> ListPhuThu { get => _ListPhuThu; set { _ListPhuThu = value; OnPropertyChanged(); } }

        private String _Notification;
        public String Notification { get => _Notification; set { _Notification = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand SelectRoom { get; set; }
        public ICommand RemoveSelectedRoom { get; set; }

        void LoadAvailableRooms()
        {
            if ( CheckInDate < CheckOutDate)
            {
                var AvailableRooms = from p in DataProvider.Ins.DB.phong
                                         //join pt in DataProvider.Ins.DB.chitietphieuthue on p.MaPhong equals pt.MaPhong
                                     where (!DataProvider.Ins.DB.chitietphieuthue.Any(pt => pt.MaPhong == p.MaPhong && pt.NgayTraPhong > CheckInDate && pt.NgayThue < CheckOutDate))
                                     select p;
                AvailableRoomList = new ObservableCollection<phong>(AvailableRooms);
            }
            else
            {
                AvailableRoomList.Clear();
            }
        }

        public TaoPhieuThueVM()
        {
            ListPhuThu = new ObservableCollection<String>(from pt in DataProvider.Ins.DB.phuthu select pt.LoaiPhuThu);
            ListPhuThu.Add(null);

            AvailableRoomList = new ObservableCollection<phong>(DataProvider.Ins.DB.phong.Where(x => x.TinhTrang == "Sẵn sàng"));

            SelectedRoomList = new ObservableCollection<RentingRoomInfo<phong, int, String>>();
            

            LoaiKhachList = new ObservableCollection<String>(from lk in DataProvider.Ins.DB.loaikhach select lk.LoaiKhach1);
            CheckInDate = DateTime.Today;
            CheckOutDate = DateTime.Today.AddDays(1);

            SelectRoom = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {

                if (LeftSelectedRoom != null)
                {
                    RentingRoomInfo<phong, int, String> room = new RentingRoomInfo<phong, int, String>(LeftSelectedRoom, (int)LeftSelectedRoom.loaiphong1.SLKhachToiDa, null);
                    SelectedRoomList.Add(room);
                    AvailableRoomList.Remove(LeftSelectedRoom);
                }
            }
            );

            RemoveSelectedRoom = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (RightSelectedRoom != null)
                {
                    AvailableRoomList.Add(RightSelectedRoom.Item1);
                    SelectedRoomList.Remove(RightSelectedRoom);

                    // Sắp xếp lại DS phòng trống sau khi thêm lại <Chưa xong>
                    this.AvailableRoomList.OrderBy(o => o.MaPhong);
                    OnPropertyChanged("ReadyRoomList");
                    //
                }
            }
            );

            CreateCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedRoomList.Count != 0 || TenKH != null || CMND != null) return true;
                return false;
            }, (p) =>
            {
                //int IntCMND = 0;
                //if (CMND.All(char.IsDigit)) IntCMND = Int32.Parse(CMND); // Kiểm tra xem CMND có phải toàn số hay không
                khachhang Customer;
                var checkExistCustomer = (DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND)).Count();
                if (checkExistCustomer != 0)
                {
                    Customer = DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND).First();
                }
                else
                {
                    Customer = new khachhang();
                    Customer.TenKH = TenKH;
                    Customer.CMND = CMND;
                    Customer.SDT = SDT;
                    Customer.DiaChi = DiaChi;
                    Customer.LoaiKhach = LoaiKhach;
                    DataProvider.Ins.DB.khachhang.Add(Customer);
                    DataProvider.Ins.DB.SaveChanges();
                    Customer = DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND).First();
                }

                phieuthue newPhieuThue = new phieuthue();
                newPhieuThue.NgayThue = CheckInDate;
                newPhieuThue.NgayTraPhong = CheckOutDate;
                newPhieuThue.MaKH = Customer.MaKH;
                newPhieuThue.TinhTrang = "Chưa thanh toán";
                DataProvider.Ins.DB.phieuthue.Add(newPhieuThue);
                DataProvider.Ins.DB.SaveChanges();

                int MaPhieuThue = DataProvider.Ins.DB.phieuthue.Max(pt => pt.MaPhieuThue);
                foreach (RentingRoomInfo<phong, int, String> room in SelectedRoomList)
                {
                    // Tạo data trong table phiếu thuê
                    // Tạo data trong table chi tiết phiếu thuê
                    chitietphieuthue newCTPhieuThue = new chitietphieuthue();
                    newCTPhieuThue.MaPhieuThue = MaPhieuThue;
                    newCTPhieuThue.MaPhong = room.Item1.MaPhong;
                    newCTPhieuThue.SLKhach = room.Item2;
                    newCTPhieuThue.PhuThu = room.Item3;
                    newCTPhieuThue.NgayThue = CheckInDate;
                    newCTPhieuThue.NgayTraPhong = CheckOutDate;
                    DataProvider.Ins.DB.chitietphieuthue.Add(newCTPhieuThue);
                    //phong rentedRoom = DataProvider.Ins.DB.phong.Where(r => r.MaPhong == newPhieuThue.MaPhong).First();
                    //rentedRoom.TinhTrang = "Đang có khách";
                    //DataProvider.Ins.DB.phieuthue.Add;
                }
                //DataProvider.Ins.DB.chitietphieuthue.SqlQuery("SET IDENTITY_INSERT mydb.chitietphieuthue ON");

                //DataProvider.Ins.DB.
               
                DataProvider.Ins.DB.SaveChanges();
                Notification = "Đặt phòng thành công!";


                Thread.Sleep(2000);
                TenKH = "";
                CMND = "";
                SDT = "";
                DiaChi = "";
                LoaiKhach = "";
                AvailableRoomList = new ObservableCollection<phong>();
                

                SelectedRoomList = new ObservableCollection<RentingRoomInfo<phong, int, String>>();
                if (p.GetType().Equals(typeof(Window))) ((Window)p).Close();
                Notification = "";

            });
            CloseCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }
    
        public TaoPhieuThueVM(phong FirstRoom)
        {
            //var PhuThuCBItemsSource = (from pt in DataProvider.Ins.DB.phuthu select pt.LoaiPhuThu);
            
            ListPhuThu = new ObservableCollection<String>(from pt in DataProvider.Ins.DB.phuthu select pt.LoaiPhuThu);
            ListPhuThu.Add(null);
            // Danh sách các phòng trống
            AvailableRoomList = new ObservableCollection<phong>(DataProvider.Ins.DB.phong.Where(x => x.TinhTrang == "Sẵn sàng" && x.MaPhong != FirstRoom.MaPhong));
            // Danh sách các phòng được chọn. Thêm sẵn vào phòng được chọn ở trang chủ
            SelectedRoomList = new ObservableCollection<RentingRoomInfo<phong, int, String>>();
            SelectedRoomList.Add(new RentingRoomInfo<phong, int, String>(FirstRoom, (int)FirstRoom.loaiphong1.SLKhachToiDa, null));

            LoaiKhachList = new ObservableCollection<String>(from lk in DataProvider.Ins.DB.loaikhach select lk.LoaiKhach1);
            CheckInDate = DateTime.Today;
            CheckOutDate = DateTime.Today.AddDays(1);

            SelectRoom = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {

                if (LeftSelectedRoom != null)
                {
                    RentingRoomInfo<phong, int, String> room = new RentingRoomInfo<phong, int, String>(LeftSelectedRoom, (int)LeftSelectedRoom.loaiphong1.SLKhachToiDa, null);
                    SelectedRoomList.Add(room);
                    AvailableRoomList.Remove(LeftSelectedRoom);
                }
            }
            );

            RemoveSelectedRoom = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (RightSelectedRoom != null)
                {
                    AvailableRoomList.Add(RightSelectedRoom.Item1);
                    SelectedRoomList.Remove(RightSelectedRoom);

                    // Sắp xếp lại DS phòng trống sau khi thêm lại <Chưa xong>
                    this.AvailableRoomList.OrderBy(o => o.MaPhong);
                    OnPropertyChanged("ReadyRoomList");
                    //
                }
            }
            );

            CreateCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedRoomList.Count != 0 || TenKH != null || CMND != null ) return true;
                return false;
            }, (p) =>
            {
                //int IntCMND = 0;
                //if (CMND.All(char.IsDigit)) IntCMND = Int32.Parse(CMND); // Kiểm tra xem CMND có phải toàn số hay không
                khachhang Customer;
                var checkExistCustomer = (DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND)).Count();
                if (checkExistCustomer != 0)
                {
                    Customer = DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND).First();
                }
                else
                {
                    Customer = new khachhang();
                    Customer.TenKH = TenKH;
                    Customer.CMND = CMND;
                    Customer.SDT = SDT;
                    Customer.DiaChi = DiaChi;
                    Customer.LoaiKhach = LoaiKhach;
                    DataProvider.Ins.DB.khachhang.Add(Customer);
                    DataProvider.Ins.DB.SaveChanges();
                    Customer = DataProvider.Ins.DB.khachhang.Where(kh => kh.TenKH == TenKH && kh.CMND == CMND).First();
                }

                phieuthue newPhieuThue = new phieuthue();
                newPhieuThue.NgayThue = CheckInDate;
                newPhieuThue.NgayTraPhong = CheckOutDate;
                newPhieuThue.MaKH = Customer.MaKH;
                newPhieuThue.TinhTrang = "Chưa thanh toán";
                DataProvider.Ins.DB.phieuthue.Add(newPhieuThue);
                DataProvider.Ins.DB.SaveChanges();

                int MaPhieuThue = DataProvider.Ins.DB.phieuthue.Max(pt => pt.MaPhieuThue);
                foreach (RentingRoomInfo<phong, int, String> room in SelectedRoomList)
                {
                    // Tạo data trong table phiếu thuê
                    // Tạo data trong table chi tiết phiếu thuê
                    chitietphieuthue newCTPhieuThue = new chitietphieuthue();
                    newCTPhieuThue.MaPhieuThue = MaPhieuThue;
                    newCTPhieuThue.MaPhong = room.Item1.MaPhong;
                    newCTPhieuThue.SLKhach = room.Item2;
                    newCTPhieuThue.PhuThu = room.Item3;
                    newCTPhieuThue.NgayThue = CheckInDate;
                    newCTPhieuThue.NgayTraPhong = CheckOutDate;
                    DataProvider.Ins.DB.chitietphieuthue.Add(newCTPhieuThue);
                    //phong rentedRoom = DataProvider.Ins.DB.phong.Where(r => r.MaPhong == newPhieuThue.MaPhong).First();
                    //rentedRoom.TinhTrang = "Đang có khách";
                    //DataProvider.Ins.DB.phieuthue.Add;
                }
                //DataProvider.Ins.DB.chitietphieuthue.SqlQuery("SET IDENTITY_INSERT mydb.chitietphieuthue ON");

                //DataProvider.Ins.DB.

                DataProvider.Ins.DB.SaveChanges(); 

                p.Close();

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
