using Cinema.Data;
using Cinema.Models;
using Cinema.VIews.auth;

namespace Cinema.VIews;

public partial class ProfilRezervacije : ContentPage
{
    private int userId;
    public ProfilRezervacije()
	{

		InitializeComponent();

        string userPreference = Preferences.Get("Username", string.Empty);
        double userRacun = Preferences.Get("UserRacun", -1.0);

        string welcomeMessage = "Welcome " + userPreference;
        string racunMessage = "Your account balance is: " + userRacun;

        userLabel.Text = welcomeMessage;
        userRacunLabel.Text = racunMessage;

        this.userId = Preferences.Get("UserId", -1);

        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateListView();
    }

    private async Task UpdateListView()
    {
        RezervacijeDatabase database = await RezervacijeDatabase.Instance;
        ProjekcijaDatabase projekcijaDatabase = await ProjekcijaDatabase.Instance;
        List<ProfilRezervacijeModel> model = new List<ProfilRezervacijeModel>();
        //await database.UnosTest();
        var rezervacijekorisnika = await database.GetRezervacijePoKorisnikId(userId);
        foreach(var rezervacija in rezervacijekorisnika)
        {
            var projekcija = await projekcijaDatabase.GetProjekcijuPoId(rezervacija.ProjekcijaId);
            if (projekcija != null)
            {
                var tmp = new ProfilRezervacijeModel()
                {
                    RezervacijaId = rezervacija.Id,
                    DatumVrijeme = projekcija.DatumVrijeme,
                    Sjediste = rezervacija.Sjediste
                };

                model.Add(tmp);
                    
            }
        }
        listView.ItemsSource = model;
    }

    private async void Logout(object sender, EventArgs e)
    {
        Preferences.Clear();
        Application.Current.MainPage = new NavigationPage(new Login());
    }
    private async void onSelect(object sender, SelectedItemChangedEventArgs e)
    {
        
    }
}