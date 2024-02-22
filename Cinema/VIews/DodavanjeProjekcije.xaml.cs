using System;
using Cinema.Data;
using Cinema.Models;
using Microsoft.Maui.Controls;

namespace Cinema.Views
{
    public partial class DodavanjeProjekcije : ContentPage
    {
        public DodavanjeProjekcije()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            // Create a new Projekcija object with user input
            Projekcija projekcija = new Projekcija
            {
                FilmId = int.Parse(FilmIdEntry.Text),
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
                // You can add navigation logic here to navigate to another page if needed.
            }
            else
            {
                await DisplayAlert("Error", "Failed to save projekcija.", "OK");
            }
        }
    }
}
