using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Provider
{
    public class myPoint
    {
        public async Task<Position> GetLocation()
        {
            Position position = null;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            position = await locator.GetPositionAsync(10000);

            return position;
        }
    }

    
}
