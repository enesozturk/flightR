using flightR.Views.Tabs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Children.Add(new NewRecord());
            Children.Add(new Profile());
            Children.Add(new Settings());
        }
    }
}