namespace Cinema.VIews;

public partial class HomeScreen : ContentPage
{
	public HomeScreen()
	{
		InitializeComponent();
	}

	private async void OnTap(object sender, TappedEventArgs e)
	{
        await Navigation.PushAsync(new DetaljiFilmaPage());
    }

}