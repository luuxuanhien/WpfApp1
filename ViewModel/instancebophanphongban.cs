using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public class instancebophanphongban
    {
        private static bophanphongban _instance;

        public static bophanphongban Instance =>
            _instance ??= new bophanphongban();
    }
}
