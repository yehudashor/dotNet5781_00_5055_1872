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
namespace UI.lines
{
    /// <summary>
    /// Interaction logic for AddLineExit.xaml
    /// </summary>
    public partial class AddLineExit : Window
    {
        public IBL1 bl;
        public int NumberLine { get; set; }
        public AddLineExit(int NumberLine1, IBL1 bl1)
        {
            InitializeComponent();
            bl = bl1;
            NumberLine = NumberLine1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.LineExitBo lineExitBo = new BO.LineExitBo
                {
                    LineFrequencyTime = new TimeSpan(int.Parse(H.Text), int.Parse(M.Text), int.Parse(S.Text)),
                    LineFinishTime = new TimeSpan(int.Parse(HA.Text), int.Parse(MA.Text), int.Parse(SA.Text)),
                    LineStartTime = new TimeSpan(int.Parse(HB.Text), int.Parse(MB.Text), int.Parse(SB.Text)),
                    BusLineID1 = NumberLine
                };
                bl.AddExitToLine(lineExitBo);
                Close();
            }
            catch (BO.BOExceptionLineExit ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
    }
}
