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
using WpfApp1.View.bophan;

namespace WpfApp1.View.DuAn
{
    /// <summary>
    /// Interaction logic for UserControlduan.xaml
    /// </summary>
    public partial class UserControlduan : UserControl
    {
        public UserControlduan()
        {
            InitializeComponent();
        }
        public void colorduan()
        {
            btnDuAn.Foreground = new SolidColorBrush(Colors.Blue);
            btnDuAn.FontWeight = FontWeights.Bold;
        }
        private void changeColorBP(Button activeBtn, Button noneBtn)
        {
            activeBtn.Foreground = new SolidColorBrush(Colors.Blue);
            activeBtn.FontWeight = FontWeights.Bold;
            noneBtn.Foreground = new SolidColorBrush(Colors.Black);
            noneBtn.FontWeight = FontWeights.Normal;
        }
        public event RoutedEventHandler duAnClicked;
        public event RoutedEventHandler phanCongClicked;
        private void btnDuAn_Click(object sender, RoutedEventArgs e)
        {
            duAnClicked?.Invoke(sender, e);
            changeColorBP(btnDuAn, btnPhanCong);
        }

        // Phương thức xử lý sự kiện click cho btnPhanCong
        private void btnPhanCong_Click(object sender, RoutedEventArgs e)
        {
            phanCongClicked?.Invoke(sender, e);
            changeColorBP(btnPhanCong, btnDuAn);
        }
        //private void btnDuAn_Click(object sender, RoutedEventArgs e)
        //{
        //    changeColorBP(btnDuAn, btnPhanCong);
        //    var mainWindow = (MainMenu)Application.Current.MainWindow;
        //    mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainWindow.MainFrameNhanVien.Navigate(new duan());

        //}

        //private void btnPhanCong_Click(object sender, RoutedEventArgs e)
        //{
        //    changeColorBP(btnPhanCong, btnDuAn);
        //    var mainWindow = (MainMenu)Application.Current.MainWindow;
        //    mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainWindow.MainFrameNhanVien.Navigate(new phancong());
        //}
    }
}
