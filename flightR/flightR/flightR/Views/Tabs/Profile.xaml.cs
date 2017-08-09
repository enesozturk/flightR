using flightR.Data;
using flightR.Helper;
using flightR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        SQLiteManager manager;
        public Profile()
        {
            InitializeComponent();

            List<FlightRecord> records = new List<FlightRecord>();
            manager = new SQLiteManager();
            records = manager.GetAll().ToList();

            lstStudents.BindingContext = records;
        }

        public void onMenuRefresh(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            List<FlightRecord> studentList = new List<FlightRecord>();
            studentList = manager.GetAll().ToList();
            lstStudents.BindingContext = studentList;
            lstStudents.IsRefreshing = false;
        }
    }
}