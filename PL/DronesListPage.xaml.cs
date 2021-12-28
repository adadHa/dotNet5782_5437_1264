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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for DronesListPage.xaml
    /// </summary>
    public partial class DronesListPage : Page
    {
        private BlApi.IBL BLObject { get; set; }


        public DronesListPage()
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WheightCategories));
            DronesListView.DataContext = BLObject.GetDrones();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses statusFilter = (DroneStatuses)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = BLObject.GetDrones(x => x.Status == statusFilter);
            DronesListView.Items.Refresh();
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WheightCategories weightFilter = (WheightCategories)WeightSelector.SelectedItem;
            DronesListView.ItemsSource = BLObject.GetDrones(x => x.MaxWeight == weightFilter);
            DronesListView.Items.Refresh();
        }
        private void StatusSelector_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DronesListView.ItemsSource = BLObject.GetDrones();
            DronesListView.Items.Refresh();
        }
        private void WeightSelector_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DronesListView.ItemsSource = BLObject.GetDrones();
            DronesListView.Items.Refresh();
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWindow = new DroneWindow();
            droneWindow.Closed += DroneWindow_Closed;
            droneWindow.Show();

        }
        private void DroneWindow_Closed(object sender, EventArgs e)
        {
            DronesListView.Items.Refresh();
        }

        private void DronesListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DronesListView.ItemsSource = BLObject.GetDrones();
            DronesListView.Items.Refresh();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneForList drone = (DroneForList)((ListView)sender).SelectedItem;
            if (drone != null)
            {
                new DroneWindow(drone).Show(); 
            }
        }

    }
}

