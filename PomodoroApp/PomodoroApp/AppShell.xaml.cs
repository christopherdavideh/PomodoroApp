using PomodoroApp.ViewModels;
using PomodoroApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PomodoroApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute("NuevoPomodoro", typeof(NewPomodoroPage));
            Routing.RegisterRoute("Pomodoros", typeof(HistoryPage));
            Routing.RegisterRoute("Timer", typeof(PomodoroPage));
        }

        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Pomodoro", "¿Quieres salir de la aplicación?", "Si", "No"))
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;

        }
    }
}
