using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WpfApp1.Model;
using System.Threading.Tasks;
using WpfApp1.View.NhanVien;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
using Microsoft.Win32;
using OfficeOpenXml;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Controls;
using System.IO;
using System.Net;
using System.Net.Mail;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;



namespace WpfApp1.ViewModel
{
    public class ChucNangThongTinNhanVienViewModel : INotifyPropertyChanged
    {
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;

            public RelayCommand(Action execute)
            {
                _execute = execute;
            }

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter) => _execute();

            public event EventHandler CanExecuteChanged;
        }

        //load dữ liệu nhân viên lên list view
        private ObservableCollection<WpfApp1.Model.NhanVien> _DanhSachNhanVien;
        public ObservableCollection<WpfApp1.Model.NhanVien> DanhSachNhanVien
        {
            get { return _DanhSachNhanVien; }
            set
            {
                _DanhSachNhanVien = value;
                OnPropertyChanged(nameof(DanhSachNhanVien));


            }
        }
        private ChucNangThongTinNhanVien _chucnangthongtinnhanvien;


        public void LoadFullDataNhanVienToListView()
        {
            var nhanviens = _chucnangthongtinnhanvien.GetNhanViens();
            Application.Current.Dispatcher.Invoke(() =>
            {
                DanhSachNhanVien.Clear();
                foreach (var nhanvien in nhanviens)
                {
                    DanhSachNhanVien.Add(nhanvien);
                }
            });
            

        }
        //Đưa dữ liệ vào combobox phòng ban, bộ phận và lọc phòng ban theo nhân viên
        public ObservableCollection<bophan> BoPhanComboBox { get; set; }
        private ObservableCollection<phongban> _PhongBanComboBox;
        public ObservableCollection<phongban> PhongBanComboBox
        {
            get { return _PhongBanComboBox; }
            set
            {
                _PhongBanComboBox = value;
                OnPropertyChanged(nameof(PhongBanComboBox));
            }
        }
        private bophan _ChonBoPhan;
        public bophan ChonBoPhan
        {
            get { return _ChonBoPhan; }
            set
            {
                _ChonBoPhan = value;
                OnPropertyChanged(nameof(ChonBoPhan));
                if (_ChonBoPhan != null)
                {
                    LocPhongBanTheoBoPhan(_ChonBoPhan.MaBP);

                }
                else
                {
                    DuaDuLieuVaoPhongBan();
                }
                LocNhanVienTheoPhongBanBoPhan();

            }
        }
        private phongban _ChonPhongBan;
        public phongban ChonPhongBan
        {
            get { return _ChonPhongBan; }
            set
            {
                _ChonPhongBan = value;
                OnPropertyChanged(nameof(_ChonPhongBan));
                LocNhanVienTheoPhongBanBoPhan();
            }
        }
        private void DuaDuLieuVaoBoPhan()
        {
            BoPhanComboBox = new ObservableCollection<bophan>(_chucnangthongtinnhanvien.GetBoPhan());
        }
        private void LocPhongBanTheoBoPhan(string mabp)
        {
            PhongBanComboBox = new ObservableCollection<phongban>(_chucnangthongtinnhanvien.GetPhongbansbyBoPhan(mabp));
        }
        private void DuaDuLieuVaoPhongBan()
        {
            PhongBanComboBox = new ObservableCollection<phongban>(_chucnangthongtinnhanvien.GetPhongBan());
        }
        // lọc lisview hiển thị thông tin nhân viên theo phòng ban và bộ phận
        private void LocNhanVienTheoPhongBanBoPhan()
        {
            var maPB = ChonPhongBan?.MaPB;
            var maBP = ChonBoPhan?.MaBP;
            var nhanViens2 = _chucnangthongtinnhanvien.GetNhanViensLoc(maBP, maPB);
            DanhSachNhanVien.Clear();
            foreach (var nhanVien12 in nhanViens2)
            {
                DanhSachNhanVien.Add(nhanVien12);
            }

        }
        //Nhấp vào ô trong lisview thì thông tin sẽ được hiển thị lên combobox và textbox
        private WpfApp1.Model.NhanVien _ChonListViewNhanVien;
        public WpfApp1.Model.NhanVien ChonListViewNhanVien
        {
            get { return _ChonListViewNhanVien; }
            set
            {
                _ChonListViewNhanVien = value;
                OnPropertyChanged(nameof(ChonListViewNhanVien));

            }
        }
        private int _maNV;
        private string _hoTen;
        private string _cccd;
        private string _maLuong;
        private string _sdt;
        private string _sex;
        private string _hocVan;
        private DateTime? _ngayKyHopDong;
        private DateTime? _ngayKetThucHopDong;
        private string _loaiHopDong;
        private string _adress;
        private int _age;
        private string _email;
        private string _note;

        public int MaNV
        {
            get { return _maNV; }
            set
            {

                _maNV = value;
                OnPropertyChanged(nameof(MaNV));

            }
        }

        public string HoTen
        {
            get { return _hoTen; }
            set
            {

                _hoTen = value;
                OnPropertyChanged(nameof(HoTen));

            }
        }

        public string CCCD
        {
            get { return _cccd; }
            set
            {

                _cccd = value;
                OnPropertyChanged(nameof(CCCD));

            }
        }

        public string MaLuong
        {
            get { return _maLuong; }
            set
            {

                _maLuong = value;
                OnPropertyChanged(nameof(MaLuong));

            }
        }



        public string SDT
        {
            get { return _sdt; }
            set
            {
                _sdt = value;
                OnPropertyChanged(nameof(SDT));

            }
        }

        public string SEX
        {
            get { return _sex; }
            set
            {

                _sex = value;
                OnPropertyChanged(nameof(SEX));

            }
        }

        public string HocVan
        {
            get { return _hocVan; }
            set
            {

                _hocVan = value;
                OnPropertyChanged(nameof(HocVan));

            }
        }

        public DateTime? NgayKyHopDong
        {
            get { return _ngayKyHopDong; }
            set
            {

                _ngayKyHopDong = value;
                OnPropertyChanged(nameof(NgayKyHopDong));

            }
        }

        public DateTime? NgayKetThucHopDong
        {
            get { return _ngayKetThucHopDong; }
            set
            {
                _ngayKetThucHopDong = value;
                OnPropertyChanged(nameof(NgayKetThucHopDong));

            }
        }

        public string LoaiHopDong
        {
            get { return _loaiHopDong; }
            set
            {

                _loaiHopDong = value;
                OnPropertyChanged(nameof(LoaiHopDong));

            }
        }

        public string Adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));

            }
        }

        public int Age
        {
            get { return _age; }
            set
            {

                _age = value;
                OnPropertyChanged(nameof(Age));

            }
        }

        public string Email
        {
            get { return _email; }
            set
            {

                _email = value;
                OnPropertyChanged(nameof(Email));

            }
        }

        public string Note
        {
            get { return _note; }
            set
            {

                _note = value;
                OnPropertyChanged(nameof(Note));

            }
        }
        public ICommand ThemNhanSu { get; }
        public void MoFormThemNhanSu()
        {
            NgayKyHopDong = null;
            NgayKetThucHopDong = null;
            MaLuong = "L001";
            Adress = "";
            Email = "";
            Note = "";
            MaNV = 0;
            HoTen = "";
            Age = 0;
            LoaiHopDong = "";
            CCCD = "";
            SDT = "";
            SEX = "";
            HocVan = "";
            var moform = new FormThemNhanVien();
            moform.ShowDialog();
        }
        public ICommand ThemNhanVien { get; }
        public ICommand CloseFormNhanVien { get; }

        private event Action _OnCloseRequested;
        public event Action OnCloseRequested
        {
            add { _OnCloseRequested += value; }
            remove { _OnCloseRequested -= value; }
        }

        private void CloseFormNhanVienFuc()
        {
            _OnCloseRequested?.Invoke();
        }

        public void DuaNhanVienVaoSql()

        {



            var nhanviennew = new WpfApp1.Model.NhanVien
            {
                NgayKyHopDong = NgayKyHopDong,
                NgayKetThucHopDong = NgayKetThucHopDong,
                MaLuong = "L001",
                Adress = Adress,
                Email = Email,
                Note = Note,
                MaNV = MaNV,
                HoTen = HoTen,
                MaBP = ChonBoPhan.MaBP,
                MaPB = ChonPhongBan.MaPB,
                Age = Age,
                LoaiHopDong = LoaiHopDong,
                CCCD = CCCD,
                SDT = SDT,
                SEX = SEX,
                HocVan = HocVan,


            };
           



            _chucnangthongtinnhanvien.AddNhanVienFromUser(nhanviennew);

            // Đọc lại danh sách nhân viên từ cơ sở dữ liệu (nếu cần thiết)
            var danhsachmoi = _chucnangthongtinnhanvien.GetNhanViens();

            // Cập nhật danh sách nhân viên
            DanhSachNhanVien.Clear();
            foreach (var nhanvien in danhsachmoi)
            {
                DanhSachNhanVien.Add(nhanvien);
            }
            LoadNhanVienSendMail.Clear();
            foreach (var nhanvien in danhsachmoi)
            {
                LoadNhanVienSendMail.Add(nhanvien);
            }
            MaNV_BaoHiem.Clear();
            foreach (var nhanvien in danhsachmoi)
            {
                MaNV_BaoHiem.Add(nhanvien);
            }
            NgayKyHopDong = null;
            NgayKetThucHopDong = null;
            MaLuong = "L001";
            Adress = "";
            Email = "";
            Note = "";
            MaNV = 0;
            HoTen = "";
            Age = 0;
            LoaiHopDong = "";
            CCCD = "";
            SDT = "";
            SEX = "";
            HocVan = "";





            _OnCloseRequested?.Invoke();


        }
        //reload ds
        public ICommand Reload_DsNV { get; }
        private void ReloadNhanVien()
        {
            ChonBoPhan = null;
        }
        // xóa nhân viên đưa vào nhân viên nghỉ việc

        private DateTime _NgayXoa;
        public DateTime NgayXoa
        {
            get { return _NgayXoa; }
            set
            {
                _NgayXoa = value;
                OnPropertyChanged(nameof(NgayXoa));
            }
        }
        private string _LyDo;
        public string LyDo
        {
            get { return _LyDo; }
            set
            {
                _LyDo = value;
                OnPropertyChanged(nameof(LyDo));
            }
        }
        private int _MaNV_Delete;
        public int MaNV_Delete
        {
            get { return _MaNV_Delete; }
            set
            {
                _MaNV_Delete = value;
                OnPropertyChanged(nameof(MaNV_Delete));
            }
        }
        private string _CCCD_Delete;
        public string CCCD_Delete
        {
            get { return _CCCD_Delete; }
            set
            {
                _CCCD_Delete = value;
                OnPropertyChanged(nameof(CCCD_Delete));
            }
        }
        public ICommand XoaNhanVien { get; set; }


        public void moformxoanhansu()
        {

            if (ChonListViewNhanVien != null)
            {
                MaNV_Delete = ChonListViewNhanVien.MaNV;
                CCCD_Delete = ChonListViewNhanVien.CCCD;
                var moform = new FormXoaNhanVien(MaNV_Delete, CCCD_Delete);
                // moform.DataContext = instanceFormAndNhanVien.Instance;  // Đảm bảo Form lấy đúng ViewModel

                moform.ShowDialog();

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trước khi xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        public void XoaNhanVienKhoiListNhanVien()
        {
            if (ChonListViewNhanVien != null)
            {
                MaNV_Delete = ChonListViewNhanVien.MaNV;
                CCCD_Delete = ChonListViewNhanVien.CCCD;
                _chucnangthongtinnhanvien.XoaNhanVien(MaNV_Delete, CCCD_Delete, LyDo, NgayXoa);
                LoadFullDataNhanVienToListView();
                MessageBox.Show("Xóa Nhân Viên Thành Công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                loadbaohiemtusql();
                loadnhanviennghivieclenlistview();
                if (_OnCloseRequeste != null)
                {
                    _OnCloseRequeste.Invoke();  // Gọi sự kiện đóng cửa sổ
                }
                else
                {
                    MessageBox.Show("Không thể đóng cửa sổ. Sự kiện chưa được đăng ký!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Nhân Viên Đã được xóa", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


        }
        public ICommand MoForm_XoaNhanSu { get; set; }
        private event Action _OnCloseRequeste;
        public event Action OnCloseRequeste
        {
            add { _OnCloseRequeste += value; }
            remove { _OnCloseRequeste -= value; }
        }

        public ICommand CloseForm_Delete { get; }
        public void CloseFormDelete()
        {
            _OnCloseRequeste?.Invoke();

        }
        // sửa thông tin nhân viên 
        private string _Sua_Phong_Ban;
        public string Sua_Phong_Ban
        {
            get { return _Sua_Phong_Ban; }
            set
            {
                _Sua_Phong_Ban = value;
                OnPropertyChanged(nameof(Sua_Phong_Ban));
            }
        }
        private string _Sua_Bo_Phan;
        public string Sua_Bo_Phan
        {
            get { return _Sua_Bo_Phan; }
            set
            {
                _Sua_Bo_Phan = value;
                OnPropertyChanged(nameof(Sua_Bo_Phan));
            }
        }
        public ICommand MoForm_SuaNhanSu { get; set; }
        public ICommand SuaNhanVien { get; }
        public ICommand CloseFormSuaNhanVien { get; }
        public void closeformsuanhanvien()
        {
            _OnCloseRequeste?.Invoke();


        }
        public void moformsuanhansu()
        {
            if (ChonListViewNhanVien != null)
            {
                MaNV = ChonListViewNhanVien.MaNV;
                HoTen = ChonListViewNhanVien.HoTen;
                CCCD = ChonListViewNhanVien.CCCD;
                MaLuong = ChonListViewNhanVien.MaLuong;
                Sua_Bo_Phan = _chucnangthongtinnhanvien.LayTenBoPhanTuMaBP(ChonListViewNhanVien.MaBP);
                Sua_Phong_Ban = _chucnangthongtinnhanvien.LayTenPhongBanTuMaPB(ChonListViewNhanVien.MaPB);
                SDT = ChonListViewNhanVien.SDT;
                SEX = ChonListViewNhanVien.SEX;
                HocVan = ChonListViewNhanVien.HocVan;
                NgayKyHopDong = ChonListViewNhanVien.NgayKyHopDong;
                NgayKetThucHopDong = ChonListViewNhanVien.NgayKetThucHopDong;
                LoaiHopDong = ChonListViewNhanVien.LoaiHopDong;
                Adress = ChonListViewNhanVien.Adress;
                Age = ChonListViewNhanVien.Age;
                Email = ChonListViewNhanVien.Email;
                Note = ChonListViewNhanVien.Note;

                var moform = new FormSuaThongTinNhanVien(MaNV, HoTen, CCCD, Sua_Bo_Phan, Sua_Phong_Ban, MaLuong, SDT, SEX, HocVan, NgayKyHopDong, NgayKetThucHopDong, LoaiHopDong, Adress, Age, Email, Note);
                moform.ShowDialog();

            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân sự cần sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        public void SuaNhanVienListView()
        {
            var nhanvien = new WpfApp1.Model.NhanVien
            {

                NgayKyHopDong = NgayKyHopDong,
                NgayKetThucHopDong = NgayKetThucHopDong,
                MaLuong = "L001",
                Adress = Adress,
                Email = Email,
                Note = Note,
                MaNV = MaNV,
                HoTen = HoTen,
                MaBP = _chucnangthongtinnhanvien.LayMaBoPhanTuTenBoPhan(Sua_Bo_Phan),
                MaPB = _chucnangthongtinnhanvien.LayMaPhongBanTuMaPB(Sua_Phong_Ban),
                Age = Age,
                LoaiHopDong = LoaiHopDong,
                CCCD = CCCD,
                SDT = SDT,
                SEX = SEX,
                HocVan = HocVan,
            };
            _chucnangthongtinnhanvien.SuaThongTinNhanVien(nhanvien);
            LoadFullDataNhanVienToListView();
            MessageBox.Show("Sửa Nhân Viên Thành Công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            NgayKyHopDong = null;
            NgayKetThucHopDong = null;
            MaLuong = "L001";
            Adress = "";
            Email = "";
            Note = "";
            MaNV = 0;
            HoTen = "";
            Age = 0;
            LoaiHopDong = "";
            CCCD = "";
            SDT = "";
            SEX = "";
            HocVan = "";

            _OnCloseRequeste?.Invoke();

        }
        // xuất nhân viên ra exel
        public ICommand XuatNhanVien_exel { get; set; }
        public void XuatNhanVienRaExel(ListView listView)
        {
            // 1. Tạo hộp thoại lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Chọn nơi lưu file Excel",
                Filter = "Excel Files|*.xlsx",
                FileName = "DanhSachNhanVien.xlsx"
            };

            // 2. Nếu người dùng chọn nơi lưu file
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName; // Lấy đường dẫn file

                // 3. Thiết lập EPPlus
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var workbook = excel.Workbook; // Khởi tạo workbook
                    var worksheet = workbook.Worksheets.Add("Nhân Viên"); // Tạo worksheet với tên tùy chỉnh

                    // 4. Kiểm tra dữ liệu từ ListView
                    if (listView.Items.Count > 0)
                    {
                        var items = listView.ItemsSource as IEnumerable<dynamic>;
                        if (items != null && items.Any())
                        {
                            int row = 1;
                            int col = 1;

                            // 5. Ghi tiêu đề cột
                            foreach (var prop in items.First().GetType().GetProperties())
                            {
                                worksheet.Cells[row, col].Value = prop.Name;
                                col++;
                            }

                            // 6. Ghi dữ liệu từng dòng
                            foreach (var item in items)
                            {
                                row++;
                                col = 1;
                                foreach (var prop in item.GetType().GetProperties())
                                {
                                    worksheet.Cells[row, col].Value = prop.GetValue(item, null);
                                    col++;
                                }
                            }
                        }
                    }

                    // 7. Lưu file Excel
                    FileInfo excelFile = new FileInfo(filePath);
                    excel.SaveAs(excelFile);

                    // 8. Thông báo thành công
                    MessageBox.Show($"Xuất file Excel thành công!\nLưu tại: {filePath}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        // gửi mail cho nhân sự

        private ObservableCollection<WpfApp1.Model.NhanVien> _LoadNhanVienSendMail;
        public ObservableCollection<WpfApp1.Model.NhanVien> LoadNhanVienSendMail
        {
            get { return _LoadNhanVienSendMail; }
            set
            {
                _LoadNhanVienSendMail = value;
                OnPropertyChanged(nameof(LoadNhanVienSendMail));
            }
        }
        public void LoadTenNVLenEmail()
        {
            LoadNhanVienSendMail = new ObservableCollection<Model.NhanVien>(_chucnangthongtinnhanvien.GetNhanViens());

        }
        private string _EmailNhanVien;
        private WpfApp1.Model.NhanVien _ChonNhanVienGuiMail;
        public string EmailNhanVien
        {
            get { return _EmailNhanVien; }
            set
            {
                _EmailNhanVien = value;
                OnPropertyChanged(nameof(EmailNhanVien));
            }
        }
        public WpfApp1.Model.NhanVien ChonNhanVienGuiMail
        {
            get { return _ChonNhanVienGuiMail; }
            set
            {
                _ChonNhanVienGuiMail = value;
 
                OnPropertyChanged(nameof(ChonNhanVienGuiMail));
                if(_ChonNhanVienGuiMail != null)
                {
                    LayMailTuTenNhanVien();

                } else
                {
                    EmailNhanVien = "";
                }
            }
        }
        private string _NoiDungEmail;
        public string NoiDungEmail
        {
            get { return _NoiDungEmail; }
            set
            {
                _NoiDungEmail = value;
                OnPropertyChanged(nameof(NoiDungEmail));
            }
        }
      
        public ICommand GuiEmailCommand { get; }
        public ICommand LamMoiNhanVienGuiMail { get; }


        public void LayMailTuTenNhanVien()
        {
            EmailNhanVien = _chucnangthongtinnhanvien.LayRaEmailDuaVaoTenNhanVien(ChonNhanVienGuiMail.HoTen);
        }
        public void GuiEmail()
        {
            if (string.IsNullOrEmpty(EmailNhanVien))
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi gửi email!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(NoiDungEmail))
            {
                MessageBox.Show("Vui lòng nhập nội dung email!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string emailNguoiGui = "ln.hako.vn@gmail.com"; // Thay bằng email của bạn
                string matKhau = "ukbbhymsttworwoi"; // Thay bằng mật khẩu hoặc App Password

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailNguoiGui);
                mail.To.Add(EmailNhanVien);
                mail.Subject = "Thông báo từ hệ thống";
                mail.Body = NoiDungEmail;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(emailNguoiGui, matKhau);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("Gửi email thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // làm mới nhân viên gửi mail




        // Quản lý bảo hiểm
        public void LamMoiGmail()
        {
            ChonNhanVienGuiMail = null;
        }
        //Thêm bảo hiểm
        private ObservableCollection<Model.NhanVien> _MaNV_BaoHiem;
        public ObservableCollection<Model.NhanVien> MaNV_BaoHiem
        {
            get { return _MaNV_BaoHiem; }
            set
            {
                _MaNV_BaoHiem = value;
                OnPropertyChanged(nameof(MaNV_BaoHiem));
            }
        }
        private Model.NhanVien _MaNV_BaoHiem_Chon;
        public Model.NhanVien MaNV_BaoHiem_Chon
        {
            get { return _MaNV_BaoHiem_Chon; }
            set 
            {
                _MaNV_BaoHiem_Chon = value;
                OnPropertyChanged(nameof(MaNV_BaoHiem_Chon));

            }
        }
        private ObservableCollection<BaoHiem> _ThemBHListView;
        public ObservableCollection<BaoHiem> ThemBHListView
        {
            get { return _ThemBHListView; }
            set
            {
                _ThemBHListView = value;
                OnPropertyChanged(nameof(ThemBHListView));
            }
        }
        private string _MaBH;
        public string MaBH
        {
            get { return _MaBH; }
            set
            {
                _MaBH = value;
                OnPropertyChanged(nameof(MaBH));
            }
        }
        private DateTime? _NgayCapBH;
        public DateTime? NgayCapBH
        {
            get { return _NgayCapBH; }
            set
            {
                _NgayCapBH = value;
                OnPropertyChanged(nameof(NgayCapBH));
            }
        }
        private string _NoiCapBH;
        public string NoiCapBH
        {
            get { return _NoiCapBH; }
            set
            {
                _NoiCapBH = value;
                OnPropertyChanged(nameof(NoiCapBH));
            }
        }
        private string _GhiChuBH;
        public string GhiChuBH
        {
            get { return _GhiChuBH; }
            set
            {
                _GhiChuBH = value;
                OnPropertyChanged(nameof(GhiChuBH));
            }
        }
        public void loadbaohiemtusql()
        {
            var baohiems = _chucnangthongtinnhanvien.GetBH();
            Application.Current.Dispatcher.Invoke(() =>
            {
                ThemBHListView.Clear();
                foreach (var baohiem in baohiems)
                {
                    ThemBHListView.Add(baohiem);
                }
            });
        }
        public void LoadNhanVienChonBaoHiem()
        {
            MaNV_BaoHiem = new ObservableCollection<Model.NhanVien>(_chucnangthongtinnhanvien.GetNhanViens());
        }
        public ICommand ThemBHX { get; set; }
        public ICommand Them_Bao_Hiem { get; }
       public ICommand CloseFormBH { get; }
       
        public void thembh()
        {
            var baohiem = new BaoHiem
            {
                MaBH = MaBH,
                MaNV = MaNV_BaoHiem_Chon.MaNV,
                NgayCap = NgayCapBH,
                NoiCap = NoiCapBH,
                GhiChu = GhiChuBH,

            };
            var bhne = _chucnangthongtinnhanvien.GetBH();
            foreach (var item in bhne)
            {
                if (item.MaNV == baohiem.MaNV)
                {
                    MessageBox.Show("Nhân Viên Đã Tồn Tại Mã Bảo Hiểm", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MaBH = "";
                    MaNV_BaoHiem_Chon = null;
                    NgayCapBH = null;
                    NoiCapBH = "";
                    GhiChuBH = "";
                    return;

                }


            }

            _chucnangthongtinnhanvien.ThemThongTinBaoHiem(baohiem);
            var bhnew = _chucnangthongtinnhanvien.GetBH();
           
            if (ThemBHListView != null)
            {
                ThemBHListView.Clear();
                foreach (var bh in bhnew)
                {
                    ThemBHListView.Add(bh);
                }

            }
            else
            {
                foreach (var bh in bhnew)
                {
                    ThemBHListView.Add(bh);
                }

            }
            MaBH = "";
            MaNV_BaoHiem_Chon = null;
            NgayCapBH = null;
            NoiCapBH = "";
            GhiChuBH = "";
            _OnCloseRequeste?.Invoke();



        }

        public void closefrombh()
        {
            _OnCloseRequeste?.Invoke();


        }
        public void openFormThemBH()
        {
            MaBH = "";
            MaNV_BaoHiem_Chon = null;
            NgayCapBH = null;
            NoiCapBH = "";
            GhiChuBH = "";
            var form = new ThemBaoHiem();
            form.ShowDialog();
        }
        // sửa bảo hiểm
        private BaoHiem _ChonBH;
        public BaoHiem ChonBH
        {
            get { return _ChonBH; }
            set
            {
                _ChonBH = value;
                OnPropertyChanged(nameof(ChonBH));
            }

        }
        private int _MaNhanVien;
        public int MaNhanVien
        {
            get { return _MaNhanVien; }
            set
            {
                _MaNhanVien = value;
                OnPropertyChanged(nameof(_MaNhanVien));
            }
        }

        public ICommand SuaBHX { get; }
        public ICommand Sua_Bao_Hiem { get; }

        public void moformsuabaohiem()
        {
            if (ChonBH != null)
            {
                MaNV = ChonBH.MaNV;
                MaBH = ChonBH.MaBH;
                NgayCapBH = ChonBH.NgayCap;
                NoiCapBH = ChonBH.NoiCap;
                Note = ChonBH.GhiChu;
                var open = new FormSuaBaoHiem(MaBH, MaNV, NgayCapBH, NoiCapBH, Note);
                open.ShowDialog();

            } else
            {
                MessageBox.Show("Vui lòng chọn bảo hiểm cần sửa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            
        }
        public void suathongtinbaohiem()
        {
            var baohiem = new BaoHiem
            {
                MaBH = MaBH,
                MaNV = ChonBH.MaNV,
                NgayCap = NgayCapBH,
                NoiCap = NoiCapBH,
                GhiChu = GhiChuBH,

            };
            _chucnangthongtinnhanvien.SuaBaoHiem(baohiem);
            loadbaohiemtusql();
            MessageBox.Show("Sửa Bảo Hiểm Thành Công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            _OnCloseRequeste?.Invoke();



        }
        // xóa bảo hiểm
        public ICommand XoaBaoHiem { get; }
        public void xoabaohiem()
        {
            if(ChonBH == null)
            {
                MessageBox.Show("Vui Lòng Chọn Bảo Hiểm Cần Xóa", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            } else
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa mục này?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButton.OKCancel,
                                                 MessageBoxImage.Warning);
                if(result == MessageBoxResult.OK)
                {
                    _chucnangthongtinnhanvien.XoaBaoHiem(ChonBH.MaNV);
                    loadbaohiemtusql();

                }
            }
        }
        // xem chi tiết bảo hiểm 
        private string _HoTenBH;
        public string HoTenBH
        {
            get { return _HoTenBH; }
            set
            {
                _HoTenBH = value;
                OnPropertyChanged(nameof(HoTenBH));
            }
        }
        public ICommand XemChiTietBH { get; }
        public void xemchitietbaohiem()
        {
            if(ChonBH != null)
            {
                MaNV = ChonBH.MaNV;
                MaBH = ChonBH.MaBH;
                HoTenBH = _chucnangthongtinnhanvien.GetTenNhanVien(MaNV);
                NgayCapBH = ChonBH.NgayCap;
                NoiCapBH = ChonBH.NoiCap;
                GhiChuBH = ChonBH.GhiChu;
                var open = new FormChiTietBaoHiem(MaNV, HoTenBH, MaBH, NgayCapBH, NoiCapBH, GhiChuBH);
                open.ShowDialog();

            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Bảo Hiểm Muốn Xem", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        
        // quản lý nhân viên thử việc nghỉ việc
        //nhân viên nghỉ việc
        private ObservableCollection<nhanviendoiviec> _ListViewNhanVienNghiViec;
        public ObservableCollection<nhanviendoiviec> ListViewNhanVienNghiViec
        {
            get { return _ListViewNhanVienNghiViec; }
            set
            {
                _ListViewNhanVienNghiViec = value;
                OnPropertyChanged(nameof(ListViewNhanVienNghiViec));
            }
        }
        public void loadnhanviennghivieclenlistview()
        {
            var nhanviens = _chucnangthongtinnhanvien.GetNhanVienDoiViecs();
            Application.Current.Dispatcher.Invoke(() =>
            {
                ListViewNhanVienNghiViec.Clear();
                foreach (var nhanvien in nhanviens)
                {
                    ListViewNhanVienNghiViec.Add(nhanvien);
                }
            });

        }
        // nhân viên thử việc
        // thêm nhân viên thử việc
        private int _MaNVTV;
        public int MaNVTV
        {
            get { return _MaNVTV; }
            set
            {
                _MaNVTV = value;
                OnPropertyChanged(nameof(MaNVTV));
            }
        }

        private string _HoTenTV;
        public string HoTenTV
        {
            get { return _HoTenTV; }
            set
            {
                _HoTenTV = value;
                OnPropertyChanged(nameof(HoTenTV));
            }
        }

        private string _SexTV;
        public string SexTV
        {
            get { return _SexTV; }
            set
            {
                _SexTV = value;
                OnPropertyChanged(nameof(SexTV));
            }
        }

        private string _CCCDTV;
        public string CCCDTV
        {
            get { return _CCCDTV; }
            set
            {
                _CCCDTV = value;
                OnPropertyChanged(nameof(CCCDTV));
            }
        }

        private string _VTTV;
        public string VTTV
        {
            get { return _VTTV; }
            set
            {
                _VTTV = value;
                OnPropertyChanged(nameof(VTTV));
            }
        }

        private string _HocVanTV;
        public string HocVanTV
        {
            get { return _HocVanTV; }
            set
            {
                _HocVanTV = value;
                OnPropertyChanged(nameof(HocVanTV));
            }
        }

        private DateTime? _NgayBatDauTV;
        public DateTime? NgayBatDauTV
        {
            get { return _NgayBatDauTV; }
            set
            {
                _NgayBatDauTV = value;
                OnPropertyChanged(nameof(NgayBatDauTV));
            }
        }

        private DateTime? _NgayKetThucTV;
        public DateTime? NgayKetThucTV
        {
            get { return _NgayKetThucTV; }
            set
            {
                _NgayKetThucTV = value;
                OnPropertyChanged(nameof(NgayKetThucTV));
            }
        }

        private string _TruongDaiHocTV;
        public string TruongDaiHocTV
        {
            get { return _TruongDaiHocTV; }
            set
            {
                _TruongDaiHocTV = value;
                OnPropertyChanged(nameof(TruongDaiHocTV));
            }
        }

        private string _SDTTV;
        public string SDTTV
        {
            get { return _SDTTV; }
            set
            {
                _SDTTV = value;
                OnPropertyChanged(nameof(SDTTV));
            }
        }

        private string _EmailTV;
        public string EmailTV
        {
            get { return _EmailTV; }
            set
            {
                _EmailTV = value;
                OnPropertyChanged(nameof(EmailTV));
            }
        }

        private string _NoteTV;
        public string NoteTV
        {
            get { return _NoteTV; }
            set
            {
                _NoteTV = value;
                OnPropertyChanged(nameof(NoteTV));
            }
        }
        private ObservableCollection<quanlythuviec> _ListViewNhanVienTV;
        public ObservableCollection<quanlythuviec> ListViewNhanVienTV
        {
            get { return _ListViewNhanVienTV; }
            set
            {
                _ListViewNhanVienTV = value;
                OnPropertyChanged(nameof(ListViewNhanVienTV));
            }
        }
        public void LoadNhanVienThuViecLenListView()
        {
            var baohiems = _chucnangthongtinnhanvien.GetDanhSachThuViec();
            Application.Current.Dispatcher.Invoke(() =>
            {
                ListViewNhanVienTV.Clear();
                foreach (var baohiem in baohiems)
                {
                    ListViewNhanVienTV.Add(baohiem);
                }
            });

        }
        public ICommand MoFormThemNhanVienTV { get; }
        public void moformthemnhanvienthuviec()
        {
            var open = new FormThemNhanVienThuViec();
            open.ShowDialog();
        }
        public ICommand ThemNVTV { get; }
        public void DuaNhanVienVaoThuViec()
        {
            var nhanVienTV = new quanlythuviec
            {
                MaNVT = MaNVTV,
                HoTen = HoTenTV,
                Sex = SexTV,
                CCCD = CCCDTV,
                SDT = SDTTV,
                ViTriTV = VTTV,
                HocVan = HocVanTV,
                NgayBatDau = NgayBatDauTV,
                NgayKetThuc = NgayKetThucTV,
                TruongDaiHoc = TruongDaiHocTV,
                Email = EmailTV,
                Note = NoteTV
            };

            _chucnangthongtinnhanvien.AddQuanLyThuViec(nhanVienTV);

            var danhSachMoiTV = _chucnangthongtinnhanvien.GetDanhSachThuViec();

            ListViewNhanVienTV.Clear();
            foreach (var nv in danhSachMoiTV)
            {
                ListViewNhanVienTV.Add(nv);
            }

            MaNVTV = 0;
            HoTenTV = "";
            SexTV = "";
            CCCDTV = "";
            SDTTV = "";
            VTTV = "";
            HocVanTV = "";
            NgayBatDauTV = null;
            NgayKetThucTV = null;
            TruongDaiHocTV = "";
            EmailTV= "";
            NoteTV = "";

            _OnCloseRequeste?.Invoke();
        }
        // xóa nhân viên thử việc

        private quanlythuviec _ChonNhanVienThuViec;
        public quanlythuviec ChonNhanVienThuViec
        {
            get { return _ChonNhanVienThuViec; }
            set
            {
                _ChonNhanVienThuViec = value;
                OnPropertyChanged(nameof(ChonNhanVienThuViec));
            }
        }
        public ICommand XoaNhanVienTV { get; }
        public void xoanhanvienthuviec()
        {
            if (ChonNhanVienThuViec == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa mục này?",
                                                      "Xác nhận xóa",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _chucnangthongtinnhanvien.XoaNhanVienThuViec(ChonNhanVienThuViec.MaNVT);
                LoadNhanVienThuViecLenListView();


            }

        }





        // constructor xử lý
        public ChucNangThongTinNhanVienViewModel(ObservableCollection<WpfApp1.Model.NhanVien> danhSachCu = null)
        {
            _chucnangthongtinnhanvien = new ChucNangThongTinNhanVien();

            DanhSachNhanVien = danhSachCu ?? new ObservableCollection<WpfApp1.Model.NhanVien>();
            LoadFullDataNhanVienToListView();
            DuaDuLieuVaoBoPhan();
            DuaDuLieuVaoPhongBan();
            ThemNhanSu = new RelayCommand(MoFormThemNhanSu);
            ThemNhanVien = new RelayCommand(DuaNhanVienVaoSql);
            Reload_DsNV = new RelayCommand(ReloadNhanVien);
            CloseFormNhanVien = new RelayCommand(CloseFormNhanVienFuc);
            MoForm_XoaNhanSu = new RelayCommand(moformxoanhansu);
            XoaNhanVien = new RelayCommand(XoaNhanVienKhoiListNhanVien);
            CloseForm_Delete = new RelayCommand(CloseFormDelete);
            MoForm_SuaNhanSu = new RelayCommand(moformsuanhansu);
            SuaNhanVien = new RelayCommand(SuaNhanVienListView);
            CloseFormSuaNhanVien = new RelayCommand(closeformsuanhanvien);
            XuatNhanVien_exel = new RelayCommand<ListView>(XuatNhanVienRaExel);
            LoadNhanVienSendMail = new ObservableCollection<Model.NhanVien>();
            LoadTenNVLenEmail();
            GuiEmailCommand = new RelayCommand(GuiEmail);
            LamMoiNhanVienGuiMail = new RelayCommand(LamMoiGmail);
            ThemBHListView = new ObservableCollection<BaoHiem>();
            loadbaohiemtusql();
            LoadNhanVienChonBaoHiem();
            LoadNhanVienChonBaoHiem();
            Them_Bao_Hiem = new RelayCommand(openFormThemBH);
            CloseFormBH = new RelayCommand(closefrombh);
            ThemBHX = new RelayCommand(thembh);
            Sua_Bao_Hiem = new RelayCommand(moformsuabaohiem);
            SuaBHX = new RelayCommand(suathongtinbaohiem);
            XoaBaoHiem = new RelayCommand(xoabaohiem);
            XemChiTietBH = new RelayCommand(xemchitietbaohiem);
            ListViewNhanVienNghiViec = new ObservableCollection<nhanviendoiviec>();
            loadnhanviennghivieclenlistview();
            ListViewNhanVienTV = new ObservableCollection<quanlythuviec>();
            LoadNhanVienThuViecLenListView();
            MoFormThemNhanVienTV = new RelayCommand(moformthemnhanvienthuviec);
            ThemNVTV = new RelayCommand(DuaNhanVienVaoThuViec);
            XoaNhanVienTV = new RelayCommand(xoanhanvienthuviec);






        }
        public class RelayCommand<T> : ICommand
        {
            private readonly Action<T> _execute;
            private readonly Predicate<T> _canExecute;

            public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

            public void Execute(object parameter) => _execute((T)parameter);

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }




        // hàm nhận sự thay đổi của ui
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
