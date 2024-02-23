namespace Cinema.VIews;

using Cinema.Data;
using Cinema.Models;
using Microsoft.Maui.Controls;
using QRCoder;


public partial class RezervacijaKartePage : ContentPage
{
    private int projkecijaId;
    private double projkecijaCijena;
    private int filmId;
    private int userId;

    public RezervacijaKartePage(int filmId)
	{
		InitializeComponent();
        this.filmId = filmId;
        InitializeAsync();
        this.userId = Preferences.Get("UserId", -1);



    }
    private async void InitializeAsync()
    {
        await LoadFilms();
    }
    private async Task LoadFilms()
    {
        ProjekcijaDatabase database = await ProjekcijaDatabase.Instance;
        var projekcije = await database.GetProjekcijePoFilmId(filmId);
        ProjekcijePicker.ItemsSource = projekcije;
        /* foreach (var film in films)
         {
             FilmPicker.Items.Add(film.Naziv); // Assuming film.Naziv is the name of the film
             filmIdMap.Add(film.Naziv, film.Id); // Add film name and ID to the dictionary
         }
         */
    }
    private void FilmPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedFilmName = (Projekcija)ProjekcijePicker.SelectedItem;
        // Get the ID of the selected film using the dictionary
        projkecijaId = selectedFilmName.Id;
        projkecijaCijena = selectedFilmName.Cijena;

    }

    /*private void OnGenerateClicked(object sender, EventArgs e)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(selectedSeatLabel.Text, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }*/

    private void OnSeatSelected(object sender, EventArgs e)
    {
        // Handle seat selection event
        var selectedSeatButton = (Button)sender;
        string selectedSeat = selectedSeatButton.Text;
        selectedSeatLabel.Text = selectedSeat;
        // You can perform actions such as highlighting the selected seat or storing it for later use
    }

    private async void OnConfirmBookingClicked(object sender, EventArgs e)
    {
        var rezervacijaDatabase = await RezervacijeDatabase.Instance;
        var korisnikDatabase = await KorisnikDatabase.Instance;

        // Populate the Film object with data from the UI
        Rezervacije rezervacije = new Rezervacije
        {
            KorisnikId = userId,
            ProjekcijaId = projkecijaId,
            Status = StatusRezervacije.aktivna,
            Sjediste = selectedSeatLabel.Text

        };

        // Save the film to the database
        int rezervacijaId = await rezervacijaDatabase.CreateRezervaciju(rezervacije);


        if (rezervacijaId != -1)
        {
            bool azuriranjeRacuna = await korisnikDatabase.AzurirajStanjeNaRacunu(userId, projkecijaCijena);

            if(azuriranjeRacuna)
            {
                await DisplayAlert("Success", "Reservation saved successfully.", "OK");
                await Navigation.PushAsync(new HomeScreen());
            }
            else
            {
                bool brisanjeRezervacije = await rezervacijaDatabase.DeleteRezervaciju(rezervacijaId);
                await DisplayAlert("Success", "Nemate dovoljno novca", "OK");

            }         
          

        }
        else
        {
            // Display an error message
            await DisplayAlert("Error", "Failed to save film.", "OK");
        }
        /*QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(selectedSeatLabel.Text, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));*/
    }
}