using AdvancedTimer.Forms.Plugin.Abstractions;
using Android.Widget;
using flightR.Models;
using flightR.Provider;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
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
        public List<Models.Point> pointList = new List<Models.Point>();
        public MobileResult mobileresult { get; set; }

        public DateTime dtLast { get; set; }
        public DateTime dtStart { get; set; }
        public TimeSpan span { get; set; }

        static IAdvancedTimer timer;

        User user = new User();

        public NewRecord(User _user)
        {
            user = _user;
            InitializeComponent();
            timer = DependencyService.Get<IAdvancedTimer>();
            stopTimerButton.IsVisible = false;
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(41.068044, 29.014540), Distance.FromKilometers(1)));
            var pin = new Pin
            {
                Type = PinType.Place,
                Address = "Pin1",
                Label = "Label",
                Position = new Xamarin.Forms.Maps.Position(41.010738, 29.005470),
            };
            MyMap.Pins.Add(pin);
        }

        public void startTimer(object sender, EventArgs e)
        {
            timer.initTimer(1000, timerElapsed, true);
            timer.startTimer();
            startTimerButton.IsVisible = false;
            stopTimerButton.IsVisible = true;
            dtStart = DateTime.Now;

            record = new Record //record oluştur
            {
                CreatedDate = DateTime.Now,
                UserId = user.Id
            };

            dtStart = DateTime.Now;
        }

        public async void stopTimer(object sender, EventArgs e)
        {
            timer.stopTimer();
            timerCounter = 0;
            
            lblaltitude.Text = 0.ToString();
            lblspeed.Text = 0.ToString();
            startTimerButton.IsVisible = true;
            stopTimerButton.IsVisible = false;
            record.Duration = span.Hours + ":" + span.Minutes + ":" + span.Seconds;
            await manager.Insert(record);
            var lastRecord = await manager.GetLastRecord(user.Id);
            foreach (var item in pointList)
            {
                item.RecordId = lastRecord.Id;
                await manager.Insert(item);
            }
            pointList.Clear();
            position = null;
            record = null;
            mobileresult = null;
            point = null;
            await DisplayAlert("Success", "Record saved your list", "Ok", "Cancel");
        }

        public async void timerElapsed(object sender, EventArgs e)
        {
            timerCounter++;
            // her saniye bu fonksiyon çalışacak
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                dtLast = new DateTime();
                dtLast = DateTime.Now;
                span = dtLast.Subtract(dtStart);
                lblTimer.Text = span.Hours + ":" + span.Minutes + ":" + span.Seconds;

                position = await GetCurrentLocation();
                //var lastrecord = await manager.GetLastRecord(1);

                point = new Models.Point
                {
                    Id = 0,
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                    Altitude = 15.154,
                    RecordId = 0 //henüz record id yok
                };
                pointList.Add(point);

                lblaltitude.Text = Math.Round(position.Altitude, 2).ToString() + " m";
                lblspeed.Text = Math.Round(position.Speed, 2).ToString() + " km/h";
            });
        }

        void CreateMap()
        {
            Map map = new Map()
            {
                HasScrollEnabled = true,
                HasZoomEnabled = true,
                MapType = MapType.Hybrid,
                IsShowingUser = true
            };

            Pin pin = new Pin
            {
                Type = PinType.Place,
                Address = "Microsoft Türkiye",
                Label = "Microsoft Türkiye",
                Position = new Xamarin.Forms.Maps.Position(41.068044, 29.014540)
            };

            map.Pins.Add(pin);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(41.068044, 29.014540), Distance.FromKilometers(1)));
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
            //var lastrecord = await manager.GetLastRecord(1);

            point = new Models.Point
            {
                Id = 0,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Altitude = 15.154,
                RecordId = 0 //henüz record id yok
            };
            pointList.Add(point);
            
            lblaltitude.Text = position.Altitude.ToString();
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                //position = await locator.GetLastKnownLocationAsync();

                //if (position != null)
                //{
                //    //got a cahched position, so let's use it.
                //    return position;
                //}

                //if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                //{
                //    //not available or enabled
                //    //return;
                //}

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1), null, true);

            }
            catch (Exception ex)
            {
                //Display error as we have timed out or can't get location.
            }

            if (position == null)
                return null;

            return position;
        }
    }
}