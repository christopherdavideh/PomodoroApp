﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PomodoroApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPomodoroPage : ContentPage
    {
        public NewPomodoroPage()
        {
            InitializeComponent();
        }
    }
}