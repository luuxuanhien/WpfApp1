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

namespace WpfApp1.View.NhanVien
{
    /// <summary>
    /// Interaction logic for UserControlNhanVien.xaml
    /// </summary>
    public partial class UserControlNhanVien : UserControl
    {
        //    public UserControlNhanVien()
        //    {
        //        InitializeComponent();
        //    }
        public void changeBtnNv()
        {
            BtnNV.Foreground = new SolidColorBrush(Colors.Blue);
            BtnNV.FontWeight = FontWeights.Bold;
        }

        //    private void Button_Click(object sender, RoutedEventArgs e)
        //    {
        //        var mainWindow = (MainMenu)Application.Current.MainWindow;
        //        mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //        //mainWindow.MainFrameNhanVien.Content = null;
        //        //mainWindow.controlNhanVien.Content = new NhanVien();
        //        mainWindow.MainFrameNhanVien.Navigate(new NhanVien());
        //        setbtnunderlineNhanVien(BtnNV, new List<Button> { BtnQL, BtnMail, BtnBH });
        //    }

        //    private void Button_Click_1(object sender, RoutedEventArgs e)
        //    {
        //        var mainWindow = (MainMenu)Application.Current.MainWindow;
        //        mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //        //mainWindow.MainFrameNhanVien.Content = null;
        //       // mainWindow.controlNhanVien.Content = new QuanLyCheDo();
        //        mainWindow.MainFrameNhanVien.Navigate(new QuanLyCheDo());
        //        setbtnunderlineNhanVien(BtnBH, new List<Button> { BtnQL, BtnMail, BtnNV });
        //    }


        //    }

        //    private void BtnQL_Click(object sender, RoutedEventArgs e)
        //    {
        //        var mainWindow = (MainMenu)Application.Current.MainWindow;
        //        mainWindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //        //mainWindow.MainFrameNhanVien.Content = null;
        //        // mainWindow.controlNhanVien.Content = new QuanLyCheDo();
        //        mainWindow.MainFrameNhanVien.Navigate(new QLTVNV());
        //        setbtnunderlineNhanVien(BtnQL, new List<Button> { BtnBH, BtnMail, BtnNV });
        //    }

        //    private void BtnMail_Click(object sender, RoutedEventArgs e)
        //    {
        //        var mainwindow = (MainMenu)Application.Current.MainWindow;
        //        mainwindow.MainFrameNhanVien.NavigationService.RemoveBackEntry();
        //        //mainWindow.MainFrameNhanVien.Content = null;
        //        // mainWindow.controlNhanVien.Content = new QuanLyCheDo();
        //        mainwindow.MainFrameNhanVien.Navigate(new Mail());
        //        setbtnunderlineNhanVien(BtnMail, new List<Button> { BtnBH, BtnQL, BtnNV });
        //    }
        //}

        public event RoutedEventHandler BtnNVClicked;
        public event RoutedEventHandler BtnBHClicked;
        public event RoutedEventHandler BtnQLClicked;
        public event RoutedEventHandler BtnMailClicked;

        public UserControlNhanVien()
        {
            InitializeComponent();
        }
        private void setbtnunderlineNhanVien(Button active, List<Button> noneActive)
        {
            active.Foreground = new SolidColorBrush(Colors.Blue);
            active.FontWeight = FontWeights.Bold;
            foreach (var button in noneActive)
            {
                button.Foreground = new SolidColorBrush(Colors.Black);
                button.FontWeight = FontWeights.Normal;

            }
        }

        public void BtnNV_Click(object sender, RoutedEventArgs e)
        {
            BtnNVClicked?.Invoke(sender, e);
            setbtnunderlineNhanVien(BtnNV, new List<Button> { BtnQL, BtnMail, BtnBH });

        }

        private void BtnBH_Click(object sender, RoutedEventArgs e)
        {
            BtnBHClicked?.Invoke(sender, e);
            setbtnunderlineNhanVien(BtnBH, new List<Button> { BtnQL, BtnMail, BtnNV });
        }

        private void BtnQL_Click(object sender, RoutedEventArgs e)
        {
            BtnQLClicked?.Invoke(sender, e);
            setbtnunderlineNhanVien(BtnQL, new List<Button> { BtnBH, BtnMail, BtnNV });
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            BtnMailClicked?.Invoke(sender, e);
            setbtnunderlineNhanVien(BtnMail, new List<Button> { BtnBH, BtnQL, BtnNV });
        }
    }
    }
