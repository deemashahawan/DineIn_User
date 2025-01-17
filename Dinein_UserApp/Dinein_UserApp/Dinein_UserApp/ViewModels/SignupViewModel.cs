﻿using Dinein_UserApp.Models;
using Dinein_UserApp.Services;
using Dinein_UserApp.Views;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dinein_UserApp.ViewModels
{
    class SignupViewModel : INotifyPropertyChanged
    {
        private readonly UserModel _userModel;

        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private bool _isBusy;
        private Users _users;
        private DataBase _dataBase;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public SignupViewModel()
        {
            _userModel = new UserModel();
            _users = new Users();
            _dataBase= new DataBase();
        }

        public Command RegisterCommand => new Command(async () => await RegisterUserAsync());

        private async Task RegisterUserAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Please fill name field!", "Ok");
                    return;
                }


                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Please fill Email field!", "Ok");
                    return;
                }

                if (Password.Length < 6)
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "password should be at least 6 digits", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Please fill password field!", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Please fill confirm Password field!", "Ok");
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "Password Not Match", "Ok");
                    return;
                }

                IsBusy = true;
                bool isSaved = await _userModel.RegisterUser(Email, Name, Password);

                if (isSaved)
                {

                    _users.Id = (string)Application.Current.Properties["UID"];
                    _users.Email = Email;
                    _users.Password = Password;
                    _users.UserName = Name;
                    await _dataBase.UserSave(_users);
                    await Application.Current.MainPage.DisplayAlert("Register User", "Registration completed", "Ok");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Register User", "Registration failed", "Ok");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await Application.Current.MainPage.DisplayAlert("warning", "email exist", "ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
