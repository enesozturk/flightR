using flightR.Models;
using flightR.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private ServiceManager manager = new ServiceManager();

        public Login()
        {
            InitializeComponent();

        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            var username = entryUsername.Text;
            var password = entryPassword.Text;
            var usermodel = new User
            {
                UserName = username,
                Password = password
            };
            var user = await manager.GetUser(usermodel);

            if (user != null)
            {
                Navigation.PushAsync(new MainPage(user));
            }
            else
            {
                await DisplayAlert("Not so long", "Wrong username or password", "OK");
            }
        }
    }
}