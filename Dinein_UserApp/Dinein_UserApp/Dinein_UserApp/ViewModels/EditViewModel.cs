﻿using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{

    class EditViewModel : INotifyPropertyChanged
    {
        private bool _isOneTwoChecked;
        private bool _isTwoThreeChecked;
        private bool _isFourChecked;
        private bool _isFiveChecked;
        private bool _isSixChecked;
        private int _selectedValue;
        private string selectedValue;



        private int _hour = 0;
        private int _minute = 0;

        private string _time = "00:00";
        private String reservationId;

        public string Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));

                    if (Time.Length == 5)
                    {
                        if (int.TryParse(Time.Substring(0, 2), out int hour) && int.TryParse(Time.Substring(3, 2), out int minute))
                        {
                            _hour = hour;
                            _minute = minute;
                        }
                    }
                }
            }
        }

        public ICommand MinusButtonCommand { get; }
        public ICommand PlusButtonCommand { get; }
        public ICommand ConfirmButtonCommand { get; }
        public EditViewModel()
        {

            MinusButtonCommand = new Command(OnMinusButtonClicked);
            PlusButtonCommand = new Command(OnPlusButtonClicked);
        }

        private void OnMinusButtonClicked()
        {
            if (_hour > 0 || _minute >= 30)
            {
                if (_minute >= 30)
                {
                    _minute -= 30;
                }
                else
                {
                    _hour--;
                    _minute += 30;
                }
            }

            Time = $"{_hour:D2}:{_minute:D2}";
        }

        private void OnPlusButtonClicked()
        {
            if (_hour < 23 || _minute < 30)
            {
                if (_minute < 30)
                {
                    _minute += 30;
                }
                else
                {
                    _hour++;
                    _minute -= 30;
                }
            }

            Time = $"{_hour:D2}:{_minute:D2}";
        }




        public ICommand EditCommand => new Command(async () => await OnEditReservation(reservationId));

        public async Task OnEditReservation(String reservationId)
        {
            if (string.IsNullOrEmpty(Time))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please enter a Time! and Number of People", "Cancel");
            }
            else
            {
                ReservationModel reservationModel = new ReservationModel();
                reservationModel.ReservationId = reservationId; 
                reservationModel.TimePicker = Time;
                reservationModel.NumberOfPeople = selectedValue;
                reservationModel.UserId = (string)Application.Current.Properties["UID"];

                DataBase dataBase = new DataBase();
                var isUpdated = await dataBase.ReservationModelUpdate(reservationModel); 

                if (isUpdated)
                {
                    await Application.Current.MainPage.DisplayAlert("Information", $"Your Reservation Time has been updated to: {Time} with {selectedValue} People", "Ok");
                    Clear();
                }
                else

                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Your reservation update failed, please try again", "Ok");
                }
                await Application.Current.MainPage.Navigation.PushAsync(new CurrentReservationPage());

            }
        }


        public void Clear()
        {
            Time = string.Empty;
            selectedValue = string.Empty;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public bool IsOneTwoChecked
        {
            get => _isOneTwoChecked;
            set
            {
                if (_isOneTwoChecked != value)
                {
                    _isOneTwoChecked = value;
                    if (value)
                    {
                        selectedValue = "1-2";
                    }
                    OnPropertyChanged(nameof(IsOneTwoChecked));
                }
            }
        }


        public bool IsTwoThreeChecked
        {
            get => _isTwoThreeChecked;
            set
            {
                if (_isTwoThreeChecked != value)
                {
                    _isTwoThreeChecked = value;
                    if (value)
                    {
                        selectedValue = "3-4";
                    }
                    OnPropertyChanged(nameof(IsTwoThreeChecked));
                }
            }
        }

        public bool IsFourChecked
        {
            get => _isFourChecked;
            set
            {
                if (_isFourChecked != value)
                {
                    _isFourChecked = value;
                    if (value)
                    {
                        selectedValue = "5-6";
                    }
                    OnPropertyChanged(nameof(IsFourChecked));
                }
            }
        }

        public bool IsFiveChecked
        {
            get => _isFiveChecked;
            set
            {
                if (_isFiveChecked != value)
                {
                    _isFiveChecked = value;
                    if (value)
                    {
                        selectedValue = "7-10";
                    }
                    OnPropertyChanged(nameof(IsFiveChecked));
                }
            }
        }
        public bool IsSixChecked
        {
            get => _isSixChecked;
            set
            {
                if (_isSixChecked != value)
                {
                    _isSixChecked = value;
                    if (value)
                    {
                        selectedValue = "more than 10";
                    }
                    OnPropertyChanged(nameof(IsSixChecked));

                }
            }
        }



        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                }
            }
        }
       
    }
}
