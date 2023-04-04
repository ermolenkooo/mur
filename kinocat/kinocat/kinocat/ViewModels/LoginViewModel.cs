using kinocat.Models;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        private void OnLoginClicked(object obj)
        {
            /*var u = App.Database.GetUsers();
            foreach (var user in u)
            {
                if (user.Email == Email && user.Password == Password)
                {
                    User myuser = new User { Email = user.Email, Image = user.Image, Username = user.Username, Id = user.Id };
                    Navigation.PushAsync(new ProfilPage(myuser));
                    return;
                }
            }*/
            Warning = "Неверный логин и/или пароль!";
        }

        private void OnRegClicked(object obj)
        {
            Navigation.PushAsync(new RegPage());
        }
    }
}
