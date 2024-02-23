
using Cinema.Data;
using Cinema.Models;
using System.Collections.ObjectModel;

namespace Cinema.VIews;


public partial class HomeScreen : ContentPage
{
    
    
    public HomeScreen()
	{

		InitializeComponent();

        NavigationPage.SetHasBackButton(this, false);

        string userPreference = Preferences.Get("Username", string.Empty);

        
        string welcomeMessage = "Welcome " + userPreference;

        userLabel.Text = welcomeMessage;

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateListView();
    }

    private async Task UpdateListView()
    {
        FilmDatabase database = await FilmDatabase.Instance;
        //await database.UnosTest();
        listView.ItemsSource = await database.SviFilmovi();
    }



    private async void ProfilRoute(object sender, System.EventArgs e)
    {
        zanrPicker.SelectedItem = null;
        await Navigation.PushAsync(new ProfilRezervacije());
    }

    private async void onSelect(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            zanrPicker.SelectedItem = null;
            await Navigation.PushAsync(new DetaljiFilmaPage
            {
                BindingContext = e.SelectedItem as Film
            });
        }
    }
    private async void OnGenreSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedGenre = zanrPicker.SelectedItem as string;
        await FilterFilmsByGenre(selectedGenre);
        
        
    }
    private async void OnClearFilterClicked(object sender, EventArgs e)
    {
        zanrPicker.SelectedItem = null;
        await UpdateListView();
    }

    private async Task FilterFilmsByGenre(string genre)
    {
        FilmDatabase database = await FilmDatabase.Instance;
        listView.ItemsSource = await database.GetFilmoviPoZanru(genre);
    }
}