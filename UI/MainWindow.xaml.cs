using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        IBL1 bl1 = BLFactory.GetBL("1");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LodIn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl1.FindUser(Password1.Password, UserN.Text))
                {
                    LineDisplay lineDisplay = new LineDisplay(bl1);
                    lineDisplay.Show();
                    Close();
                }
            }
            catch (BO.BOExceptionUser ex)
            {
                _ = MessageBox.Show("One of the details you entered is incorrect!!! try again" + ex);
                UserN.ClearValue(TextBox.TextProperty);
            }
        }
    }
}
