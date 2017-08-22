using flightR.Models;
using flightR.Views.Tabs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flightR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        User user = new User();
        public MainPage(User _user)
        {
            user = _user;
            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Profile", null, () => { }));

            Children.Add(new NewRecord(user));
            Children.Add(new Profile(user));
            Children.Add(new Settings(user));
        }
    }
}