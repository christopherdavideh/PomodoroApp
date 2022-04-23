using PomodoroApp.Utilities;
using PomodoroApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PomodoroApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            //OnLoginClicked();
        }

        //private async void OnLoginClicked(object obj)
        private async void OnLoginClicked()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            if (Application.Current.Properties.ContainsKey(Literals.Pomodoros))
            {
                IsBusy = true;
                await Task.Delay(2000);
                await Shell.Current.GoToAsync($"//{nameof(HistoryPage)}");
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                await Task.Delay(2000);
                await Shell.Current.GoToAsync($"NuevoPomodoro");
                IsBusy = false;
            }
            
            
        }
    }
}
