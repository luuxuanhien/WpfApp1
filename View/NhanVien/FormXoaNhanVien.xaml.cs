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
using System.Windows.Shapes;
using WpfApp1.ViewModel;
using OfficeOpenXml;

namespace WpfApp1.View.NhanVien
{
    /// <summary>
    /// Interaction logic for FormXoaNhanVien.xaml
    /// </summary>
    public partial class FormXoaNhanVien : Window
    {
        public FormXoaNhanVien()
        {
            InitializeComponent();
           
           

        }
        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() => this.Close());
        }
        public FormXoaNhanVien(int manv, string cccd)
        {
            InitializeComponent();
            //DataContext = instanceFormAndNhanVien.Instance;

            MaNV_Delet.Text = manv.ToString();
            CCCD_Delet.Text  = cccd;
            var viewMode = instanceFormAndNhanVien.Instance;
            this.DataContext = viewMode;

            if (viewMode != null)
            {
                viewMode.OnCloseRequeste -= CloseWindow;
                viewMode.OnCloseRequeste += CloseWindow;
            }


        }


    }
}
