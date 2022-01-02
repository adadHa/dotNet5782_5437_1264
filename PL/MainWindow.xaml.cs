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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DronesListPage droneListPage { get; set; }
        CustomerListPage customersListPage { get; set; }
        StationsListPage stationsListPage { get; set; }
        private BlApi.IBL BLObject { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();

        }

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            droneListPage = new DronesListPage();
            CurrentPage.Content = droneListPage;
        }

        private void CloseOptionsDroneWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            customersListPage = new CustomerListPage(BLObject);
            CurrentPage.Content = customersListPage;
        }

        private void CurrentPage_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            stationsListPage = new StationsListPage();
            CurrentPage.Content = stationsListPage;
        }
    }
}
