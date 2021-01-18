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
using System.Windows.Shapes;
using BLAPI;


namespace UI.Users
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public ObservableCollection<BO.UserBo> busLineBOs = new ObservableCollection<BO.UserBo>();
        public IBL1 bl;
        public User(IBL1 bl1)
        {
            InitializeComponent();
            try
            {
                bl = bl1;
                IEnumerable<BO.UserBo> userBos = bl.GetListUsers();
                foreach (BO.UserBo item in userBos)
                {
                    busLineBOs.Add(item);
                }
                userBoListView.ItemsSource = busLineBOs;
            }
            catch (BO.BOExceptionUser ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser(bl);
            addUser.Show();
            Close();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {

            FrameworkElement frameworkElement = sender as FrameworkElement;
            BO.UserBo userBo = frameworkElement.DataContext as BO.UserBo;
            UdaptingUser udaptingUser = new UdaptingUser(bl, userBo);
            udaptingUser.Show();
            Close();

        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = sender as FrameworkElement;
                BO.UserBo userBo = frameworkElement.DataContext as BO.UserBo;
                bl.DeleteUserFromDo(userBo.Username);
                _ = busLineBOs.Remove(userBo);
                busLineBOs.Add(bl.GetUser(userBo.Username));
            }
            catch (BO.BOExceptionUser ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
