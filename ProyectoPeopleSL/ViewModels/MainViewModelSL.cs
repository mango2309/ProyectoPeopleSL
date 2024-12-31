using ProyectoPeopleSL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoPeopleSL.ViewModels
{
    public class MainViewModelSL : INotifyPropertyChanged
    {
        private string _newPersonName;
        private string _statusMessage;

        public string NewPersonName
        {
            get => _newPersonName;
            set { _newPersonName = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PersonSL> People { get; set; }
        public ICommand AddPersonCommand { get; }
        public ICommand GetPeopleCommand { get; }
        public ICommand DeletePersonCommand { get; }

        public MainViewModelSL()
        {
            People = new ObservableCollection<PersonSL>();
            AddPersonCommand = new Command(AddPerson);
            GetPeopleCommand = new Command(GetPeople);
            DeletePersonCommand = new Command<int>(DeletePerson);
        }

        private void AddPerson()
        {
            if (!string.IsNullOrEmpty(NewPersonName))
            {
                App.PersonRepo.AddNewPerson(NewPersonName);
                StatusMessage = App.PersonRepo.StatusMessage;
                GetPeople();
                NewPersonName = string.Empty; 
            }
            else
            {
                StatusMessage = "Por favor, ingresa un nombre válido.";
            }
        }

        private void GetPeople()
        {
            try
            {
                People.Clear();
                var peopleList = App.PersonRepo.GetAllPeople();
                foreach (var person in peopleList)
                {
                    People.Add(person);
                }
                StatusMessage = "Personas cargadas correctamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al cargar personas: {ex.Message}";
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void DeletePerson(int id)
        {
            App.PersonRepo.DeletePerson(id);
            StatusMessage = App.PersonRepo.StatusMessage;

            // Mostrar mensaje personalizado
            if (App.PersonRepo.StatusMessage.Contains("eliminado"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Registro Eliminado",
                    "Sebastian Largo acaba de eliminar un registro.",
                    "OK");
            }

            GetPeople();
        }

        
    }
}
