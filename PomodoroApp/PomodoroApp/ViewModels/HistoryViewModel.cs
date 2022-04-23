using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using PomodoroApp.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PomodoroApp.Utilities;
using PomodoroApp.Views;

namespace PomodoroApp.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        #region Properties
        //private ObservableCollection<Pomodoro> pomodoros;

        public ObservableCollection<Pomodoro> Pomodoros { get; }
        /*{
            get { return pomodoros; }
            set { pomodoros = value; OnPropertyChanged(); }
        }*/

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; OnPropertyChanged(); }
        }

        private Pomodoro _selectedPomodoro;
        public Pomodoro SelectedPomodoro
        {
            get => _selectedPomodoro;
            set
            {
                SetProperty(ref _selectedPomodoro, value);
                OnPomodroSelected(value);
            }
        }
        #endregion

        #region Command Properties
        public ICommand AddPomodoroCommand { get; }
        public Command LoadPomodoroCommand { get; }
        public Command<Pomodoro> PomodoroTapped { get; }
        #endregion
        public HistoryViewModel()
        {
            Title = "Pomodoros";
            Pomodoros = new ObservableCollection<Pomodoro>();
            LoadPomodoros();
            LoadPomodoroCommand = new Command(async () => await ExecuteLoadPomodorosCommand());
            AddPomodoroCommand = new Command(OnAddPomodoro);
            PomodoroTapped = new Command<Pomodoro>(OnPomodroSelected);
        }

        private void LoadPomodoros()
        {
            var pomodoros = JsonConvert.DeserializeObject<List<Pomodoro>>(Application.Current.Properties[Literals.Pomodoros].ToString());
            foreach (var item in pomodoros)
            {
                Pomodoros.Add(item);
            }
        }
        async Task ExecuteLoadPomodorosCommand()
        {
            IsBusy = true;

            try
            {
                Pomodoros.Clear();
                var pomodoros = JsonConvert.DeserializeObject<List<Pomodoro>>(Application.Current.Properties[Literals.Pomodoros].ToString());
                foreach (var item in pomodoros)
                {
                    Pomodoros.Add(item);
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Error = ex.Message;
                await Task.Delay(1500);
                IsError = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPomodoro = null;
        }

        private async void OnAddPomodoro(object obj)
        {
            await Shell.Current.GoToAsync("NuevoPomodoro");
        }

        async void OnPomodroSelected(Pomodoro pomodro)
        {
            if (pomodro == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(PomodoroPage)}?{nameof(PomodoroViewModel.PomodoroId)}={pomodro.Id}");
        }
    }
}
