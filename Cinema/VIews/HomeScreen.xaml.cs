
using Cinema.Models;
using System.Collections.ObjectModel;

namespace Cinema.VIews;

public partial class HomeScreen : ContentPage
{

    //public ObservableCollection<FilmPregledModel> Filmovi { get; set; }
    public HomeScreen()
	{

		InitializeComponent();


        /*Filmovi = new ObservableCollection<FilmPregledModel>
        {
            new FilmPregledModel(naziv: "Black Panther", slika: "https://m.media-amazon.com/images/M/MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_.jpg", opis: "Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt ...", ocjena: "4.5", zanr: "Action" ),
            
            
        };*/

        BindingContext = this;





        NavigationPage.SetHasBackButton(this, false);

        string userPreference = Preferences.Get("Username", string.Empty);

        
        string welcomeMessage = "Welcome " + userPreference;

        userLabel.Text = welcomeMessage;
    }

	private async void OnTap(object sender, TappedEventArgs e)
	{
        await Navigation.PushAsync(new DetaljiFilmaPage());
    }

    private async void ProfilRoute(object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ProfilRezervacije());
    }

}