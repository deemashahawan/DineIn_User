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
    public partial class BillPage : ContentPage
    {
        public BillPage()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CurrentReservationPage());
        }
    }
}