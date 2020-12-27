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

namespace UI
{
    /// <summary>
    /// Interaction logic for ManagerLogin.xaml
    /// </summary>
    public partial class ManagerLogin : Window
    {
        public ManagerLogin()
        {
            InitializeComponent();
        }

        private void LodIn(object sender, RoutedEventArgs e)
        {
            if (Password1.Password.Length > 6 && UserN.Text.Length > 4)
            {
                LineDisplay lineDisplay = new LineDisplay();
                lineDisplay.Show();
                UserN.Text = "";
                Password1.Password = "";
            }
            // lineDisplay.Close();
            //Password1.Password = "k";
            //Password1.TextInput == "uuu";
            else
            {
                _ = MessageBox.Show("One of the details you entered is incorrect!!! try again");
                UserN.Text = "";
                Password1.Password = "";
            }
        }
    }
}
