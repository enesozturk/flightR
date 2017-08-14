using flightR.Models;
using flightR.Provider;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        ServiceManager service = new ServiceManager();
        public Profile()
        {
            InitializeComponent();
        }

        public async void getRecords(object sender, EventArgs e)
        {
            IEnumerable<Record> records = new List<Record>();
            records = await service.GetAll();

            lstStudents.BindingContext = records;
        }

        public void onMenuRefresh(object sender, EventArgs e)
        {
            RefreshData();
        }

        public async void onMenuDelete(object sender, EventArgs e)
        {

        }

        private async void RefreshData()
        {
            IEnumerable<Record> studentList = new List<Record>();
            studentList = await service.GetAll();
            lstStudents.BindingContext = studentList;
            lstStudents.IsRefreshing = false;
        }
    }
}