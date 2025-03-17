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
using WpfApp1.View.ChamCong;

namespace WpfApp1.View.BangLuong
{
    /// <summary>
    /// Interaction logic for UserControlBangLuong.xaml
    /// </summary>
    public partial class UserControlBangLuong : UserControl
    {
        public UserControlBangLuong()
        {
            InitializeComponent();
        }
        public void colorbtn1()
        {

            bangluong1.Foreground = new SolidColorBrush(Colors.Blue);
            bangluong1.FontWeight = FontWeights.Bold;
        }
        
        private void setcolor(Button activebtn, List<Button> noneActive)
        {
            activebtn.Foreground = new SolidColorBrush(Colors.Blue);
            activebtn.FontWeight = FontWeights.Bold;
            foreach (var button in noneActive)
            {
                button.Foreground = new SolidColorBrush(Colors.Black);
                button.FontWeight = FontWeights.Normal;
            }
        }
        public event RoutedEventHandler bangluong1Clicked;
        public event RoutedEventHandler changeClicked;
        public event RoutedEventHandler bangtinhClicked;
        private void bangluong1_Click(object sender, RoutedEventArgs e)
        {
            bangluong1Clicked?.Invoke(sender, e);
            setcolor(bangluong1, new List<Button> { change, bangtinh });
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            changeClicked?.Invoke(sender, e);
            setcolor(change, new List<Button> { bangluong1, bangtinh });
        }

        private void bangtinh_Click(object sender, RoutedEventArgs e)
        {
            bangtinhClicked?.Invoke(sender, e);
            setcolor(bangtinh, new List<Button> { change, bangluong1 });
        }


        //private void bangluong_Click(object sender, RoutedEventArgs e)
        //{
        //    setcolor(bangluong, new List<Button> { change, bangtinh });
        //    var window = (MainMenu)Application.Current.MainWindow;
        //    window.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    window.MainFrameNhanVien.Navigate(new BangLuong());
        //}

        //private void change_Click(object sender, RoutedEventArgs e)
        //{
        //    setcolor(change, new List<Button> { bangtinh, bangtinh });
        //    var window = (MainMenu)Application.Current.MainWindow;
        //    window.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    window.MainFrameNhanVien.Navigate(new ChangeBangLuong());
        //}

        //private void bangtinh_Click(object sender, RoutedEventArgs e)
        //{
        //    setcolor(bangtinh, new List<Button> { change, bangluong });
        //    var window = (MainMenu)Application.Current.MainWindow;
        //    window.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    window.MainFrameNhanVien.Navigate(new BangTinhLuong());
        //}
    }
        

      
}
