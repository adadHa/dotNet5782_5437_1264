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
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WheightCategories Wheight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelWindow(int? parcelId = null)
        {
            InitializeComponent();
            if(parcelId == null) //add mode
            {
                AddWheightValueComboBox.DataContext = Enum.GetValues(typeof(WheightCategories));
                AddPriorityValueComboBox.DataContext = Enum.GetValues(typeof(Priorities));
            }
            else // options mode
            {
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
            SenderId = (int)((ComboBox)sender).SelectedItem;
        }

        private void TargetIdValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TargetId = (int)((ComboBox)sender).SelectedItem;
        }

        private void AddWheightValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wheight = (WheightCategories)((ComboBox)sender).SelectedItem;
        }

        private void AddPriorityValueComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Priority = (Priorities)((ComboBox)sender).SelectedItem;
        }
    }
}
