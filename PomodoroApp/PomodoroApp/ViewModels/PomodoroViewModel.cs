using Newtonsoft.Json;
using PomodoroApp.Models;
using PomodoroApp.Utilities;
using PomodoroApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace PomodoroApp.ViewModels
{
    [QueryProperty(nameof(PomodoroId), nameof(PomodoroId))]
    public class PomodoroViewModel : BaseViewModel
    {
        #region Properties
        private string pomodoroId;
        private string name;
        private int count;
        private Timer timer;
        private Timer timerBreak;
        private int pomodoroDuration;
        private int breakDuration;

        //Propiedad que indica cuanto ha avanzado el tiempo
        private TimeSpan ellapsed;

        public TimeSpan Ellapsed
        {
            get { return ellapsed; }
            set { ellapsed = value; OnPropertyChanged(); }
        }

        private TimeSpan ellapsedBreak;

        public TimeSpan EllapsedBreak
        {
            get { return ellapsedBreak; }
            set { ellapsedBreak = value; OnPropertyChanged(); }
        }

        //Propiedad que indica si se esta ejecutando el timer o no

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; OnPropertyChanged(); }
        }

        private bool isBreak;

        public bool IsBreak
        {
            get { return isBreak; }
            set { isBreak = value; OnPropertyChanged(); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; OnPropertyChanged(); }
        }

        private string textButton;

        public string TextButton
        {
            get { return textButton; }
            set { textButton = value; OnPropertyChanged(); }
        }

        private int duration;

        public int Duration
        {
            get { return duration; }
            set { duration = value; OnPropertyChanged(); }
        }

        private int durationBreak;

        public int DurationBreak
        {
            get { return durationBreak; }
            set { durationBreak = value; OnPropertyChanged(); }
        }

        public string PomodoroId
        {
            get
            {
                return pomodoroId;
            }
            set
            {
                pomodoroId = value;
                LoadPomodoroId(value);
            }
        }

        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private string error;
        public string Error
        {
            get { return error; }
            set { error = value; OnPropertyChanged(); }
        }
        #endregion

        #region CommandProperties
        public ICommand StartOrPauseCommand { get; set; }
        public ICommand FinishCommand { get; set; }
        #endregion

        public PomodoroViewModel()
        {
            //Title = "Timer";
            TextButton = "Iniciar";
            IsVisible = true;
            InitializeTimer();            
            //LoadSettingValues();
            StartOrPauseCommand = new Command(StarOrPauseCommandExecute);
            FinishCommand = new Command(FinishCommandExecute);
        }

        private void FinishCommandExecute(object obj)
        {
            Title = "";
            Duration = 0;
            DurationBreak = 0;
            //await Shell.Current.GoToAsync($"Timer");
        }

        private void LoadSettingValues()
        {
            if (Application.Current.Properties.ContainsKey(Literals.PomodoroDuration))
            {
                pomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDuration];
                Duration = pomodoroDuration * 60;
            }
            if (Application.Current.Properties.ContainsKey(Literals.BreakDuration))
            {
                breakDuration = (int)Application.Current.Properties[Literals.BreakDuration];
                DurationBreak = breakDuration * 60;
            }   
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

            timerBreak = new Timer();
            timerBreak.Interval = 1000;
            timerBreak.Elapsed += TimerBreak_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Agrega 1 segundo y actualizar la propiedad para que vaya incrementando
            Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));
            if (IsRunning && Ellapsed.TotalSeconds == pomodoroDuration * 60)
            {
                IsRunning = false;
                IsBreak = true;
                IsVisible = false;
                Ellapsed = TimeSpan.Zero;
                timerBreak.Start();
                timer.Stop();
            }            
        }

        private void TimerBreak_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Agrega 1 segundo y actualizar la propiedad para que vaya incrementando
            EllapsedBreak = EllapsedBreak.Add(TimeSpan.FromSeconds(1));
            if (IsBreak && EllapsedBreak.TotalSeconds == breakDuration * 60)
            {
                IsRunning = true;
                IsBreak = false;
                IsVisible = true;
                EllapsedBreak = TimeSpan.Zero;
                timer.Start();
                timerBreak.Stop();
            }
        }

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;
        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
        }

        private async void StarOrPauseCommandExecute()
        {
            if (Title != "" )
            {                

                if (IsRunning)
                {
                    StopTimer();
                    TextButton = "Iniciar";
                }
                else
                {
                    StartTimer();
                    TextButton = "Pausar";
                }
            }
            else
            {
                var result = await Application.Current.MainPage.DisplayAlert("Error", "Seleccione un Pomodoro primero", "Ok", "Omitir");
                if (result)
                {
                    await Shell.Current.GoToAsync($"Pomodoros");
                }
            }
        }

        public async void LoadPomodoroId(string pomodroId)
        {
            try
            {                
                var pomodoros = JsonConvert.DeserializeObject<List<Pomodoro>>(Application.Current.Properties[Literals.Pomodoros].ToString());
                foreach (var item in pomodoros)
                {
                    if (item.Id == pomodoroId)
                    {
                        Title = item.Name;
                        Count = item.Count;
                        pomodoroDuration = item.Duration;
                        breakDuration = item.BreakDuration;
                        Duration = item.Duration * 60;
                        DurationBreak = item.BreakDuration * 60;
                        break;
                    }                    
                }
            }
            catch (Exception)
            {
                IsError = true;
                Error = "Cantidad de pomodros debe se mayor a 0";
                await Task.Delay(1500);
                IsError = false;
            }
        }
    }
}
