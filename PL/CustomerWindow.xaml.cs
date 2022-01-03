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
using System.Windows.Shapes;
using BlApi;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private IBL BLObject { get; set; }
        private int Id;
        private string Phone;
        private string Name;
        private double Longitude;
        private double Lattitude;


        // options/add mode
        public CustomerWindow(CustomerForList customer = null)
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            if (customer == null) //add modw
            {

            }
            else //options mode
            {
                OptionsCustomerWindow.DataContext = BLObject.GetCustomer(customer.Id);
            }
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            int phone;
            if (!int.TryParse(IdValueTextBox.Text, out Id) || Id < 0)
            {
                MessageBox.Show("Id should be an integer grater or equal to than 0!");
            }

            if ((int.TryParse(PhoneValueTextBox.Text, out phone) && phone >= 0) || PhoneValueTextBox.Text == "" || PhoneValueTextBox.Text.Length == 10)
            {
                MessageBox.Show("Phone should be an integer grater or equal to than 0 and should be only 10 digits!");
            }

            if ((double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude >= 0) && (double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude <= 180) || LongitudeValueTextBox.Text == "")
            {
                MessageBox.Show("Phone should be an double grater or equal to than 0 and need to be less or equal to 180!");
            }

            if ((double.TryParse(LattitudeValueTextBox.Text, out Lattitude) && Lattitude >= 0) && (double.TryParse(LattitudeValueTextBox.Text, out Longitude) && Lattitude <= 180) || LattitudeValueTextBox.Text == "")
            {
                MessageBox.Show("Phone should be an double grater or equal to than 0 and need to be less or equal to 180!");
            }

            else
            {
                try
                {
                    BLObject.AddCustomer(Id, Name, Phone, new BO.Location { Longitude = this.Longitude, Latitude = this.Lattitude });
                    MessageBox.Show($"Customer {Id} was added succefully!");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void IdValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(IdValueTextBox.Text, out Id) && Id >= 0) || IdValueTextBox.Text == "")
            {
                IdValueTextBox.Background = Brushes.White;
            }
            else
            {
                IdValueTextBox.Background = Brushes.Red;
            }
        }

        private void NameValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Name = NameValueTextBox.Text;
        }

        private void PTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int phone;
            if ((int.TryParse(PhoneValueTextBox.Text, out phone) && phone >= 0) || PhoneValueTextBox.Text == "" || PhoneValueTextBox.Text.Length == 10)
            {
                Phone = PhoneValueTextBox.Text;
                PhoneValueTextBox.Background = Brushes.Green;
            }
            else
            {
                PhoneValueTextBox.Background = Brushes.Red;
            }
        }

        private void LongitudeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude >= 0) && (double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude <= 180) || LongitudeValueTextBox.Text == "")
            {
                LongitudeValueTextBox.Background = Brushes.White;
            }
            else
            {
                LongitudeValueTextBox.Background = Brushes.Red;
            }
        }

        private void LattitudeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((double.TryParse(LattitudeValueTextBox.Text, out Lattitude) && Lattitude >= 0) && (double.TryParse(LattitudeValueTextBox.Text, out Longitude) && Lattitude <= 180) || LattitudeValueTextBox.Text == "")
            {
                LattitudeValueTextBox.Background = Brushes.White;
            }
            else
            {
                LattitudeValueTextBox.Background = Brushes.Red;
            }
        }

        private void ParcelsFromCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ParcelsToCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
