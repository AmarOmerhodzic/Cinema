using Cinema.VIews;
using Cinema.VIews.auth;

namespace Cinema
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string token = Preferences.Get("AuthToken", string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
            {
                MainPage = new NavigationPage(new HomeScreen());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
           
        }
    }
}