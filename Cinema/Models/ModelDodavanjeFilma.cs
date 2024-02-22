/*using Cinema.Data;
using Cinema.Models;
using Cinema.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.ViewModels

{
    public class ModelDodavanjeFilma : BindableObject
    {

        private string _imageUrl;
        private Film _film;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        public Film Film
        {
            get { return _film; }
            set
            {
                _film = value;
                OnPropertyChanged();
            }
        }


        public ICommand SaveCommand { get; private set; }
        public ModelDodavanjeFilma()
        {
            Film = new Film();
            SaveCommand = new Command(async () => await SaveDataAsync());
        }

        public async Task SaveDataAsync()
        {
            try
            {
                // Create a new instance of FilmDatabase and ProjekcijaDatabase
                var filmDatabase = new FilmDatabase(); // Assuming this is correctly configured

                // Save the film and projection to the database
                bool filmSaved = await filmDatabase.CreateFilm(Film);

                if (filmSaved)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Data saved successfully!", "OK");

                    // Navigate to another page to display the saved data
                    await Application.Current.MainPage.Navigation.PushAsync(new HomeAdminScreen());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to save data!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
*/