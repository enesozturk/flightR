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
            regionPicker.Items.Add("Alidağı/Kayseri");
            regionPicker.Items.Add("Ölüdeniz/Fethiye");
            regionPicker.SelectedIndex = 0;
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.755190, 30.337009), Distance.FromKilometers(1)));
        }

        public void indexChanged(object sender, EventArgs e)
        {
            var index = regionPicker.SelectedIndex;
            switch (index)
            {
                case 0:
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.755190, 30.337009), Distance.FromKilometers(1)));
                    break;
                case 1:
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(40.745440, 30.019646), Distance.FromKilometers(1)));
                    break;
                case 2:
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(38.662137, 35.551897), Distance.FromKilometers(1)));
                    break;
                case 3:
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(36.549093, 29.117040), Distance.FromKilometers(1)));
                    break;
                default:
                    break;
            }
        }
    }
}