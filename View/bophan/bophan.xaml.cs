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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModel;

namespace WpfApp1.View.bophan
{
    /// <summary>
    /// Interaction logic for bophan.xaml
    /// </summary>
    public partial class bophan : Page
    {
        public bophan()
        {
            InitializeComponent();
            var viewMode = instancebophanphongban.Instance;
            this.DataContext = viewMode;
        }
    }
}
