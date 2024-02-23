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
            SetupValidation();
        }

        private void SetupValidation()
        {
            OcjenaEntry.TextChanged += (sender, e) =>
            {
                if (sender is Entry entry)
                {
                    if (!string.IsNullOrEmpty(entry.Text) &&
                        (!double.TryParse(entry.Text, out double value) || value < 0 || value > 5))
                    {
                        // Display message
                        DisplayAlert("Error", "Please enter a value between 0 and 5.", "OK");

                        // Clear the text or set it to the nearest valid value
                        entry.Text = "";
                    }
                }
            };
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
                Trailer = TrailerUrlEntry.Text
            };

            // Validate and set Ocjena
            if (!string.IsNullOrEmpty(OcjenaEntry.Text) && double.TryParse(OcjenaEntry.Text, out double ocjenaValue))
            {
                if (ocjenaValue >= 0 && ocjenaValue <= 5)
                {
                    film.Ocjena = ocjenaValue;
                }
                else
                {
                    // Display message
                    await DisplayAlert("Error", "Please enter a value between 0 and 5.", "OK");
                    return;
                }
            }

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
