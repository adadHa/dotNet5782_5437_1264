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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private BlApi.IBL BLObject { get; set; }
        public Drone Drone { get; set; }

        //for adding a drone:
        private int Id;
        private string Model;
        private string Weight;
        private int InitialStation;

        //constructor of add drone mode
        public DroneWindow()
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            ComboBoxInsertWeight.ItemsSource = Enum.GetNames(typeof(WheightCategories));

            //make the add drone window visible
            AddDroneWindow.Visibility = Visibility.Visible;
            OptionsDroneWindow.Visibility = Visibility.Collapsed;
        }

        // constructor of view drone/options mode
        public DroneWindow( DroneForList drone)
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            Drone = BLObject.GetDrone(drone.Id);
            OptionsDroneWindow.DataContext = Drone;
            OptionsDroneWindow.Visibility = Visibility.Visible;
            AddDroneWindow.Visibility = Visibility.Collapsed;
            TimeInput.Visibility = Visibility.Collapsed;
            ChargeDroneButton.Visibility = Visibility.Collapsed;
            ReleaseDroneButton.Visibility = Visibility.Collapsed;
            LinkDroneButton.Visibility = Visibility.Collapsed;
            PickUpDroneButton.Visibility = Visibility.Collapsed;
            SupplyParcelDroneButton.Visibility = Visibility.Collapsed;
            //show only desired buttons
            if (drone.Status == DroneStatuses.Available)
            {
                ChargeDroneButton.Visibility = Visibility.Visible;
                LinkDroneButton.Visibility = Visibility.Visible;
            }
            else if(drone.Status == DroneStatuses.Maintenance)
            {
                ReleaseDroneButton.Visibility = Visibility.Visible;
            }
            else if (drone.Status == DroneStatuses.Shipping && BLObject.GetParcel(drone.DeliveredParcelNumber).PickedUp == null)
            {
                PickUpDroneButton.Visibility = Visibility.Visible;
            }
            else if (drone.Status == DroneStatuses.Shipping && BLObject.GetParcel(drone.DeliveredParcelNumber).PickedUp != null)
            {
                SupplyParcelDroneButton.Visibility = Visibility.Visible;
            }
            TitleTextBox.Text = $"Drone {drone.Id}";
    

        }

        private void TextBoxInsertId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(TextBoxInsertId.Text, out Id) && Id > 0) || TextBoxInsertId.Text == "")
            {
                TextBoxInsertId.Background = Brushes.White;
            }
            else
            {
                TextBoxInsertId.Background = Brushes.Red;
            }
        }

        private void TextBoxInsertModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Model = TextBoxInsertModel.Text;
        }

        private void TextBoxInsertInitialStationId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((int.TryParse(TextBoxInsertInitialStationId.Text, out Id) && Id >= 0) || TextBoxInsertInitialStationId.Text == "")
            {
                TextBoxInsertInitialStationId.Background = Brushes.White;
            }
            else
            {
                TextBoxInsertInitialStationId.Background = Brushes.Red;
            }
        }
        private void ComboBoxInsertWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight = (string)ComboBoxInsertWeight.SelectedItem;
        }

        private void ButtonAddDrone_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TextBoxInsertId.Text, out Id) || Id < 0)
            {
                MessageBox.Show("Id should be an integer grater or equal to than 0!");
            }
            else if (!int.TryParse(TextBoxInsertInitialStationId.Text, out InitialStation) || Id < 0)
            {
                MessageBox.Show("Initial id should be an integer grater or equal to 0!");
            }
            else
            {
                try
                {
                    BLObject.AddDrone(Id, Model, Weight, InitialStation);
                    MessageBox.Show($"Drone {Id} was added succefully!");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                } 
            }

        }

        private void TextBoxInsertId_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxInsertId.Text = "";
        }

        private void TextBoxInsertModel_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxInsertModel.Text = "";
        }

        private void TextBoxInsertInitialStationId_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxInsertInitialStationId.Text = "";
        }

        private void CloseDroneWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void ModelValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BLObject.UpdateDrone(Drone.Id, ModelValueTextBox.Text);
            }

        }

        private void CloseOptionsDroneWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChargeDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.ChargeDrone(Drone.Id);
                Drone = BLObject.GetDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} has sarted charging. \n " +
                    $"Its battery now is {Drone.Battery}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LinkDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.BindDrone(Drone.Id);
                Drone = BLObject.GetDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} was sent succefully to deliver parcel {Drone.TransferedParcel.Id}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ReleaseDroneButton_Click(object sender, RoutedEventArgs e)
        {
            TimeInput.Visibility = Visibility.Visible;
            TimeInput.Focus();
        }

        private void PickUpDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.CollectParcelByDrone(Drone.Id);
                Drone = BLObject.GetDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} picks up parcel {Drone.TransferedParcel.Id}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SupplyParcelDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.SupplyParcel(Drone.Id);
                Drone = BLObject.GetDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} supply parcel {Drone.TransferedParcel.Id}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                double time = 0;
                if (double.TryParse(TimeInputTextBox.Text, out time))
                {

                    if (time < 0)
                    {
                        MessageBox.Show("Time should be positive!");
                    }
                    else
                    {
                        try
                        {
                            BLObject.ReleaseDroneFromCharging(Drone.Id, time);
                            MessageBox.Show($"Drone {Drone.Id} has stopped charging. \n " +
                            $"Its battery now is {Drone.Battery}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        TimeInput.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    MessageBox.Show("Charging time should be positive!");
                }
            }
        }

        private void TimeInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double time;
            if ((double.TryParse(TimeInputTextBox.Text, out time) && time >= 0) || TimeInputTextBox.Text == "")
            {
                TimeInputTextBox.Background = Brushes.White;
            }
            else
            {
                TimeInputTextBox.Background = Brushes.Red;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(Drone.TransferedParcel.Id).Show();
        }
    }
}
