using PomodoroApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PomodoroApp.ViewModels
{
    public class AppViewModel : BaseViewModel
    {


        #region Commands

        public Command LogoutCommand { get; set; } 

        #endregion
        public AppViewModel()
        {

            LogoutCommand = new Command(LogoutCommandExecute, LogoutComandCanExecute);

        }

        private async void LogoutCommandExecute()
        {
            await Shell.Current.GoToAsync("//LoginPage");
            LogoutCommand.ChangeCanExecute();
        }

        private bool LogoutComandCanExecute()
        {
            return true;

        }
    }
}
