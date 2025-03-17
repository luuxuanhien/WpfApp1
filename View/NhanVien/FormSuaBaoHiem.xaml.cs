using DocumentFormat.OpenXml.Wordprocessing;
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
    /// Interaction logic for FormSuaBaoHiem.xaml
    /// </summary>
    public partial class FormSuaBaoHiem : Window
    {
        public FormSuaBaoHiem()
        {
            InitializeComponent();
        }
        public FormSuaBaoHiem(string MaBH, int MaNV, DateTime? ngaycap, string noicap, string ghichu)
        {
            InitializeComponent();
            MaNV_BH.Text = MaNV.ToString();
            MaBHH.Text = MaBH;
            NgayCapBH.SelectedDate = ngaycap;
            NoiCapBH.Text = noicap;
            Note.Text = ghichu;
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

      
    }
}
