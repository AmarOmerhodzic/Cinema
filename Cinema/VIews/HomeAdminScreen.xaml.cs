using Microsoft.Maui.Controls;
using Cinema.ViewModels;
using Cinema.Data;
using Cinema.Models;

namespace Cinema.Views
{
    public partial class HomeAdminScreen : ContentPage
    {
        public HomeAdminScreen()
        {
            InitializeComponent();
            BindingContext = new ModelDodavanjeFilma(); // Set the BindingContext to an instance of NewPageViewModel
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
                    // You may want to update UI or perform any other action
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



    }
}