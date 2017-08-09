using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using flightR.Helper;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net;
using flightR.Droid.ConnectionHelper;

[assembly: Xamarin.Forms.Dependency(typeof(GetAndroidConnection))]
namespace flightR.Droid.ConnectionHelper
{
    public class GetAndroidConnection : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DbName);
            var platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(platform, path);
            return connection;
        }
    }
}