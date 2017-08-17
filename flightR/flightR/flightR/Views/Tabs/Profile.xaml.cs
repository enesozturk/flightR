using flightR.Models;
using flightR.Provider;
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
                
                lbl.Text = model.Count.ToString();
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
        }

        public async void onSelected(object sender, EventArgs e)
        {
            //
        }

        public async void onDelete(object sender, EventArgs e)
        {
            //
        }

    }
}