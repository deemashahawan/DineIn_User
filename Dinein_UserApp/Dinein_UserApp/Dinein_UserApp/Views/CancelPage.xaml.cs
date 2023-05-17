﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_UserApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelPage : ContentPage
    {
        public CancelPage()
        {
            InitializeComponent();
        }

        private void Button_Reserve(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReservationPage());
        }
    }
}