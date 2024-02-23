using Microsoft.Maui.Controls;
using Cinema.Data;
using Cinema.Models;
using Cinema.VIews;
using Cinema.VIews.auth;

namespace Cinema.Views
{
    public partial class HomeAdminScreen : ContentPage
    {
        public HomeAdminScreen()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateListView();
        }

        private async Task UpdateListView()
        {
            FilmDatabase database = await FilmDatabase.Instance;
            //await database.UnosTest();
            listView.ItemsSource = await database.SviFilmovi();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                // Get the button that was clicked
                Button button = (Button)sender;

                // Get the command parameter which should be the ID of the film
                int id = (int)button.CommandParameter;

                // Instantiate an object of FilmDatabase class
                FilmDatabase filmDatabase = new FilmDatabase(); // Instantiate the FilmDatabase class

                // Call the DeleteFilm method
                bool deletionResult = await filmDatabase.DeleteFilm(id);

                // Handle the deletion result
                if (deletionResult)
                {
                    // Deletion successful
                    await DisplayAlert("Success", "Film successfully deleted.", "OK");
                    await UpdateListView();
                }
                else
                {
                    // Deletion failed
                    await DisplayAlert("Error", "Failed to delete film.", "OK");
                    // You may want to show an error message or perform any other action
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error occurred while deleting the film.", "OK");
                // Handle exception
            }
        }
        private void LougoutRoute(object sender, EventArgs e)
        {
            Preferences.Clear();
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        private async void ClearSessionData()
        {

            Preferences.Clear(); 
            await Navigation.PopToRootAsync();
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QrCodeAdmin());
        }
        private async void Projekcija(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DodavanjeProjekcije());
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var film = button?.BindingContext as Film;

            if (film != null)
            {
                await Navigation.PushAsync(new EditPage(film));
            }
        }
    }
}