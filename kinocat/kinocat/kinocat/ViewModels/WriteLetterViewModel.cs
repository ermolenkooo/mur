using kinocat.Models;
using kinocat.Services;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace kinocat.ViewModels
{
    class WriteLetterViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }
        public Command CreateCommand { get; }

        private Serial selectedFilm;
        private User selectedUser;
        private string text;
        LettersService lettersService = new LettersService();

        public INavigation Navigation { get; set; }

        public WriteLetterViewModel(Serial f, User u)
        {
            selectedUser = u;
            selectedFilm = f;
            CreateCommand = new Command(OnButtonClicked);
            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(SelectedUser, SelectedUser));
        }

        private void OnSearchClicked(object obj)
        {
            Navigation.PushAsync(new SearchPage(SelectedUser));
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }

        public Serial SelectedFilm
        {
            get { return selectedFilm; }
            set
            {
                if (selectedFilm != value)
                {
                    selectedFilm = value;
                    OnPropertyChanged("SelectedFilm");
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        private async void OnButtonClicked(object obj) //публикуем рецензию
        {
            await lettersService.Add(new Letter { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Text = Text, Time = DateTime.Now });
            await Navigation.PushAsync(new FilmPage(SelectedUser, SelectedFilm));
        }
    }
}
