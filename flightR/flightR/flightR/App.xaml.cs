using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using flightR;
using flightR.Views;    

using Xamarin.Forms;
using Java.Util;

namespace flightR
{
    public partial class App : Application
    {
        public static string DbName { get; set; } = "flightRDB.db3";
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
