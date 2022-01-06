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
        private DroneStatuses? statusFilter = null;
        private WheightCategories? weightFilter = null;
        public DronesListPage()
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            StatusSelector.DataContext = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.DataContext = Enum.GetValues(typeof(WheightCategories));
            DronesListView.DataContext = BLObject.GetDrones();
            GroupByStatusSelector.DataContext = Enum.GetValues(typeof(DroneStatuses));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statusFilter = (DroneStatuses?)StatusSelector.SelectedItem;
            IEnumerable<DroneForList> currenList = BLObject.GetDrones();
            DronesListView.DataContext = from drone in currenList
                                         where drone.Status == statusFilter || statusFilter == null
                                         where drone.MaxWeight == weightFilter || weightFilter == null
                                         select drone;
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            weightFilter = (WheightCategories?)WeightSelector.SelectedItem;
            IEnumerable<DroneForList> currenList = BLObject.GetDrones();
            DronesListView.DataContext = from drone in currenList
                                         where drone.Status == statusFilter || statusFilter == null
                                         where drone.MaxWeight == weightFilter || weightFilter == null
                                         select drone;
        }
        private void StatusSelector_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DronesListView.DataContext = BLObject.GetDrones();
        }
        private void WeightSelector_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DronesListView.DataContext = BLObject.GetDrones();

        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWindow = new DroneWindow();
            droneWindow.Closed += DroneWindow_Closed;
            droneWindow.Show();

        }
        private void DroneWindow_Closed(object sender, EventArgs e)
        {
            DronesListView.ItemsSource = BLObject.GetDrones();
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

        private void GroupByWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GroupByStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GroupByStatusSelector_Checked(object sender, RoutedEventArgs e)
        {
            var currenList = (IEnumerable<DroneForList>)DronesListView.DataContext;
            var resultList = from drone in currenList
                             orderby drone.Status
                             select drone;
            DronesListView.DataContext = resultList;
        }

        private void GroupByStatusSelector_Unchecked(object sender, RoutedEventArgs e)
        {
            DronesListView.DataContext = BLObject.GetDrones();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DronesListView.DataContext = BLObject.GetDrones();
            DronesListView.Items.Refresh();
            GroupByStatusSelector.IsChecked = false;
            StatusSelector.SelectedIndex = -1;
            WeightSelector.SelectedIndex = -1;
        }
    }
}

