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

namespace UI.PO
{
    /// <summary>
    /// Interaction logic for modl.xaml
    /// </summary>
    public partial class modl : Window
    {
        public modl()
        {
            InitializeComponent();
        }
        static readonly DependencyProperty BusLinePO = DependencyProperty.Register("Line ", typeof(BusLine), typeof(modl));
        public BusLine Line { get => (BusLine)GetValue(BusLinePO); set => SetValue(BusLinePO, value); }
        public BO.BusLineBO BusLineBO
        {
            set
            {
                if (value == null)
                    Line = new BusLine();
                else
                {
                    value.DeepCopyTo(Line);
                }
            }
        }

    }
}
