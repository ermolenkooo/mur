using kinocat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using kinocat.Services;
using System.Linq;
using kinocat.Views;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace kinocat.ViewModels
{
    public class LettersOfFilmViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        public INavigation Navigation { get; set; }

        Serial selectedFilm;
        Letter selectedLetter;
        ObservableCollection<Letter> letters;
        User authoUser;
        UsersService usersService = new UsersService();
        MarksService marksService = new MarksService();
        LettersService lettersService = new LettersService();

        public User AuthoUser
        {
            get { return authoUser; }
            set
            {
                if (authoUser != value)
                {
                    authoUser = value;
                    OnPropertyChanged("AuthoUser");
                }
            }
        }

        public ObservableCollection<Letter> Letters
        {
            get { return letters; }
            set
            {
                if (letters != value)
                {
                    letters = value;
                    OnPropertyChanged("Letters");
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

        public Letter SelectedLetter
        {
            get { return selectedLetter; }
            set
            {
                if (selectedLetter != value)
                {
                    Letter tempf = value;
                    selectedLetter = null;
                    OnPropertyChanged("SelectedLetter");
                    Navigation.PushAsync(new LetterPage(AuthoUser, tempf));
                }
            }
        }

        public LettersOfFilmViewModel(Serial f, User u)
        {
            AuthoUser = u;
            SelectedFilm = f;

            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);

            letters = new ObservableCollection<Letter>();

            GetData();
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(AuthoUser, AuthoUser));
        }

        private void OnSearchClicked(object obj)
        {
            Navigation.PushAsync(new SearchPage(AuthoUser));
        }

        private async void GetData()
        {
            var marks = await marksService.GetMarksOfFilm(SelectedFilm.Id);
            var allletters = await lettersService.GetLettersOfFilm(SelectedFilm.Id);

            foreach (var l in allletters)
            {
                l.Film = SelectedFilm;
                l.Film.Mark = marks.Where(x => x.Id_film == SelectedFilm.Id).First().Mark;
                var u = await usersService.GetID(l.Id_user);
                u.Photo = u.Photo.Replace("data:image/jpeg;base64,", "");
                byte[] Base64Stream = Convert.FromBase64String(u.Photo);
                u.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                l.User = u;
                Letters.Add(l);
            }  
        }
    }
}
