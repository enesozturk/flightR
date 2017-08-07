using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecord : ContentPage
    {
        public NewRecord()
        {
            InitializeComponent();
        }

        public async void getPositionButton_Click(object sender, EventArgs e)
        {
            await GetPosition();
        }

        public async Task GetPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            lblLat.Text = position.Latitude.ToString();
            lblLong.Text = position.Longitude.ToString();
        }
    }
}