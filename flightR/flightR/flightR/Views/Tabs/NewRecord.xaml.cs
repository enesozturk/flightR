using AdvancedTimer.Forms.Plugin.Abstractions;
using flightR.Models;
using flightR.Provider;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecord : ContentPage
    {
        public int timerCounter { get; set; } = 0; //for text

        private ServiceManager manager = new ServiceManager();

        public Plugin.Geolocator.Abstractions.Position position { get; set; }
        public Record record { get; set; }
        public Models.Point point { get; set; }
        public MobileResult mobileresult { get; set; }

        static IAdvancedTimer timer;

        public NewRecord()
        {
            timer = DependencyService.Get<IAdvancedTimer>();
            InitializeComponent();
        }

        public void startTimer(object sender, EventArgs e)
        {
            timer.initTimer(1000, timerElapsed, true);
            timer.startTimer();
        }

        public void stopTimer(object sender, EventArgs e)
        {
            DisplayAlert("Success", "Record saved your list", "Ok", "Cancel");
            timer.stopTimer();
            timerCounter = 0;
            position = null;
            record = null;
            mobileresult = null;
            point = null;
        }

        public async void timerElapsed(object sender, EventArgs e)
        {
            timerCounter++;
            // her saniye bu fonksiyon çalışacak
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                if(timerCounter == 1)
                {
                    record = new Record //record oluştur
                    {
                        CreatedDate = DateTime.Now,
                        UserId = 1
                    };
                    await manager.NewRecord(record);
                }
                if (timerCounter % 5 == 0)
                {
                    await CreateNewPoint();
                }
            });
        }

        void CreateMap()
        {
            Map map = new Map()
            {
                HasScrollEnabled = true,
                HasZoomEnabled = true,
                MapType = MapType.Hybrid
            };
        }

        private async void CreateNewRecord()
        {
            record = new Record //record oluştur
            {
                CreatedDate = DateTime.Now,
                UserId = 1
            };
            await manager.NewRecord(record); //record'u db ye kaydet
        }

        private async Task CreateNewPoint()
        {
            //her saniye point oluşturacak
            position = await GetCurrentLocation();
            var record2 = await manager.GetLastRecord(1);
            point = new Models.Point
            {
                Id = 0,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Altitude = 15.154,
                RecordId = record2.Id
            };

            lbllatitude.Text = position.Latitude.ToString();
            lbllongitude.Text = position.Longitude.ToString();
            lblaltitude.Text = position.Altitude.ToString();
            lblspeed.Text = position.Speed.ToString();

            //pointi db ye kaydet
            await manager.Insert(point);
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            var position = await locator.GetPositionAsync(10000);
            
            return position;
        }
    }
}