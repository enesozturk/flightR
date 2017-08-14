using flightR.Models;
using flightR.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Insert : ContentPage
    {
        public Insert()
        {
            InitializeComponent();
        }

        private async void onSave(object sender, EventArgs e)
        {
            Record newRecord = new Record
            {
                Latitude = Convert.ToDouble(txtLatitude.Text),
                Longitude = Convert.ToDouble(txtLongitude.Text),
                Altitude = Convert.ToDouble(txtAltitude.Text)
            };

            ServiceManager manager = new ServiceManager();
            MobileResult mobileResult = await manager.Insert(newRecord);

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
    }
}