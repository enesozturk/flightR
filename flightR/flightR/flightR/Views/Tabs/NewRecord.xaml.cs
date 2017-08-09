using flightR.Data;
using flightR.Helper;
using flightR.Models;
using flightR.Utils;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public List<FlightRecord> newList;

        public NewRecord()
        {
            InitializeComponent();
            btnRecord.Clicked += BtnRecord_Clicked;
        }

        private void BtnRecord_Clicked(object sender, EventArgs e)
        {
            buttonCounter++;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (buttonCounter % 2 == 0)
                {
                    return false;
                }
                else
                {
                    if (timerCounter == 5)
                    {
                        Task.Run(async () =>
                        {
                            var position = await GetPosition();
                            SQLiteManager manager = new SQLiteManager();
                            FlightRecord record = new FlightRecord();
                            record.Altitude = position.Altitude;
                            record.Latitude = position.Latitude;
                            record.Longitude = position.Longitude;

                            int isInserted = manager.Insert(record);
                            if (isInserted > 0) DisplayAlert("Added Record", "Latitude: " + record.Latitude, "Ok");
                        });
                    }
                    timerCounter++;
                    lblCounter.Text = timerCounter.ToString();
                    return true; // True = Repeat again, False = Stop the timer
                }
            });
        }
        
        public async Task<Position> GetPosition()
        {
            Position position = null;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            return position;
        }
    }
}