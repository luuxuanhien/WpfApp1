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

namespace WpfApp1.View.ChamCong
{
    /// <summary>
    /// Interaction logic for UserControlChamCong.xaml
    /// </summary>
    public partial class UserControlChamCong : UserControl
    {
        public UserControlChamCong()
        {
            InitializeComponent();
        }
        public void clorbangcong()
        {
            bangcong.Foreground = new SolidColorBrush(Colors.Blue);
            bangcong.FontWeight = FontWeights.Bold;
        }
        private void setcolor(Button activebtn, List<Button> noneActive)
        {
            activebtn.Foreground = new SolidColorBrush(Colors.Blue);
            activebtn.FontWeight = FontWeights.Bold;
            foreach(var button in noneActive)
            {
                button.Foreground = new SolidColorBrush(Colors.Black);
                button.FontWeight = FontWeights.Normal;
            }
        }
        public event RoutedEventHandler bangcongClicked;
        public event RoutedEventHandler bangcongthuviecClicked;
        public event RoutedEventHandler khenthuongClicked;
        private void bangcong_Click(object sender, RoutedEventArgs e)
        {
            bangcongClicked?.Invoke(sender, e);
            setcolor(bangcong, new List<Button> { bangcongthuviec, khenthuong });
        }

        private void bangcongthuviec_Click(object sender, RoutedEventArgs e)
        {
            bangcongthuviecClicked?.Invoke(sender, e);
            setcolor(bangcongthuviec, new List<Button> { bangcong, khenthuong });
        }

        private void khenthuong_Click(object sender, RoutedEventArgs e)
        {
            khenthuongClicked?.Invoke(sender, e);
            setcolor(khenthuong, new List<Button> { bangcongthuviec, bangcong });

        }

        //private void bangcong_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainwindow = (MainMenu)Application.Current.MainWindow;
        //    mainwindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainwindow.MainFrameNhanVien.Navigate(new bangcong());
        //    setcolor(bangcong, new List<Button> { bangcongthuviec, khenthuong });
        //}

        //private void bangcongthuviec_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainwindow = (MainMenu)Application.Current.MainWindow;
        //    mainwindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainwindow.MainFrameNhanVien.Navigate(new bangcongthuviec());
        //    setcolor(bangcongthuviec, new List<Button> { bangcong, khenthuong });

        //}

        //private void khenthuong_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainwindow = (MainMenu)Application.Current.MainWindow;
        //    mainwindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //    mainwindow.MainFrameNhanVien.Navigate(new khenthuongkyluat());
        //    setcolor(khenthuong, new List<Button> { bangcongthuviec, bangcong});
        //}
    }
}
