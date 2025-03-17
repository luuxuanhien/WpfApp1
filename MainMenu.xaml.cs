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
using WpfApp1.View.NhanVien;
using WpfApp1.View.bophan;
using WpfApp1.View.ChamCong;
using WpfApp1.View.BangLuong;
using WpfApp1.View.DuAn;
using WpfApp1.View.NghiPhep;
using WpfApp1.View.TraCuuTT;
using static MaterialDesignThemes.Wpf.Theme;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            
            
        }



        private loginviewmodel _viewModel;

        public MainMenu(string tenuser, string quyenhan, string tennhanvien)
        {
            InitializeComponent();

            _viewModel = new loginviewmodel
            {
                TenUser = tenuser,
                QuyenHan = quyenhan,
                TenNhanVien = tennhanvien
            };

            DataContext = _viewModel;
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void resetbuttoncolor()
        {
            string color1 = "#F5FFFA";
            Brush borderBrush = (Brush)new BrushConverter().ConvertFromString(color1);
            nhanvien.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            nhanvien.BorderBrush = borderBrush;
            trangchu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            trangchu.BorderBrush = borderBrush;
            bophan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            bophan.BorderBrush = borderBrush;
            chamcong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            chamcong.BorderBrush = borderBrush;
            bangluong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            bangluong.BorderBrush = borderBrush;
            duan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            duan.BorderBrush = borderBrush;
            nghiphep.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            nghiphep.BorderBrush = borderBrush;
            baocaothongke.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            baocaothongke.BorderBrush = borderBrush;
            tracuuthongtin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            tracuuthongtin.BorderBrush = borderBrush;
            hethong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FFFA"));
            hethong.BorderBrush = borderBrush;

        }
        private void hienpage()
        {
            MainFrameNhanVien.Visibility = Visibility.Visible;
            addUserControl.Visibility = Visibility.Visible;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(UserListBox.Visibility == Visibility.Visible)
            {
                UserListBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserListBox.Visibility = Visibility.Visible;
            }
           
        }

        private void nhanvien_click(object sender, RoutedEventArgs e)
        {

            resetbuttoncolor();
            hienpage();
            nhanvien.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            nhanvien.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));

            UserControlNhanVien userControlNhanVien = new UserControlNhanVien();
            addUserControl.Children.Add(userControlNhanVien);
            MainFrameNhanVien.Navigate(new Uri("View/NhanVien/NhanVien.xaml", UriKind.Relative));
            userControlNhanVien.BtnNVClicked += BtnNV_Click;
            userControlNhanVien.BtnBHClicked += BtnBH_Click;
            userControlNhanVien.BtnQLClicked += BtnQL_Click;
            userControlNhanVien.BtnMailClicked += BtnMail_Click;
            userControlNhanVien.changeBtnNv();


        }
        // usercontrol của nhân viên

        private void BtnNV_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/NhanVien/NhanVien.xaml", UriKind.Relative));

        }

        private void BtnBH_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/NhanVien/QuanLyCheDo.xaml", UriKind.Relative));
        }

        private void BtnQL_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/NhanVien/QLTVNV.xaml", UriKind.Relative));
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/NhanVien/Mail.xaml", UriKind.Relative));

        }


        private void bophan_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            bophan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            bophan.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            usercontrolbophan UsercontrolBoPhan = new usercontrolbophan();
            addUserControl.Children.Add(UsercontrolBoPhan);
            MainFrameNhanVien.Navigate(new Uri("View/bophan/phongban.xaml", UriKind.Relative));
            UsercontrolBoPhan.btnBP();
            UsercontrolBoPhan.phongBanClicked += phongBan_Click;
            UsercontrolBoPhan.boPhanClicked += boPhan_Click;


        }
        private void phongBan_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/bophan/phongban.xaml", UriKind.Relative));
        }

        // Xử lý sự kiện click cho nút "Bộ Phận"
        private void boPhan_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/bophan/bophan.xaml", UriKind.Relative));
        }

        private void trangchu_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            trangchu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            trangchu.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            imageSangTao.Visibility = Visibility.Visible;
            MainFrameNhanVien.Visibility = Visibility.Collapsed;
            addUserControl.Visibility = Visibility.Collapsed;
        }

        private void chamcong_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            chamcong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            chamcong.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            UserControlChamCong userControlChamCong = new UserControlChamCong();
            addUserControl.Children.Add(userControlChamCong);
            MainFrameNhanVien.Navigate(new Uri("View/ChamCong/bangcong.xaml", UriKind.Relative));
            userControlChamCong.clorbangcong();
            userControlChamCong.bangcongClicked += bangcong_Click;
            userControlChamCong.bangcongthuviecClicked += bangcongthuviec_Click;
            userControlChamCong.khenthuongClicked += khenthuong_Click;

        }
        private void bangcong_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/ChamCong/bangcong.xaml", UriKind.Relative));
        }

        private void bangcongthuviec_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/ChamCong/bangcongthuviec.xaml", UriKind.Relative));
        }

        private void khenthuong_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/ChamCong/khenthuongkyluat.xaml", UriKind.Relative));
        }

        private void bangluong_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            bangluong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            bangluong.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            UserControlBangLuong userControlBangLuong = new UserControlBangLuong();
            addUserControl.Children.Add(userControlBangLuong);
            MainFrameNhanVien.Navigate(new Uri("View/BangLuong/BangLuong.xaml", UriKind.Relative));
            userControlBangLuong.colorbtn1();
            userControlBangLuong.bangluong1Clicked += bangluong1_Click;
            userControlBangLuong.changeClicked += change_Click;
            userControlBangLuong.bangtinhClicked += bangtinh_Click;


        }
        private void bangluong1_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/BangLuong/BangLuong.xaml", UriKind.Relative));

        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/BangLuong/ChangeBangLuong.xaml", UriKind.Relative));

        }

        private void bangtinh_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/BangLuong/BangTinhLuong.xaml", UriKind.Relative));
        }


        private void duan_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            duan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            duan.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            UserControlduan usercontrolduan = new UserControlduan();
            addUserControl.Children.Add(usercontrolduan);
            MainFrameNhanVien.Navigate(new Uri("View/DuAn/duan.xaml", UriKind.Relative));
            usercontrolduan.colorduan();
            usercontrolduan.duAnClicked += duAn_Click;
            usercontrolduan.phanCongClicked += phanCong_Click;

        }
        private void duAn_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/DuAn/duan.xaml", UriKind.Relative));
        }

        // Xử lý sự kiện click cho nút "Phân Công"
        private void phanCong_Click(object sender, RoutedEventArgs e)
        {
            MainFrameNhanVien.Navigate(new Uri("View/DuAn/phancong.xaml", UriKind.Relative));
        }

        private void nghiphep_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            nghiphep.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            nghiphep.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            MainFrameNhanVien.Navigate(new Uri("View/NghiPhep/nghiphep.xaml", UriKind.Relative));
            usercontrolNP Usercontrolnp = new usercontrolNP();
            addUserControl.Children.Add(Usercontrolnp);
            
        }

        private void baocaothongke_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            baocaothongke.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            baocaothongke.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
        }

        private void tracuuthongtin_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            tracuuthongtin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            tracuuthongtin.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            TimKiem timkiem = new TimKiem();
            addUserControl.Children.Add(timkiem);
            MainFrameNhanVien.Navigate(new Uri("View/TraCuuTT/tracuutt.xaml", UriKind.Relative));

        }

        private void hethong_Click(object sender, RoutedEventArgs e)
        {
            resetbuttoncolor();
            hienpage();
            hethong.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
            hethong.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00CED1"));
        }
    }
}
