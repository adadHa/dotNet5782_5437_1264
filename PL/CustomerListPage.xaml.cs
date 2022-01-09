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
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        BlApi.IBL BLObject;
        public CustomerListPage(BlApi.IBL blObject)
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            CustomersListView.DataContext = BLObject.GetCustomers();
        }

        private void titleTextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            titleTextBlock.Foreground = Brushes.White;
        }
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Closed += CustomerWindow_Closed;
            customerWindow.Show();
        }
        private void CustomerWindow_Closed(object sender, EventArgs e)
        {
            CustomersListView.DataContext = BLObject.GetCustomers();
        }
        private void CustomersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerForList customer = (CustomerForList)((ListView)sender).SelectedItem;
            if (customer != null)
            {
                new CustomerWindow(customer.Id).Show();
            }
        }
        
    }
}
