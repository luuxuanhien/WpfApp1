﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public class instanceFormAndNhanVien
    {
        private static ChucNangThongTinNhanVienViewModel _instance;

        public static ChucNangThongTinNhanVienViewModel Instance =>
            _instance ??= new ChucNangThongTinNhanVienViewModel();
    }
}
