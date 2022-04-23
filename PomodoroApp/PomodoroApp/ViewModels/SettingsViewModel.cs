using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PomodoroApp.Utilities;

namespace PomodoroApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<int> pomodoroDurations;

        public ObservableCollection<int> PomodoroDurations
        {
            get { return pomodoroDurations; }
            set { pomodoroDurations = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> breakDurations;

        public ObservableCollection<int> BreakDurations
        {
            get { return breakDurations; }
            set { breakDurations = value; OnPropertyChanged(); }
        }

        //Propiedades para rastrear la opcion deseada por el usuario
        private int selectedPomodoroDurations;

        public int SelectedPomodoroDurations
        {
            get { return selectedPomodoroDurations; }
            set { selectedPomodoroDurations = value; OnPropertyChanged(); }
        }

        private int selectedBreakDurations;

        public int SelectedBreakDurations
        {
            get { return selectedBreakDurations; }
            set { selectedBreakDurations = value; OnPropertyChanged(); }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; OnPropertyChanged(); }
        }

        #endregion

        #region CommandProperties
        public ICommand SaveCommand { get; set; }
        #endregion

        public SettingsViewModel()
        {
            Title = "Ajustes";
            LoadPomodoroDurations();
            LoadBreakDurations();
            LoadConfiguration();
            
            SaveCommand = new Command(SaveCommandExecute);
        }

        private void LoadPomodoroDurations()
        {
            PomodoroDurations = new ObservableCollection<int>();
            PomodoroDurations.Add(1);
            PomodoroDurations.Add(2);
            PomodoroDurations.Add(5);
            PomodoroDurations.Add(10);
            PomodoroDurations.Add(15);
            PomodoroDurations.Add(20);
            PomodoroDurations.Add(25);
        }

        private void LoadBreakDurations()
        {
            BreakDurations = new ObservableCollection<int>();
            BreakDurations.Add(1);
            BreakDurations.Add(2);
            BreakDurations.Add(5);
            BreakDurations.Add(10);
            BreakDurations.Add(15);
            BreakDurations.Add(20);
            BreakDurations.Add(25);
        }
                

        private void LoadConfiguration()
        {
            /*if (Application.Current.Properties.ContainsKey(Literals.PomodoroDuration))
            {
                SelectedPomodoroDurations = (int)Application.Current.Properties[Literals.PomodoroDuration];
            }

            if (Application.Current.Properties.ContainsKey(Literals.BreakDuration))
            {
                SelectedBreakDurations = (int)Application.Current.Properties[Literals.BreakDuration];
            }*/
        }

        private async void SaveCommandExecute()
        {
            
            //Guardamos en el Diccinario de Prooiedades de la Aplicacion
            /*if (SelectedPomodoroDurations != 0 && SelectedBreakDurations != 0)
            {
                IsBusy = true;
                Application.Current.Properties[Literals.PomodoroDuration] = SelectedPomodoroDurations;
                Application.Current.Properties[Literals.BreakDuration] = SelectedBreakDurations;
                //Para Guardar de manera proactiva en el dispositivo
                await Application.Current.SavePropertiesAsync();
                await Task.Delay(1000);
                await Shell.Current.GoToAsync($"//PomodoroPage");
                IsBusy = false;
            }
            else
            {
                IsError = true;
                Error = "Seleccione el tiempo de duracion o pausa";
                await Task.Delay(1500);
                IsError = false;
            }   */      
        }
    }
}
