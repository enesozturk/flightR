using flightR.Models;
using flightR.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace flightR.ViewModels
{
    public class RecordDetail
    {
        private Record _record { get; set; }
        public Record Record
        {
            get
            {
                return _record;
            }
            set
            {
                _record = value;

                Application.Current.MainPage.Navigation.PushAsync(new RecordDetailPage(_record));
            }
        }
    }
}
