//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;

namespace UI.PO
{
    public class StationPO : DependencyObject
    {
        private static readonly DependencyProperty StationNumberProperty = DependencyProperty.Register("StationNumber", typeof(int), typeof(StationPO));
        public int StationNumber { get => (int)GetValue(StationNumberProperty); set => SetValue(StationNumberProperty, value); }

        private static readonly DependencyProperty NameOfStationProperty = DependencyProperty.Register("NameOfStation", typeof(string), typeof(StationPO));
        public string NameOfStation { get => (string)GetValue(NameOfStationProperty); set => SetValue(NameOfStationProperty, value); }


        private static readonly DependencyProperty StationAddressProperty = DependencyProperty.Register("StationAddress", typeof(string), typeof(StationPO));
        public string StationAddress { get => (string)GetValue(StationAddressProperty); set => SetValue(StationAddressProperty, value); }

        private static readonly DependencyProperty AccessForDisabledProperty = DependencyProperty.Register("AccessForDisabled", typeof(bool), typeof(StationPO));
        public bool AccessForDisabled { get => (bool)GetValue(AccessForDisabledProperty); set => SetValue(AccessForDisabledProperty, value); }

        private static readonly DependencyProperty RoofToTheStationProperty = DependencyProperty.Register("RoofToTheStation", typeof(bool), typeof(StationPO));
        public bool RoofToTheStation { get => (bool)GetValue(RoofToTheStationProperty); set => SetValue(RoofToTheStationProperty, value); }


        private static readonly DependencyProperty IsAvailable3Property = DependencyProperty.Register("IsAvailable3", typeof(bool), typeof(StationPO));
        public bool IsAvailable3 { get => (bool)GetValue(IsAvailable3Property); set => SetValue(IsAvailable3Property, value); }


        private static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(float), typeof(StationPO));
        public float Longitude { get => (float)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }

        private static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(float), typeof(StationPO));
        public float Latitude { get => (float)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
    }
}
