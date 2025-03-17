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

namespace WpfApp1.View.bophan
{
    /// <summary>
    /// Interaction logic for usercontrolbophan.xaml
    /// </summary>
    public partial class usercontrolbophan : UserControl
    {
        public usercontrolbophan()
        {
            InitializeComponent();
        }
        public void btnBP()
        {
            btnPhongBan.Foreground = new SolidColorBrush(Colors.Blue);
            btnPhongBan.FontWeight = FontWeights.Bold;
        }
        private void changeColorBP(Button activeBtn, Button noneBtn)
        {
            activeBtn.Foreground = new SolidColorBrush(Colors.Blue);
            activeBtn.FontWeight = FontWeights.Bold;
            noneBtn.Foreground = new SolidColorBrush(Colors.Black);
            noneBtn.FontWeight = FontWeights.Normal;
        }
        public event RoutedEventHandler phongBanClicked;
        public event RoutedEventHandler boPhanClicked;
        private void btnPhongBan_Click(object sender, RoutedEventArgs e)
        {
            phongBanClicked?.Invoke(sender, e);
            changeColorBP(btnPhongBan, btnBoPhan);
        }

        // Phương thức xử lý sự kiện click cho btnBoPhan
        private void btnBoPhan_Click(object sender, RoutedEventArgs e)
        {
            boPhanClicked?.Invoke(sender, e);
            changeColorBP(btnBoPhan, btnPhongBan);
        }

        //private void btnPhongBan_Click(object sender, RoutedEventArgs e)
        //{
        //    changeColorBP(btnPhongBan, btnBoPhan);
        //    var mainWindow = (MainMenu)Application.Current.MainWindow;
        //    mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainWindow.MainFrameNhanVien.Navigate(new phongban());


        //}

        //private void btnBoPhan_Click(object sender, RoutedEventArgs e)
        //{
        //    changeColorBP(btnBoPhan, btnPhongBan);
        //    var mainWindow = (MainMenu)Application.Current.MainWindow;
        //    mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainWindow.MainFrameNhanVien.Navigate(new bophan());
        //}
    }
}
