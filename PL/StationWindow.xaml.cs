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
using BO;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL BLObject = BlFactory.GetBl();
        private int Id;
        private string Name;
        private int freeChargingSlots;
        private double Longitude;
        private double Lattitude;
        public StationWindow(StationForList station = null)
        {
            InitializeComponent();
            if(station == null) // add mode
            {


            }
            else
            {

            }
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdValueTextBox.Text, out Id) || Id < 0)
            {
                MessageBox.Show("Id should be an integer grater or equal to than 0!");
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
                    BLObject.AddStation(Id, Name,, new BO.Location { Longitude = this.Longitude, Latitude = this.Lattitude });
                    MessageBox.Show($"Customer {Id} was added succefully!");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
