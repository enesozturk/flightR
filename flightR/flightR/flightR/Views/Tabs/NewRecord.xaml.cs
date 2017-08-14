using flightR.Models;
using flightR.Provider;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecord : ContentPage
    {
        public int timerCounter { get; set; } = 0; //for text
        public int buttonCounter { get; set; } = 0; // to stop
        public Position position { get; set; }

        public List<Record> newList;

        public NewRecord()
        {
            InitializeComponent();
        }

        private async void btnRecord(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new Insert());
            await GetCurrentLocation();
            await Write1();
            await Write2();

            Record newRecord = new Record
            {
                Latitude = 2.222,
                Longitude = 3.333,
                Altitude = 1.111
            };

            ServiceManager manager = new ServiceManager();
            MobileResult mobileResult = await Task.Run(() => manager.Insert(newRecord));

            if (mobileResult.Result)
            {
                await DisplayAlert("Success", mobileResult.Message, "Ok", "Cancel");
                Navigation.PopModalAsync();
            }
            else
            {
                DisplayAlert("Error", mobileResult.Message, "Ok", "Cancel");
            }
        }

        private async Task Write1()
        {
            lblLat.Text = "Write1";
        }

        private async Task Write2()
        {
            lblLong.Text = "Write2";
        }

        private async Task GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            lblLat.Text = position.Latitude.ToString();
            lblLong.Text = position.Longitude.ToString();
            lblAlt.Text = position.Altitude.ToString();

            position = position;
        }
    }
}