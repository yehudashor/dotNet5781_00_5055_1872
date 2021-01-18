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
    /// Interaction logic for UdaptingUser.xaml
    /// </summary>
    public partial class UdaptingUser : Window
    {
        public IBL1 bl;
        public UserBo UserBo;
        public UdaptingUser(IBL1 bl1, UserBo UserBo1)
        {
            InitializeComponent();
            bl = bl1;
            UserBo = UserBo1;
            usernameTextBox.Text = UserBo.Username;
            permission1ComboBox.ItemsSource = Enum.GetValues(typeof(Permission));
            permission1ComboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult box = MessageBox.Show("האם אתה בטוח שברצונך לעדכן את פרטי המשתמש?", "ask", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                UserBo.Password = passwordTextBox.Text.ToString();
                UserBo.Permission1 = (Permission)permission1ComboBox.SelectedItem;
                UserBo.ChackDelete = (bool)chackDeleteCheckBox.IsChecked;
                switch (box)
                {
                    case MessageBoxResult.OK:
                        bl.UdptingUser(UserBo);
                        User user = new User(bl);
                        user.Show();
                        MessageBoxResult messageBoxResult = MessageBox.Show("העדכון בוצע בהצלחה", "Good");
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
