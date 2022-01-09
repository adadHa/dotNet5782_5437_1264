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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBL BLObject = BlFactory.GetBl();
        Parcel Parcel;
        public int? SenderId = null;
        public int? TargetId = null;
        public WheightCategories? Wheight = null;
        public Priorities? Priority = null;
        public ParcelWindow(int? parcelId = null)
        {
            InitializeComponent();
            if(parcelId == null) //add mode
            {
                AddParcelGrid.Visibility = Visibility.Visible;
                ParcelGrid.Visibility = Visibility.Collapsed;
                TargetIdValueComboBox.DataContext = BLObject.GetCustomers();
                SenderIdValueComboBox.DataContext = BLObject.GetCustomers();
                AddWheightValueComboBox.DataContext = Enum.GetValues(typeof(WheightCategories));
                AddPriorityValueComboBox.DataContext = Enum.GetValues(typeof(Priorities));
            }
            else // options mode
            {
                ParcelGrid.Visibility = Visibility.Visible;
                AddParcelGrid.Visibility = Visibility.Collapsed;
                Parcel = BLObject.GetParcel((int)parcelId);
                ParcelGrid.DataContext = Parcel;
                WheightValueComboBox.ItemsSource = Enum.GetValues(typeof(WheightCategories));
                PriorityValueComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            }
        }

        private void WheightValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PriorityValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SenderIdValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerForList selectedCustomer = (CustomerForList)((ComboBox)sender).SelectedItem;
            SenderId = selectedCustomer.Id;
        }

        private void TargetIdValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerForList selectedCustomer = (CustomerForList)((ComboBox)sender).SelectedItem;
            TargetId = selectedCustomer.Id;
        }

        private void AddWheightValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wheight = (WheightCategories)((ComboBox)sender).SelectedItem;
        }

        private void AddPriorityValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Priority = (Priorities)((ComboBox)sender).SelectedItem;
        }

        private void ButtonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            if (Wheight != null && Priority != null && SenderId != null && TargetId != null )
            {
                BLObject.AddParcel((int)SenderId, (int)TargetId, Enum.GetName((WheightCategories)Wheight), Enum.GetName((Priorities)Priority));
                Close();
            }  
            else
            {
                MessageBox.Show("Please fill all fields with a value");
            }
            
        }

        private void SenderButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(Parcel.Sender.Id).Show();
        }

        private void TargetButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(Parcel.Target.Id).Show();
        }

        private void DroneButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(Parcel.Drone.Id).Show();
        }
    }
}
