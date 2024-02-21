using Microsoft.Maui.Controls;
using Cinema.ViewModels;

namespace Cinema.Views
{
    public partial class NewPage1 : ContentPage
    {
        public NewPage1()
        {
            InitializeComponent();
            BindingContext = new ModelDodavanjeFilma();
        }

        
    }
}
