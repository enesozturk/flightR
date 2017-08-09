using flightR.Models;
using flightR.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Data
{
    public class Recorder
    {
        public static IList<FlightRecord> records { get; set; }

        static Recorder()
        {
            records = new ObservableCollection<FlightRecord>()
            {
                new FlightRecord()
                {
                    Altitude = 23.122334,
                    Latitude = 54.235413,
                    Longitude = 12.344565
                },
                new FlightRecord()
                {
                    Altitude = 23.122334,
                    Latitude = 55.235413,
                    Longitude = 12.344565
                },
                new FlightRecord()
                {
                    Altitude = 23.122334,
                    Latitude = 56.235413,
                    Longitude = 12.344565
                },
                new FlightRecord()
                {
                    Altitude = 23.122334,
                    Latitude = 57.235413,
                    Longitude = 12.344565
                },
                new FlightRecord()
                {
                    Altitude = 23.122334,
                    Latitude = 58.235413,
                    Longitude = 12.344565
                }
            };
        }

        public static ObservableCollection<Grouping<double,FlightRecord>> BindingWithGrouping()
        {
            var result = records;

            var list = new ObservableCollection<Grouping<double, FlightRecord>>
                (result.
                OrderBy(c => c.Altitude).
                GroupBy(c=>c.Longitude).
                Select(k=>new Grouping<double, FlightRecord>(k.Key, k))
                );

            return list;
        }

        public static void AddNewRecord(FlightRecord newRecord)
        {
            records.Add(newRecord);
        }
    }
}
