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
using WpfApp1.ViewModel;
using static WpfApp1.App;

namespace WpfApp1.View.NhanVien
{
    /// <summary>
    /// Interaction logic for FormThemNhanVien.xaml
    /// </summary>
    public partial class FormThemNhanVien : Window
    {
        public FormThemNhanVien()
        {
            InitializeComponent();


            var viewModel = instanceFormAndNhanVien.Instance;

            if (viewModel != null)
            {
                viewModel.OnCloseRequested -= CloseWindow;
                viewModel.OnCloseRequested += CloseWindow;
            }

            this.DataContext = viewModel;
        }
        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() => this.Close());
        }
        


        //private void Them_NhanVien_Click(object sender, RoutedEventArgs e)
        //{
        //    var formthemnhanvien = new FormThemNhanVien();
            
        //    {
        //        var navigationService = NavigationService.GetNavigationService(this);
        //        if (navigationService != null)
        //        {
        //            navigationService.Navigate(new NhanVien()); // Load lại chính Page đó
        //        }

        //    }
            
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var delete = new FormThemNhanVien();
        //    delete.Close();
        
    }
}
