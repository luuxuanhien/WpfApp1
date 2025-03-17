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
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1.View.NhanVien
{
    /// <summary>
    /// Interaction logic for ThemBaoHiem.xaml
    /// </summary>
    public partial class ThemBaoHiem : Window
    {
        public ThemBaoHiem()
        {
            InitializeComponent();

            var viewMode = instanceFormAndNhanVien.Instance;
            this.DataContext = viewMode;
            if (viewMode != null)
            {
                viewMode.OnCloseRequeste -= CloseWindow;
                viewMode.OnCloseRequeste += CloseWindow;
            }
        }
        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() => this.Close());
        }
        public ThemBaoHiem(int MaNV, string MaBH, DateTime? NgayCap, string NoiCap, string GhiChu)
        {
            InitializeComponent();
            MaNV_BH.Text = MaNV.ToString();
            MaBHH.Text = MaBH;
            NgayCapBH.SelectedDate = NgayCap;
            NoiCapBH.Text = NoiCap;
            Note.Text = GhiChu;

            var viewMode = instanceFormAndNhanVien.Instance;
            this.DataContext = viewMode;
        }

        private void NoiCapBH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MaBH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
