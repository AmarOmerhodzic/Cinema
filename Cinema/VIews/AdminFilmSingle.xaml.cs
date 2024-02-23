using Microsoft.Maui.Controls;
using Cinema.Models;
using Cinema.Data;

namespace Cinema.Views
{
    public partial class NewPage1 : ContentPage
    {
        public Film Film { get; set; } = new Film();

        public NewPage1()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var filmDatabase = await FilmDatabase.Instance;

            // Populate the Film object with data from the UI
            Film film = new Film
            {
                Slika = ImageUrlEntry.Text,
                Naziv = NazivFilmaEntry.Text,
                Opis = OpisFilmaEntry.Text,
                Zanr = ZanrPicker.SelectedItem?.ToString(),
                Trajanje = Convert.ToInt32(TrajanjeEntry.Text),
                Trailer = TrailerUrlEntry.Text,
                Ocjena = Convert.ToDouble(OcjenaEntry.Text)
            };

            // Save the film to the database
            bool success = await filmDatabase.CreateFilm(film);

            if (success)
            {
                // Display a success message
                await DisplayAlert("Success", "Film saved successfully.", "OK");

                await Navigation.PushAsync(new HomeAdminScreen());

            }
            else
            {
                // Display an error message
                await DisplayAlert("Error", "Failed to save film.", "OK");
            }
        }
    }
}
