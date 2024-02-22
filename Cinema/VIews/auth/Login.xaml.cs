using Cinema.Data;
using Cinema.Models;
using Cinema.Views;

namespace Cinema.VIews.auth;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter email and password.", "OK");
            return;
        }

        KorisnikDatabase database = await KorisnikDatabase.Instance;

        Korisnik korisnik = await database.PrijaviKorisnika(email, password);

        if (korisnik != null)
        {

            // Provjera uloge korisnika
            if (korisnik.Uloga == Uloga.Admin)
            {
                string bijesanToken = "bijesanadmintoken";
                Preferences.Set("AdminAuthToken", bijesanToken);
                // Ako je uloga korisnika admin, preusmjeri ga na HomeAdmin
                await Navigation.PushAsync(new HomeAdminScreen());
            }
            else
            {
                // sacuvaj username korisnika
                string userPrefrence = korisnik.KorisnickoIme;
                Preferences.Set("Username", userPrefrence);
                string bijesanToken = "bijesantoken";
                Preferences.Set("AuthToken", bijesanToken);
                // Inace, preusmjeri ga na HomeScreen
                await Navigation.PushAsync(new HomeScreen());
            }
            await DisplayAlert("Success", "Login successful!", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Invalid email or password.", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navigate to the registration page
        // For example:
        await Navigation.PushAsync(new Registracija());
        //await DisplayAlert("Register", "Navigate to the registration page.", "OK");
    }
}