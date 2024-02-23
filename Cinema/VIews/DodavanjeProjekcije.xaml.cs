using System;
using Cinema.Data;
using Cinema.Models;
using Microsoft.Maui.Controls;

namespace Cinema.Views
{
    public partial class DodavanjeProjekcije : ContentPage
    {
        private Dictionary<string, int> filmIdMap;

        private int filmId;
        public DodavanjeProjekcije()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            await LoadFilms();
        }
        private async Task LoadFilms()
        {
            filmIdMap = new Dictionary<string, int>(); // Initialize the dictionary
            FilmDatabase database = await FilmDatabase.Instance;
            var films = await database.SviFilmovi();
            FilmPicker.ItemsSource = films;
           /* foreach (var film in films)
            {
                FilmPicker.Items.Add(film.Naziv); // Assuming film.Naziv is the name of the film
                filmIdMap.Add(film.Naziv, film.Id); // Add film name and ID to the dictionary
            }
            */
        }
        private void FilmPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedFilmName = (Film)FilmPicker.SelectedItem;
            // Get the ID of the selected film using the dictionary
            filmId = selectedFilmName.Id;
            
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            // Create a new Projekcija object with user input
            Projekcija projekcija = new Projekcija
            {
                FilmId = filmId,
                DatumVrijeme = DatumVrijemePicker.Date,
                BrojKarata = string.IsNullOrEmpty(BrojKarataEntry.Text) ? null : (int?)int.Parse(BrojKarataEntry.Text),
                Cijena = double.Parse(CijenaEntry.Text)
            };

            // Save the Projekcija object to the SQLite database
            var dbInstance = await ProjekcijaDatabase.Instance;
            bool success = await dbInstance.CreateProjekciju(projekcija);

            if (success)
            {
                await DisplayAlert("Success", "Projekcija successfully saved.", "OK");
                await Navigation.PushAsync(new HomeAdminScreen());
            }
            else
            {
                await DisplayAlert("Error", "Failed to save projekcija.", "OK");
            }
        }
    }
}
