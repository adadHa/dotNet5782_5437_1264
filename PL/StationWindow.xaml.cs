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
        private int freeChargeSlots;
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
                StationWindowGrid.DataContext = station;
                IsInOptionsModeCheckBox.IsChecked = true; // all the relevant elements are bind to this checkbox and will be visible/not visible as required
            }
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdValueTextBox.Text, out Id) || Id < 0)
            {
                MessageBox.Show("Id should be an integer grater or equal to than 0!");
            }

            if (!(double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude >= 0 && double.TryParse(LongitudeValueTextBox.Text, out Longitude) && Longitude <= 180))
            {
                MessageBox.Show("Longitude should be an double grater or equal to than 0 and need to be less or equal to 180!");
            }

            if (!(double.TryParse(LattitudeValueTextBox.Text, out Lattitude) && Lattitude >= 0 && double.TryParse(LattitudeValueTextBox.Text, out Longitude) && Lattitude <= 180))
            {
                MessageBox.Show("Lattitude should be an double grater or equal to than 0 and need to be less or equal to 180!");
            }

            else
            {
                try
                {
                    BLObject.AddStation(Id, Name, freeChargeSlots, new BO.Location { Longitude = this.Longitude, Latitude = this.Lattitude });
                    MessageBox.Show($"Station {Id} was added succefully!");
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

        private void FreeChargeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(FreeChargeSlots.Text, out freeChargeSlots) && freeChargeSlots >= 0) || FreeChargeSlots.Text == "")
            {
                FreeChargeSlots.Background = Brushes.White;
            }
            else
            {
                FreeChargeSlots.Background = Brushes.Red;
            }
        }
    }
}
