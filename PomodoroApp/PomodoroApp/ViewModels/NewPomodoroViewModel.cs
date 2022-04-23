using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PomodoroApp.Utilities;
using System.Text;
using Newtonsoft.Json;
using PomodoroApp.Models;

namespace PomodoroApp.ViewModels
{
    public class NewPomodoroViewModel : BaseViewModel
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

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }

        #endregion

        #region CommandProperties
        public ICommand SaveCommand { get; set; }
        #endregion

        public NewPomodoroViewModel()
        {
            Title = "Nuevo Pomodoro";
            LoadPomodoroDurations();
            LoadBreakDurations();
            //LoadConfiguration();

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
            if (Application.Current.Properties.ContainsKey(Literals.Pomodoros))
            {
                List<Pomodoro> pomodoroList = new List<Pomodoro>();
                var pomodoros = JsonConvert.DeserializeObject<List<Pomodoro>>(Application.Current.Properties[Literals.Pomodoros].ToString());
                foreach (var item in pomodoros)
                {
                    pomodoroList.Add(item);
                }
            }
        }

        private async void SaveCommandExecute()
        {

            //Guardamos en el Diccinario de Prooiedades de la Aplicacion
            if (SelectedPomodoroDurations != 0 && SelectedBreakDurations != 0 && (Name != null || Name != "") && Convert.ToInt32(Count) > 0)
            {
                IsBusy = true;
                List<Pomodoro> pomodoroList = new List<Pomodoro>();
                if (Application.Current.Properties.ContainsKey(Literals.Pomodoros))
                {
                    //List<Pomodoro> pomodoroList = new List<Pomodoro>();
                    var pomodoros = JsonConvert.DeserializeObject<List<Pomodoro>>(Application.Current.Properties[Literals.Pomodoros].ToString());
                    foreach (var item in pomodoros)
                    {
                        pomodoroList.Add(item);
                    }
                }
                Pomodoro pomodoroData = new Pomodoro()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Count = Count,
                    Duration = SelectedPomodoroDurations,
                    BreakDuration = SelectedBreakDurations,
                    CreateAt = DateTime.Now,
                    LastUsed = DateTime.Now,
                };
                pomodoroList.Add(pomodoroData);

                Application.Current.Properties[Literals.Pomodoros] = JsonConvert.SerializeObject(pomodoroList);
                //Para Guardar de manera proactiva en el dispositivo
                await Application.Current.SavePropertiesAsync();
                await Task.Delay(1000);
                await Shell.Current.GoToAsync($"Pomodoros");
                IsBusy = false;
            }
            else if (Name == null || Name == "")
            {
                IsError = true;
                Error = "Nombre no puede estar vacio";
                await Task.Delay(1500);
                IsError = false;
            }
            else if (Convert.ToInt32(Count) <= 0)
            {
                IsError = true;
                Error = "Cantidad de pomodros debe se mayor a 0";
                await Task.Delay(1500);
                IsError = false;
            }
            else if (SelectedPomodoroDurations == 0 || SelectedBreakDurations == 0)
            {
                IsError = true;
                Error = "Seleccione el tiempo de duracion o pausa";
                await Task.Delay(1500);
                IsError = false;
            }
        }
    }
}
