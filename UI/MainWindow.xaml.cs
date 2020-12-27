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
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         IBL1 bl = BLFactory.GetBL("1");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Administrator(object sender, RoutedEventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin();
            _ = managerLogin.ShowDialog();
        }

        private void Customer(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("In construction");
        }
    }
}
