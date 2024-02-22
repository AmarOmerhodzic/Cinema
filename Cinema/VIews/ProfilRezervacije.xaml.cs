using Cinema.VIews.auth;

namespace Cinema.VIews;

public partial class ProfilRezervacije : ContentPage
{
	public ProfilRezervacije()
	{
		InitializeComponent();
	}

    private async void Logout(object sender, EventArgs e)
    {
        Preferences.Clear();
        Application.Current.MainPage = new NavigationPage(new Login());
    }
}