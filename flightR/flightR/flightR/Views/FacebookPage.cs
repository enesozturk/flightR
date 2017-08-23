using flightR.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace flightR.Views
{
    public class FacebookPage : ContentPage
    {
        //https://www.facebook.com/dialog/oauth?client_id=
        //1460923340641913&redirect_uri=
        //https://www.facebook.com/connect/login_success.html
        private string FacebookClientId = "1460923340641913";
        public FacebookPage()
        {
            this.BackgroundColor = Color.White;

            var apiRequest = "https://www.facebook.com/dialog/oauth?client_id="
                + FacebookClientId
                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebView_Navigated;

            Content = webView;
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokeFromUrl(e.Url);

            if (!String.IsNullOrEmpty(accessToken))
            {
                ServiceManager manager = new ServiceManager();
                var profile = await manager.GetFacebookProfile(accessToken);
                var name = profile.Name;
            }
        }

        private string ExtractAccessTokeFromUrl(string uri)
        {
            if(uri.Contains("access_token") && uri.Contains("&expires_in="))
            {
                var http = "https";
                if (Xamarin.Forms.Device.OS == TargetPlatform.Windows || Xamarin.Forms.Device.OS == TargetPlatform.WinPhone)
                    http = "http";
                uri = uri.Replace(http + "://www.facebook.com/connect/login_success.html#access_token=", "");
                var accessToken = uri.Remove(uri.IndexOf("&expires_in="));
                return accessToken;
            }
            return "";
        }
    }
}
