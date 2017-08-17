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
        public int buttonCounter { get; set; } = 0; // to stop
        public Plugin.Geolocator.Abstractions.Position Position { get; set; }

        public List<Models.Point> newList;
        private ServiceManager manager = new ServiceManager();

        public NewRecord()
        {
            InitializeComponent();
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

        private async void btnRecord(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new Insert());
            await GetCurrentLocation(); // pozisyon bilgileri al

            Record newRecord = new Record //yeni kayıt modeli oluştur
            {
                CreatedDate = DateTime.Now,
                UserId = 1
            };

            await manager.NewRecord(newRecord); //kaydı db ye gonder

            var record = await manager.GetLastRecord(1); //son oluşturulan kaydı al

            Models.Point newPoint = new Models.Point //yeni nokta oluştur
            {
                Latitude = 2.222,
                Longitude = 3.333,
                Altitude = 1.111,
                PointListId = record.Id //yeni noktaya son kayıdın id'sini ver
            };

            MobileResult mobileResult = await Task.Run(() => manager.Insert(newPoint));// oluşturulan point modelini dbye kaydet

            if (mobileResult.Result) //kaydedildi mesajı göster
            {
                await DisplayAlert("Success", mobileResult.Message, "Ok", "Cancel");
                Navigation.PopModalAsync();
            }
            else
            {
                DisplayAlert("Error", mobileResult.Message, "Ok", "Cancel");
            }
        }

        private async Task GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            //lblLat.Text = position.Latitude.ToString();
            //lblLong.Text = position.Longitude.ToString();
            //lblAlt.Text = position.Altitude.ToString();

            Position = position;
        }
    }
}