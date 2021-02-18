using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using UI.PO;

namespace UI.lines
{
    /// <summary>
    /// Interaction logic for AddLineExit.xaml
    /// </summary>
    public partial class AddLineExit : Window
    {
        public IBL1 bl;
        public int NumberLine { get; set; }
        public ShowLine showLine;
        public AddLineExit(int NumberLine1, IBL1 bl1, ShowLine showLine1)
        {
            InitializeComponent();
            showLine = showLine1;
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
                lineExitBo = bl.GetLineExit(lineExitBo.BusLineID1, lineExitBo.LineStartTime);
                LineExitPO lineExitPO = new LineExitPO();
                lineExitBo.DeepCopyTo(lineExitPO);
                foreach (TimeSpan item in lineExitBo.TimeFinishTrval)
                {
                    lineExitPO.TimeFinishTrval.Add(item);
                }
                foreach (TimeSpan item in lineExitBo.DepartureTimes)
                {
                    lineExitPO.DepartureTimes.Add(item);
                }
                showLine.lineExitBos.Add(lineExitPO);
                _ = showLine.lineExitBos.OrderBy(item => item.LineStartTime);
                Close();
            }
            catch (BO.BOExceptionLineExit ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel,
                     MessageBoxImage.Error);
            }
        }
        //PreviewTextInput= "NumberValidationTextBox" 
        /// <summary>
        /// A function that ensures that only numbers can be entered in the textBox field !!!
        /// (Part of the above bonus).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
