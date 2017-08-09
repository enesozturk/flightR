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
using flightR.Droid.Helper;
using flightR.Helper;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceHelper))]
namespace flightR.Droid.Helper
{
    public class DeviceHelper : IDeviceHelper
    {
        public string GetDeviceName()
        {
            return "Android";
        }
    }
}