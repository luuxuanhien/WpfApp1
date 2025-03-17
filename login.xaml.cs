using System.Text;
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

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class login : Window
{
    public login()
    {
        InitializeComponent();
       
        var viewModel = new loginviewmodel();

        // Gán phương thức Close của cửa sổ vào Action
        viewModel.CloseLoginWindowAction = Close;
        DataContext = viewModel;

    }

   
}