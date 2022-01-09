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
    /// Interaction logic for ParcelsListPage.xaml
    /// </summary>
    public partial class ParcelsListPage : Page
    {
        private BlApi.IBL BLObject { get; set; }
        public ParcelsListPage( ParcelForList parcel = null)
        {
            InitializeComponent();
            BLObject = BlFactory.GetBl();
            ParcelsListView.DataContext = BLObject.GetParcels();
        }
        private void ParcelsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelForList parcel = (ParcelForList)((ListView)sender).SelectedItem;
            if (parcel != null)
            {
                new ParcelWindow(parcel.Id).Show();
            }
        }

        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow parcelWindow = new ParcelWindow();
            parcelWindow.Closed += ParcelWindow_Closed;
            parcelWindow.Show();

        }
        private void ParcelWindow_Closed(object sender, EventArgs e)
        {
            ParcelsListView.ItemsSource = BLObject.GetParcels();
            ParcelsListView.Items.Refresh();
        }
    }
}
