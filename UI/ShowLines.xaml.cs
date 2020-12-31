﻿using System;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for ShowLine.xaml
    /// </summary>
    public partial class ShowLine : Window
    {
        IBL1 bl4;
        public BO.BusLineBO BusLine { get; set; }
        public ShowLine(BO.BusLineBO busLineBO)
        {
            InitializeComponent();
            BusLine = busLineBO;
            busStationBOListView.ItemsSource = busLineBO.BusStationBO1;
            shoeLine.DataContext = busLineBO;
        }
    }
}
