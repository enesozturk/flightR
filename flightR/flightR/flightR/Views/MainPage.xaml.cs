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

            ToolbarItems.Add(new ToolbarItem("Profile", null, () => { }));
            ToolbarItems.Add(new ToolbarItem("Settings", null, () => { }));

            Children.Add(new NewRecord());
            Children.Add(new Profile());
            Children.Add(new Settings());
        }
    }
}