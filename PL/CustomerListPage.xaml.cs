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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        BlApi.IBL BlObject;
        public CustomerListPage(BlApi.IBL blObject)
        {
            InitializeComponent();
            BlObject = blObject;
            CustomersListView.DataContext = blObject.GetCustomers();
        }

        private void titleTextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            titleTextBlock.Foreground = Brushes.White;
        }
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow().Show();
        }
    }
}
