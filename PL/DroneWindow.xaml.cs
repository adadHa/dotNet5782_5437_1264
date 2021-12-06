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
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL.IBL BLObject { get; set; }
        public DroneForList Drone { get; }

        //for adding a drone:
        private int Id;
        private string Model;
        private string Weight;
        private int InitialStation;

        //constructor of add drone mode
        public DroneWindow(IBL.IBL blObject)
        {
            InitializeComponent();
            BLObject = blObject;
            ComboBoxInsertWeight.ItemsSource = Enum.GetNames(typeof(WheightCategories));

            //make the add drone window visible
            AddDroneWindow.Visibility = Visibility.Visible;
            OptionsDroneWindow.Visibility = Visibility.Collapsed;
        }

        // constructor of view drone/options mode
        public DroneWindow(IBL.IBL blObject, DroneForList drone) : this(blObject)
        {
            OptionsDroneWindow.Visibility = Visibility.Visible;
            AddDroneWindow.Visibility = Visibility.Collapsed;
            Drone = drone;
            TitleTextBox.Text = $"Drone {drone.Id}";
            IdAndParcelIdTextBlock.Text = $"With parcel {drone.DeliveredParcelNumber}";
            LocationValueTextBlock.Text = "Location: " + drone.Location.ToString();
            WeightCategoryValueTextBlock.Text = "Weight: " + Enum.GetName(drone.MaxWeight);
            StatusValueTextBlock.Text = "Status: " + Enum.GetName(drone.Status);
            BatteryValueTextBlock.Text = $"Battery: {Math.Round(drone.Battery, 2)}";
        }

        private void TextBoxInsertId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(TextBoxInsertId.Text, out Id) || Id < 0)
            {
                MessageBox.Show("Id should be an integer grater than 0!");
            }
        }

        private void TextBoxInsertModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Model = TextBoxInsertModel.Text;
        }

        private void TextBoxInsertInitialStationId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(TextBoxInsertInitialStationId.Text, out InitialStation))
            {
                MessageBox.Show("Initial id should be an integer grater than 0!");
            }
        }
        private void ComboBoxInsertWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight = (string)ComboBoxInsertWeight.SelectedItem;
        }

        private void ButtonAddDrone_Click(object sender, RoutedEventArgs e)
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

        private void ButtonAddDrone_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAddDrone.Background = FindResource("AddButtonMouseOver") as Brush;
        }

        private void ButtonAddDrone_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAddDrone.Background = FindResource("AddButton") as Brush;
        }

        private void LocationValueTextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {
            int a = 0;
        }

        private void ModelValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
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
                MessageBox.Show($"Drone {Drone.Id} has sarted charging. \n " +
                    $"Its battery now is {Drone.Battery}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReleaseDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BLObject.ReleaseDroneFromCharging(Drone.Id,);
                MessageBox.Show($"Drone {Drone.Id} has stopped charging. \n " +
                    $"Its battery now is {Drone.Battery}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SendDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.BindDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} was sent succefully to deliver parcel {Drone.DeliveredParcelNumber}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PickUpDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLObject.CollectParcelByDrone(Drone.Id);
                MessageBox.Show($"Drone {Drone.Id} was picks up parcel {Drone.DeliveredParcelNumber}!");
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
                MessageBox.Show($"Drone {Drone.Id} supply parcel {Drone.DeliveredParcelNumber}!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
