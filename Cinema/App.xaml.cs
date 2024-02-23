using Cinema.Views;
using Cinema.VIews;
using Cinema.VIews.auth;

namespace Cinema
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Dark;
            string token = Preferences.Get("AuthToken", string.Empty);
            string adminToken = Preferences.Get("AdminAuthToken", string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
            {
                MainPage = new NavigationPage(new HomeScreen());
            }else if (!string.IsNullOrWhiteSpace(adminToken))
            {
                MainPage = new NavigationPage(new HomeAdminScreen());
            }
            
            else
            {
                MainPage = new NavigationPage(new Login());
            }
           
        }
    }
}