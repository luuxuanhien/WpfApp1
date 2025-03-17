using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WpfApp1.Model;
using System.Threading.Tasks;
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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Net.Mime.MediaTypeNames.Application;
using DocumentFormat.OpenXml.ExtendedProperties;
using WpfApp1.View.bophan;



namespace WpfApp1.ViewModel
{
    public class bophanphongban: INotifyPropertyChanged
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
        private ChucNangThongTinNhanVien _chucnangthongtinnhanvien;
       //load bộ phận vào comboxbox phòn ban


        private ObservableCollection<WpfApp1.Model.bophan> _PhongBanThuocBP;
        public ObservableCollection<WpfApp1.Model.bophan> PhongBanThuocBP
        {
            get { return _PhongBanThuocBP; }
            set
            {
                _PhongBanThuocBP = value;
                OnPropertyChanged(nameof(PhongBanThuocBP));
            }
        }
        public void loadbophanlenphongban()
        {
            PhongBanThuocBP = new ObservableCollection<WpfApp1.Model.bophan>(_chucnangthongtinnhanvien.GetBoPhan());

        }

        //load bộ phân lên list view
        private ObservableCollection<WpfApp1.Model.bophan> _LoadBoPhanListView;
        public ObservableCollection<WpfApp1.Model.bophan> LoadBoPhanListView
        {
            get { return _LoadBoPhanListView; }
            set
            {
                _LoadBoPhanListView = value;
                OnPropertyChanged(nameof(LoadBoPhanListView));
            }
        }
        public void loadbophanlenlistview()
        {
            var baohiems = _chucnangthongtinnhanvien.GetAllBoPhan();
          
                LoadBoPhanListView.Clear();
                foreach (var baohiem in baohiems)
                {
                    LoadBoPhanListView.Add(baohiem);
                }
           

        }
        // load phòng ban lên list view
        private ObservableCollection<WpfApp1.Model.phongban> _LoadPhongBanListView;
        public ObservableCollection<WpfApp1.Model.phongban> LoadPhongBanListView
        {
            get { return _LoadPhongBanListView; }
            set
            {
                _LoadPhongBanListView = value;
                OnPropertyChanged(nameof(LoadPhongBanListView));
            }
        }
        public void loadphongbanlenlistview()
        {
            var baohiems = _chucnangthongtinnhanvien.GetAllPhongBan();

            LoadPhongBanListView.Clear();
            foreach (var baohiem in baohiems)
            {
                LoadPhongBanListView.Add(baohiem);
            }

        }
        // thêm bộ phận
        private string _maBP;
        private string _tenBP;
        private DateTime? _ngayThanhLapBP;
        private string _note;

        public string MaBP
        {
            get => _maBP;
            set { _maBP = value; OnPropertyChanged(nameof(MaBP)); }
        }

        public string TenBP
        {
            get => _tenBP;
            set { _tenBP = value; OnPropertyChanged(nameof(TenBP)); }
        }

        public DateTime? NgayThanhLapBP
        {
            get => _ngayThanhLapBP;
            set { _ngayThanhLapBP = value; OnPropertyChanged(nameof(NgayThanhLapBP)); }
        }

        public string NoteBP
        {
            get => _note;
            set { _note = value; OnPropertyChanged(nameof(NoteBP)); }
        }
        public ICommand ThemBoPhanLV { get; }
        public void thembophan()
        {
           
            var boPhanMoi = new WpfApp1.Model.bophan
            {
                MaBP = MaBP,
                TenBP = TenBP,
                NgayThanhLap = NgayThanhLapBP,
                Note = NoteBP
            };

            _chucnangthongtinnhanvien.ThemBoPhan(boPhanMoi);
            loadbophanlenlistview();
            loadbophanlenphongban();

            MaBP = "";
            TenBP = "";
            NgayThanhLapBP = null;
            NoteBP = "";
        

        }
        // sửa bộ phận
        private WpfApp1.Model.bophan _ChonBoPhanListView;
        public WpfApp1.Model.bophan ChonBoPhanListView
        {
            get { return _ChonBoPhanListView; }
            set
            {
                _ChonBoPhanListView = value;
                OnPropertyChanged(nameof(ChonBoPhanListView));
                CapNhatTextBox();
            }
        }
        private void CapNhatTextBox()
        {
            if (ChonBoPhanListView != null)
            {
                MaBP = ChonBoPhanListView.MaBP;
                TenBP = ChonBoPhanListView.TenBP;
                NgayThanhLapBP = ChonBoPhanListView.NgayThanhLap;
            }
        }
        public ICommand SuaBoPhanLV { get; }
        public void suanhansu()
        {
            var boPhanMoi = new WpfApp1.Model.bophan
            {
                MaBP = MaBP,
                TenBP = TenBP,
                NgayThanhLap = NgayThanhLapBP,
                Note = NoteBP
            };

            _chucnangthongtinnhanvien.SuaBoPhan(boPhanMoi);
            loadbophanlenlistview();

            MaBP = "";
            TenBP = "";
            NgayThanhLapBP = null;
            NoteBP = "";



        }
        // làm mới bộ phận
        public ICommand LamMoiBP { get; }
        public void lammoibp()
        {
            MaBP = "";
            TenBP = "";
            NgayThanhLapBP = null;
            NoteBP = "";

        }
        // thêm phòng ban
        private string _maPB;
      
