using ProyectoPeopleSL.Models;
using ProyectoPeopleSL.ViewModels;

namespace ProyectoPeopleSL
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModelSL();
        }

    }

}
