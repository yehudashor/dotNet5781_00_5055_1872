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
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for ManagerLogin.xaml
    /// </summary>
    public partial class ManagerLogin : Window
    {
        IBL1 bl1;
        public ManagerLogin(IBL1 bl)
        {
            bl1 = bl;
            InitializeComponent();
        }

        private void LodIn(object sender, RoutedEventArgs e)
        {
            if (bl1.FindUser(Password1.Password, UserN.Text))
            {
                LineDisplay lineDisplay = new LineDisplay(bl1);
                lineDisplay.Show();
            }

            else
            {
                _ = MessageBox.Show("One of the details you entered is incorrect!!! try again");
                UserN.ClearValue(TextBox.TextProperty);
                //Password1.ClearValue(TextBox.TextProperty);
            }
        }
    }
}
