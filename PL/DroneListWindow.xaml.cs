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
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.IBL BLObject { get; set; }

        public DroneListWindow()
        {
            InitializeComponent();
        }

        public DroneListWindow(IBL.IBL blObject)
        {
            InitializeComponent();
            BLObject = blObject;
            DronesListView.ItemsSource = BLObject.GetDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses statusFilter = (DroneStatuses)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = BLObject.GetDrones(x => x.Status == statusFilter);
        }
    }
}