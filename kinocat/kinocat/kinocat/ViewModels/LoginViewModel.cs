using kinocat.Models;
using kinocat.Services;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kinocat.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegCommand { get; }

        private string email;
        private string password;
        private string warning;

        public INavigation Navigation { get; set; }
        UsersService usersService = new UsersService();

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegCommand = new Command(OnRegClicked);
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Warning
        {
            get { return warning; }
            set
            {
                if (warning != value)
                {
                    warning = value;
                    OnPropertyChanged("Warning");
                }
            }
        }

        private async void OnLoginClicked(object obj)
        {
            //DependencyService.Get<IAudio>().PlayAudioFile();
            User u = new User { Email = Email, Password = Password };
            var user = await usersService.Login(u);
            if (user == null)
                Warning = "Неверный логин и/или пароль!";
            else
            {
                await Navigation.PushAsync(new ProfilPage(user, user));
            }
        }

        private void OnRegClicked(object obj)
        {
            Navigation.PushAsync(new RegPage());
        }
    }
}
