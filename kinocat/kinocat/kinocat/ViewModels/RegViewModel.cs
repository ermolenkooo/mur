using kinocat.Models;
using kinocat.Services;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace kinocat.ViewModels
{
    public class RegViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegCommand { get; }
        public Command FileCommand { get; }

        private string username;
        private string email;
        private string password;
        private string file;
        private string warning;
        private string base64;

        public INavigation Navigation { get; set; }
        UsersService usersService = new UsersService();

        public RegViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegCommand = new Command(OnRegClicked);
            FileCommand = new Command(OnFileClicked);
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
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

        public string File
        {
            get { return file; }
            set
            {
                if (file != value)
                {
                    file = value;
                    OnPropertyChanged("File");
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
            Navigation.PushAsync(new LoginPage());
        }

        private async void OnRegClicked(object obj)
        {
            var users = await usersService.Get();
            foreach (var u in users)
            {
                if (Email == null || Username == null || Password == null)
                {
                    Warning = "Заполните все поля!";
                    return;
                }
                else if (Email == u.Email)
                {
                    Warning = "На данный e-mail уже зарегистрирован аккаунт!";
                    return;
                }
                else if (Username == u.Name)
                {
                    Warning = "Данное имя пользователя уже используется!";
                    return;
                }
            }
            User newuser = new User { Email = Email, Password = Password, Name = Username, Photo = base64 };
            //Navigation.PushAsync(new ProfilPage(user));
        }

        private async void OnFileClicked(object obj)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Выберите фото профиля!"
            });

            File = result.FileName;
            string base64ImageRepresentation = "data:image/jpeg;base64,";
            byte[] imageArray = System.IO.File.ReadAllBytes(result.FullPath);
            base64ImageRepresentation += Convert.ToBase64String(imageArray);
            base64 = base64ImageRepresentation;
        }
    }
}
