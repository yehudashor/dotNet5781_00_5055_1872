using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.PO
{
    public class StationLinePO : DependencyObject
    {

        public int BusLineID2 { get; set; }
        public string NameOfStation { get; set; }
        public int StationNumberOnLine { get; set; }
        public int LocationNumberOnLine { get; set; }
        public bool ChackDelete2 { get; set; }

        private static readonly DependencyProperty AverageTimeProperty = DependencyProperty.Register("AverageTime", typeof(TimeSpan), typeof(StationLinePO));
        public TimeSpan AverageTime { get => (TimeSpan)GetValue(AverageTimeProperty); set => SetValue(AverageTimeProperty, value); }

        private static readonly DependencyProperty DistanceBetweenTooStationsProperty = DependencyProperty.Register(" DistanceBetweenTooStations", typeof(float), typeof(StationLinePO));
        public float DistanceBetweenTooStations { get => (float)GetValue(DistanceBetweenTooStationsProperty); set => SetValue(DistanceBetweenTooStationsProperty, value); }


    }
}
