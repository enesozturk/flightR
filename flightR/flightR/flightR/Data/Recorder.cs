using flightR.Models;
using flightR.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace flightR.Data
{
    public class Recorder
    {
        public static IList<Point> records { get; set; }

        static Recorder()
        {
            records = new ObservableCollection<Point>()
            {
                new Point()
                {
                    Altitude = 23.122334,
                    Latitude = 54.235413,
                    Longitude = 12.344565
                },
                new Point()
                {
                    Altitude = 23.122334,
                    Latitude = 55.235413,
                    Longitude = 12.344565
                },
                new Point()
                {
                    Altitude = 23.122334,
                    Latitude = 56.235413,
                    Longitude = 12.344565
                },
                new Point()
                {
                    Altitude = 23.122334,
                    Latitude = 57.235413,
                    Longitude = 12.344565
                },
                new Point()
                {
                    Altitude = 23.122334,
                    Latitude = 58.235413,
                    Longitude = 12.344565
                }
            };
        }

        public static ObservableCollection<Grouping<double,Point>> BindingWithGrouping()
        {
            var result = records;

            var list = new ObservableCollection<Grouping<double, Point>>
                (result.
                OrderBy(c => c.Altitude).
                GroupBy(c=>c.Longitude).
                Select(k=>new Grouping<double, Point>(k.Key, k))
                );

            return list;
        }

        public static void AddNewRecord(Point newRecord)
        {
            records.Add(newRecord);
        }
    }
}
