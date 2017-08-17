using flightR.Models;
using flightR.Provider;
using flightR.Views.Modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        ServiceManager service = new ServiceManager();
        readonly ObservableCollection<Models.Record> model = new ObservableCollection<Models.Record>();

        public Profile()
        {
            InitializeComponent();
            loadData();
        }

        public async void loadData()
        {
            this.IsBusy = true;
            try
            {
                model.Clear();
                var records = await service.GetRecords();
                foreach (Models.Record item in records)
                    model.Add(item);

                lblCount.Text = model.Count.ToString() + "Record(s)";
                recordList.ItemsSource = model;
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public void getRecords(object sender, EventArgs e)
        {
            loadData();
        }

        public void onRefresh(object sender, EventArgs e)
        {
            loadData();
            recordList.IsRefreshing = false;
        }

        public async void onTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RecordDetail()));
        }

        public async void onDelete(object sender, EventArgs e)
        {
            //
        }

    }
}