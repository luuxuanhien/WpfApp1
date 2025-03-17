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

namespace WpfApp1.View.NhanVien
{
    /// <summary>
    /// Interaction logic for FormChiTietBaoHiem.xaml
    /// </summary>
    public partial class FormChiTietBaoHiem : Window
    {
        public FormChiTietBaoHiem()
        {
            InitializeComponent();
        }
        public FormChiTietBaoHiem(int manv, string hoten, string mabh, DateTime? ngay, string noicap, string ghichu)
        {
            InitializeComponent();
            MaNhanVien.Text = manv.ToString();
            HoTen.Text = hoten;
            MaBaoHiem.Text = mabh;
            NgayCap.SelectedDate = ngay;
            NoiCap.Text = noicap;
            GhiChu.Text = ghichu;
            var viewMode = instanceFormAndNhanVien.Instance;
            this.DataContext = viewMode;


        }

       
    }
}
