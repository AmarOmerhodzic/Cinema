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
        await Navigation.PopToRootAsync();
        await Navigation.PushModalAsync(new Login());
    }
}