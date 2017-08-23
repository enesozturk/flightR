using flightR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private User user = new User();
        public Settings(User _user)
        {
            user = _user;
            InitializeComponent();

            regionPicker.Items.Add("Serdivan/Sakarya");
            regionPicker.Items.Add("Kartepe/Kocaeli");
            regionPicker.Items.Add("Ölüdeniz/Fethiye");
            regionPicker.SelectedIndex = 0;
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.773364, 30.333172), Distance.FromKilometers(1)));
        }

        public void indexChanged(object sender, EventArgs e)
        {
            var index = regionPicker.SelectedIndex;
            switch (index)
            {
                case 0:
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 390 m",
                        Label = "Speed: 37 km/h",
                        Position = new Position(40.777355, 30.336812)
                    };
                    var pin2 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 410 m",
                        Label = "Speed: 25 km/h",
                        Position = new Position(40.778392, 30.333871)
                    };
                    var pin3 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 150 m",
                        Label = "Speed: 20 km/h",
                        Position = new Position(40.780540, 30.340483)
                    };
                    MyMap.Pins.Add(pin);
                    MyMap.Pins.Add(pin2);
                    MyMap.Pins.Add(pin3);

                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.773364, 30.333172), Distance.FromKilometers(1)));
                    break;
                case 1:
                    var kartepe = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 690 m",
                        Label = "Speed: 37 km/h",
                        Position = new Position(40.683542, 30.065719)
                    };
                    var kartepe2 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 510 m",
                        Label = "Speed: 25 km/h",
                        Position = new Position(40.686493, 30.055993)
                    };
                    var kartepe3 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 750 m",
                        Label = "Speed: 20 km/h",
                        Position = new Position(40.688488, 30.060775)
                    };
                    var kartepe4 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 890 m",
                        Label = "Speed: 37 km/h",
                        Position = new Position(40.686276, 30.057271)
                    };
                   
                    MyMap.Pins.Add(kartepe);
                    MyMap.Pins.Add(kartepe2);
                    MyMap.Pins.Add(kartepe3);
                    MyMap.Pins.Add(kartepe4);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.677941, 30.060483), Distance.FromKilometers(1)));
                    break;
                case 2:
                    var fethiye = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 690 m",
                        Label = "Speed: 37 km/h",
                        Position = new Position(36.539424, 29.123649)
                    };
                    var fethiye2 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 510 m",
                        Label = "Speed: 25 km/h",
                        Position = new Position(36.548174, 29.114588)
                    };
                    var fethiye3 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 750 m",
                        Label = "Speed: 20 km/h",
                        Position = new Position(36.547852, 29.121538)
                    };
                    var fethiye4 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 890 m",
                        Label = "Speed: 37 km/h",
                        Position = new Position(36.537183, 29.132314)
                    };
                    var fethiye5 = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: 1110 m",
                        Label = "Speed: 25 km/h",
                        Position = new Position(36.546068, 29.140527)
                    };
                    MyMap.Pins.Add(fethiye);
                    MyMap.Pins.Add(fethiye2);
                    MyMap.Pins.Add(fethiye3);
                    MyMap.Pins.Add(fethiye4);
                    MyMap.Pins.Add(fethiye5);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(36.549093, 29.117040), Distance.FromKilometers(1)));
                    break;
                default:
                    break;
            }
        }
    }
}