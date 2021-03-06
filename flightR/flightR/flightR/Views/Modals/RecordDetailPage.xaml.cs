﻿using flightR.Models;
using flightR.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace flightR.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordDetailPage : ContentPage
    {
        private ServiceManager manager = new ServiceManager();
        private ObservableCollection<Models.Point> points;

        private Record record { get; set; }

        public double MaxSpeed { get; set; }
        public double MaxAltitude { get; set; }

        public RecordDetailPage(Record _record)
        {
            InitializeComponent();
            record = _record;
            this.BackgroundColor = Color.White;
            GetPoints();

            flightSlider.Minimum = 0f;
            flightSlider.Value = 0;
            sliderValue.Text = flightSlider.Value.ToString();
            lblDuration.Text = "Duration: " + record.Duration.ToString();
        }

        public void drawPoints(ObservableCollection<Models.Point> list, int count)
        {
            if (list.Count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Address = "Alt: " + list[i].Altitude.ToString() + " m",
                        Label = "Speed: " + list[i].Speed.ToString() + " km/h",
                        Position = new Position(list[i].Latitude, list[i].Longitude)
                    };
                    rdMap.Pins.Add(pin);
                }
            }
            else
            {
                DisplayAlert("Not so long", "This list does not have item more than one.", "OK");
            }
        }

        private async void GetPoints()
        {
            var _points = await manager.GetPoints();
            var points2 = _points.Where(x => x.RecordId == record.Id);
            points = new ObservableCollection<Models.Point>(points2);

            var maxSliderValue = points.Count();
            flightSlider.Maximum = maxSliderValue;
            flightSlider.ValueChanged += FlightSlider_ValueChanged;

            var firstMapPoint = points.FirstOrDefault();

            rdMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(firstMapPoint.Latitude, firstMapPoint.Longitude), Distance.FromKilometers(1)));

            var maxspeed = points.Max(x => x.Speed).ToString();
            var maxalt = points.Max(x => x.Altitude).ToString();

            if (maxspeed != null && maxalt != null)
            {
                lblMaxSpeed.Text = "Maximum Speed: " + maxspeed.ToString() + " km/h";
                lblMaxAltitude.Text = "Maximum Altitude: " + maxalt.ToString() + " m";
            }
            else
            {
                lblMaxSpeed.Text = "Maximum Speed: - ";
                lblMaxAltitude.Text = "Maximum Altitude: - ";
            }
        }


        private void FlightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = e.NewValue;
            var valueInt = Convert.ToInt32(e.NewValue);

            drawPoints(points, valueInt);

            sliderValue.Text = flightSlider.Value.ToString();
        }
    }
}