        private string _tenPB;
        private DateTime? _ngayThanhLapPB;

        private WpfApp1.Model.bophan _ChonPhongBanThuocBP;
        public WpfApp1.Model.bophan ChonPhongBanThuocBP
        {
            get { return _ChonPhongBanThuocBP; }
            set
            {
                _ChonPhongBanThuocBP = value;
                OnPropertyChanged(nameof(ChonPhongBanThuocBP));
                
                

            }
        }
        public string MaPB
        {
            get => _maPB;
            set
            {
                _maPB = value;
                OnPropertyChanged(nameof(MaPB));
            }
        }

     

        public string TenPB
        {
            get => _tenPB;
            set
            {
                _tenPB = value;
                OnPropertyChanged(nameof(TenPB));
            }
        }

        public DateTime? NgayThanhLapPB
        {
            get => _ngayThanhLapPB;
            set
            {
                _ngayThanhLapPB = value;
                OnPropertyChanged(nameof(NgayThanhLapPB));
            }
        }
        public ICommand ThemPhongBan { get; }
        public ICommand SuaPhongBan { get; }
        public ICommand LamMoiPB { get; }
        public void themphongban()
        {
            var boPhanMoi = new WpfApp1.Model.phongban
            {
                MaBP = ChonPhongBanThuocBP.MaBP,
                MaPB = MaPB,
                TenPB = TenPB,
                NgayThanhLap = NgayThanhLapPB,
                Note = NoteBP
            };

            _chucnangthongtinnhanvien.ThemPhongBan(boPhanMoi);
            loadphongbanlenlistview();
           

            MaPB = "";
            TenPB = "";
            NgayThanhLapPB = null;
            ChonPhongBanThuocBP = null;
            

        }
        private WpfApp1.Model.phongban _ChonPhongBanListView;
        public WpfApp1.Model.phongban ChonPhongBanListView
        {
            get { return _ChonPhongBanListView; }
            set
            {
                _ChonPhongBanListView = value;
                OnPropertyChanged(nameof(ChonPhongBanListView));
                if (_ChonPhongBanListView != null)
                {
                    chonlistviewphongban();
                }
               
                
            }
        }
        // suaphongban
        public void suaphongban()
        {
            var boPhanMoi = new WpfApp1.Model.phongban
            {
                MaBP = ChonPhongBanThuocBP.MaBP,
                MaPB = MaPB,
                TenPB = TenPB,
                NgayThanhLap = NgayThanhLapPB,
                Note = NoteBP
            };

            _chucnangthongtinnhanvien.SuaPhongBan(boPhanMoi);
            loadphongbanlenlistview();


            MaPB = "";
            TenPB = "";
            NgayThanhLapPB = null;
            ChonPhongBanThuocBP = null;
        }
        private void chonlistviewphongban()
        {
            ChonPhongBanThuocBP = PhongBanThuocBP.FirstOrDefault(bp=>bp.MaBP == ChonPhongBanListView.MaBP);
            MaPB = ChonPhongBanListView.MaPB;
            TenPB = ChonPhongBanListView.TenPB;
            NgayThanhLapPB = ChonPhongBanListView.NgayThanhLap;
        }
        // làm mới
        public void lammoipb()
        {
            MaPB = "";
            TenPB = "";
            NgayThanhLapPB = null;
            ChonPhongBanThuocBP = null;
            ChonPhongBanListView = null;

        }

        // contructor
        public bophanphongban()
        {
            _chucnangthongtinnhanvien = new ChucNangThongTinNhanVien();
            PhongBanThuocBP = new ObservableCollection<WpfApp1.Model.bophan>();
            loadbophanlenphongban();
            LoadBoPhanListView = new ObservableCollection<WpfApp1.Model.bophan>();
            loadbophanlenlistview();
            LoadPhongBanListView = new ObservableCollection<WpfApp1.Model.phongban>();
            loadphongbanlenlistview();

            ThemBoPhanLV = new RelayCommand(thembophan);
            SuaBoPhanLV = new RelayCommand(suanhansu);
            LamMoiBP = new RelayCommand(lammoibp);
            ThemPhongBan = new RelayCommand(themphongban);
            SuaPhongBan = new RelayCommand(suaphongban);
            LamMoiPB = new RelayCommand(lammoipb);
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
