namespace Cinema.VIews;
using Cinema.Data;
using Cinema.Models;

public partial class EditPage : ContentPage
{
    private Film _film;

    public EditPage(Film film)
    {
        InitializeComponent();
        SetupValidation();
        _film = film;
        BindingContext = _film;

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

        // Update the film with data from the UI
        _film.Slika = ImageUrlEntry.Text;
        _film.Naziv = NazivFilmaEntry.Text;
        _film.Opis = OpisFilmaEntry.Text;
        _film.Zanr = ZanrPicker.SelectedItem?.ToString();
        _film.Trajanje = Convert.ToInt32(TrajanjeEntry.Text);
        _film.Trailer = TrailerUrlEntry.Text;
        _film.Ocjena = Convert.ToDouble(OcjenaEntry.Text);

        if (!string.IsNullOrEmpty(OcjenaEntry.Text) && double.TryParse(OcjenaEntry.Text, out double ocjenaValue))
        {
            if (ocjenaValue >= 0 && ocjenaValue <= 5)
            {
                _film.Ocjena = ocjenaValue;
            }
            else
            {
                // Display message
                await DisplayAlert("Error", "Please enter a value between 0 and 5.", "OK");
                return;
            }
        }
        // Update the film in the database
        bool success = await filmDatabase.UpdateFilm(_film);

        if (success)
        {
            // Display a success message
            await DisplayAlert("Success", "Film updated successfully.", "OK");

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
        else
        {
            // Display an error message
            await DisplayAlert("Error", "Failed to update film.", "OK");
        }
    }
}
