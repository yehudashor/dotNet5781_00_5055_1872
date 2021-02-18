using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace UI.PO
{
    public enum Area1 { ירושלים, גליל, השרון, שפלה, אילת, צפון, דרום, מרכז };
    public enum Urban { Urban, NotUrban };
    public enum Available { Available, Notavailable };
    public class BusLine : DependencyObject
    {
        public ObservableCollection<LineExitPO> LineExitBos1 = new ObservableCollection<LineExitPO>();

        public ObservableCollection<StationLinePO> StationLineBOs = new ObservableCollection<StationLinePO>();

        private static readonly DependencyProperty IDProperty = DependencyProperty.Register("BusLineID1", typeof(int), typeof(BusLine));
        public int BusLineID1 { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }

        private static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(BusLine));
        public int LineNumber { get => (int)GetValue(LineNumberProperty); set => SetValue(LineNumberProperty, value); }

        private static readonly DependencyProperty FirstStationProperty = DependencyProperty.Register("FirstStation", typeof(int), typeof(BusLine));
        public int FirstStation { get => (int)GetValue(FirstStationProperty); set => SetValue(FirstStationProperty, value); }


        private static readonly DependencyProperty LastStationProperty = DependencyProperty.Register("LastStation ", typeof(int), typeof(BusLine));
        public int LastStation { get => (int)GetValue(LastStationProperty); set => SetValue(LastStationProperty, value); }

        private static readonly DependencyProperty AreaBusUrbanProperty = DependencyProperty.Register("AreaBusUrban", typeof(Area1), typeof(BusLine));
        public Area1 AreaBusUrban { get => (Area1)GetValue(AreaBusUrbanProperty); set => SetValue(AreaBusUrbanProperty, value); }

        private static readonly DependencyProperty GetUrbanProperty = DependencyProperty.Register("GetUrban", typeof(Urban), typeof(BusLine));
        public Urban GetUrban { get => (Urban)GetValue(GetUrbanProperty); set => SetValue(GetUrbanProperty, value); }

        private static readonly DependencyProperty GetAvailableProperty = DependencyProperty.Register("GetAvailable ", typeof(Available), typeof(BusLine));
        public Available GetAvailable { get => (Available)GetValue(GetAvailableProperty); set => SetValue(GetAvailableProperty, value); }

        private static readonly DependencyProperty BeginningTimeProperty = DependencyProperty.Register("BeginningTime", typeof(TimeSpan), typeof(BusLine));
        public TimeSpan BeginningTime { get => (TimeSpan)GetValue(BeginningTimeProperty); set => SetValue(BeginningTimeProperty, value); }

        private static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register("EndTime ", typeof(TimeSpan), typeof(BusLine));
        public TimeSpan EndTime { get => (TimeSpan)GetValue(EndTimeProperty); set => SetValue(EndTimeProperty, value); }
    }
}
