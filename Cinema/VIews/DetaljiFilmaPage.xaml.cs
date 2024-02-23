using Cinema.Models;

namespace Cinema.VIews;

public partial class DetaljiFilmaPage : ContentPage
{
    
    
	public DetaljiFilmaPage()
	{
		InitializeComponent();
        
    }
    private async void OnReserveTicketClicked(object sender, EventArgs e)
    {
        if (BindingContext is Film selectedFilm)
        {
            // Pass the filmId as a parameter when navigating to the RezervacijaKartePage
            await Navigation.PushAsync(new RezervacijaKartePage(selectedFilm.Id));
        }
    }
}