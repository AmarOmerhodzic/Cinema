using Microsoft.Maui.Controls;
using Cinema.Data;
using Cinema.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace Cinema.Views
{
    public partial class NewPage1 : ContentPage, INotifyPropertyChanged
    {
        private string _imageUrl;
        private Film _film;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(ImageUrl));
                }
            }
        }

        public Film Film
        {
            get { return _film; }
            set
            {
                if (_film != value)
                {
                    _film = value;
                    OnPropertyChanged(nameof(Film));
                }
            }
        }

        public ICommand SaveCommand { get; private set; }

        public NewPage1()
        {
            InitializeComponent();
            Film = new Film();
            SaveCommand = new Command(async () => await SaveDataAsync());
            BindingContext = this;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task SaveDataAsync()
        {
            try
            {
                var filmDatabase = new FilmDatabase();

                bool filmSaved = await filmDatabase.CreateFilm(Film);

                if (filmSaved)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Data saved successfully!", "OK");

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

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await SaveDataAsync();
        }
    }
}
