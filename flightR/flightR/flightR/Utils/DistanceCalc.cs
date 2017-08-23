using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Utils
{
    public static class DistanceCalc
    {

        public static double rad(double x)
        {
            return x * Math.PI / 180;
        }

        public static double distance(Models.Point p1, Models.Point p2)
        {
            var R = 6378137; // Earth’s mean radius in meter
            var dLat = rad(p2.Latitude - p1.Latitude);
            var dLong = rad(p2.Longitude - p1.Longitude);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(rad(p1.Longitude)) * Math.Cos(rad(p2.Longitude)) *
              Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d; // returns the distance in meter
        }
    }
}
