using kinocat.Models;
using kinocat.Services;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace kinocat.ViewModels
{
    class LetterViewModel : BaseViewModel
    {
        public Command UserCommand { get; }
        public Command FilmCommand { get; }

        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        private Letter letter;

        public User User { get; set; }

        public INavigation Navigation { get; set; }

        public LetterViewModel(User u, Letter l)
        {
            Letter = l;
            User = u;
            UserCommand = new Command(UserClicked);
            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
            FilmCommand = new Command(FilmClicked);
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(User, User));
        }

        private void OnSearchClicked(object obj)
        {
            Navigation.PushAsync(new SearchPage(User));
        }

        public Letter Letter
        {
            get { return letter; }
            set
            {
                if (letter != value)
                {
                    letter = value;
                    OnPropertyChanged("Letter");
                }
            }
        }

        private void UserClicked(object obj) //переходим к пользователю
        {
            Navigation.PushAsync(new ProfilPage(Letter.User, User));
        }

        private void FilmClicked(object obj) //переходим к фильму
        {
            Navigation.PushAsync(new FilmPage(User, Letter.Film));
        }
    }
}
