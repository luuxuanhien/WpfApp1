using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
namespace WpfApp1.ViewModel
{
  

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public class loginviewmodel: INotifyPropertyChanged
    {
        public Action CloseLoginWindowAction { get; set; }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private string _tenuser;
        public string TenUser
        {
            get { return _tenuser; }
            set { _tenuser = value; OnPropertyChanged(nameof(TenUser)); }
        }
        private string _tennhanvien;
        public string TenNhanVien
        {
            get { return _tennhanvien; }
            set { _tennhanvien = value; OnPropertyChanged(nameof(TenNhanVien)); }
        }
        private string _quyenhan;
        public string QuyenHan
        {
            get { return _quyenhan; }
            set { _quyenhan = value; OnPropertyChanged(nameof(QuyenHan)); }
        }


        private loginmodel _loginservice;
        public ICommand LoginCommand { get; }
        public loginviewmodel()
        {
            _loginservice = new loginmodel();
            LoginCommand = new RelayCommand(Login);
        }
        private void Login()
        {
            var user = _loginservice.GetTaikhoan(Username, Password);
            if(user != null)
            {
                var phanquyen = _loginservice.phanloaitk(user.MaLoaiTK);
                if(phanquyen.MaLoaiTK== 1)
                {
                    
                    TenUser = user.TenTK;
                    QuyenHan = phanquyen.TenMaLoaiTK;
                    TenNhanVien = user.TenTK;
                    

                    var mainwindow = new MainMenu(TenUser,QuyenHan,TenNhanVien);
                    mainwindow.Show();
                    CloseLoginWindowAction?.Invoke();




                }
            }



        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
