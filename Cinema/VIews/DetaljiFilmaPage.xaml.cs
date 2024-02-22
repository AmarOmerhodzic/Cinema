namespace Cinema.VIews;

public partial class DetaljiFilmaPage : ContentPage
{
	public DetaljiFilmaPage()
	{
		InitializeComponent();
	}
    private async void OnReserveTicketClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RezervacijaKartePage());
    }
}