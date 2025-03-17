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
    /// Interaction logic for FormSuaThongTinNhanVien.xaml
    /// </summary>
    public partial class FormSuaThongTinNhanVien : Window
    {
        public FormSuaThongTinNhanVien()
        {
            InitializeComponent();
          
        }
        public FormSuaThongTinNhanVien(int MaNV, string hoten, string cccd, string mabp, string mapb, string maluong,  string sdt, string sex, string hocvan, DateTime? ngayky, DateTime? ngayend, string loaihd, string diachi, int age, string email, string note)
        {
            InitializeComponent();


            MaNhanVien.Text = MaNV.ToString();
            PhongBan.Text = mapb;
            maLuong.Text = maluong;
            HoTen.Text = hoten;
            CCCD.Text = cccd;
            SDT.Text = sdt;
            SEX.Text = sex;
            HocVan.Text = hocvan;
            Adress.Text = diachi;
            NgayKy.SelectedDate = ngayky;
            NgayHet.SelectedDate = ngayend;
            Email.Text = email;
            Age.Text = age.ToString();
            Note.Text = note;
            LoaiHopDong.Text = loaihd;
            bophan.Text = mabp;
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
