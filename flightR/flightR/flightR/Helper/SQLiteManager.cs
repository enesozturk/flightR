using flightR.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace flightR.Helper
{
    public class SQLiteManager
    {
        SQLiteConnection _sqlconnection;

        public SQLiteManager()
        {
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<FlightRecord>();
        }

        #region CRUD
        public int Insert(FlightRecord f)
        {
            return _sqlconnection.Insert(f);
        }

        public int Update(FlightRecord f)
        {
            return _sqlconnection.Update(f);
        }

        public int Delete(int Id)
        {
            return _sqlconnection.Delete<FlightRecord>(Id);
        }

        public IEnumerable<FlightRecord> GetAll()
        {
            //return (from i in _sqlconnection.Table<Student>() select i);
            return _sqlconnection.Table<FlightRecord>();
        }

        public FlightRecord Get(int Id)
        {
            return _sqlconnection.Table<FlightRecord>().Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Dispose()
        {
            _sqlconnection.Dispose();
        }
        #endregion
    }
}
