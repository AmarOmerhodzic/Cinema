using Cinema.Data;
using Cinema.Models;

namespace Cinema.VIews.auth;

public partial class Registracija : ContentPage
{
	public Registracija()
	{
		InitializeComponent();
	}
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        // Validacija unesenih podataka
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Please enter email, password, and confirm password.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Password and confirm password do not match.", "OK");
            return;
        }

        // Kreiranje novog korisnika
        Korisnik noviKorisnik = new Korisnik
        {
            KorisnickoIme = email,
            Lozinka = password,
            StanjeNaRacunu = 20, // Mozete postaviti inicijalno stanje racuna po potrebi
            Uloga = Uloga.Korisnik // Mozete postaviti ulogu korisnika po potrebi
        };

        // Dodavanje novog korisnika u bazu podataka
        KorisnikDatabase database = await KorisnikDatabase.Instance;
        bool registracijaUspjesna = await database.RegistrirajKorisnika(noviKorisnik);

        if (registracijaUspjesna)
        {
            await DisplayAlert("Success", "Registration successful!, You recieved 20$ bonus for registration", "OK");
            // Nakon uspjesne registracije, mozete preusmjeriti korisnika na ekran prijave ili obaviti druge radnje
            // For example:
             await Navigation.PopAsync(); // Povratak na ekran prijave
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }

    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        // Povratak na ekran prijave
        await Navigation.PopAsync();
    }
}