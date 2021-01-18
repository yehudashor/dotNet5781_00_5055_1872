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
using BO;

namespace UI.Users
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public IBL1 bl;
        public AddUser(IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            permission1ComboBox.ItemsSource = Enum.GetValues(typeof(Permission));
            permission1ComboBox.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult box = MessageBox.Show("האם אתה בטוח שברצונך להוסיף את המשתמש?", "ask", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                UserBo userBo = new UserBo
                {
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                    Permission1 = (Permission)permission1ComboBox.SelectedItem,
                    ChackDelete = (bool)chackDeleteCheckBox.IsChecked
                };
                switch (box)
                {
                    case MessageBoxResult.OK:
                        bl.AddUserToDo(userBo);
                        User user = new User(bl);
                        user.Show();
                        MessageBoxResult messageBoxResult = MessageBox.Show("המשתמש נוסף למערכת", "Good");
                        Close();
                        break;
                    case MessageBoxResult.Cancel:
                        Close();
                        break;
                }
            }
            catch (BOExceptionUser ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearValue(TextBox.TextProperty);
            }
        }
    }
}
