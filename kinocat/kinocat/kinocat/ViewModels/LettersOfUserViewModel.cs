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
    public class LettersOfUserViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        public INavigation Navigation { get; set; }

        Serial selectedFilm;
        ObservableCollection<Serial> films;
        IEnumerable<Letter> letters;
        IEnumerable<User> users;
        User user, authoUser;
        string type;
        UsersService usersService = new UsersService();
        MarksService marksService = new MarksService();
        LettersService lettersService = new LettersService();
        FilmsService filmsService = new FilmsService();
        SerialsService serialsService = new SerialsService();

        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }

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

        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("AuthoUser");
                }
            }
        }

        public ObservableCollection<Serial> Films
        {
            get { return films; }
            set
            {
                if (films != value)
                {
                    films = value;
                    OnPropertyChanged("Films");
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
                    //тут поиск нужной рецензии
                    Serial tempf = value;
                    selectedFilm = null;
                    OnPropertyChanged("SelectedFilm");
                    Letter letter = letters.Where(x => x.Id_film == tempf.Id && x.Time.ToString("d") == tempf.Time).First();
                    var u = users.Where(x => x.Id == letter.Id_user).First();
                    u.Photo = u.Photo.Replace("data:image/jpeg;base64,", "");
                    byte[] Base64Stream = Convert.FromBase64String(u.Photo);
                    u.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                    letter.User = u;
                    letter.Film = tempf;
                    Navigation.PushAsync(new LetterPage(AuthoUser, letter));
                }
            }
        }

        public LettersOfUserViewModel(User u, User authorizeUser, string type)
        {
            Type = type;
            AuthoUser = authorizeUser;
            User = u;

            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);

            selectedFilm = new Serial();
            films = new ObservableCollection<Serial>();

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
            users = await usersService.Get();
            var marks = await marksService.GetMarksOfUser(User.Id);
            letters = await lettersService.GetLettersOfUser(User.Id);
            if (Type == "Фильмы")
            {
                foreach (var l in letters)
                {
                    if (await serialsService.GetID(l.Id_film) == null)
                    {
                        MarkOfUser m = new MarkOfUser();
                        m = marks.Where(x => x.Id_film == l.Id_film).FirstOrDefault();
                        Film f = await filmsService.GetID(l.Id_film);
                        f.Poster = f.Poster.Replace("data:image/jpeg;base64,", "");
                        byte[] Base64Stream = Convert.FromBase64String(f.Poster);
                        films.Add(new Serial
                        {
                            Id = f.Id,
                            Age = f.Age,
                            Id_country = f.Id_country,
                            Description = f.Description,
                            Id_genre = f.Id_genre,
                            Name = f.Name,
                            Original = f.Original,
                            Poster = f.Poster,
                            Timing = f.Timing,
                            Year = f.Year,
                            Mark = m.Mark,
                            Time = l.Time.ToString("d"),
                            Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream))
                        });
                    }
                }
            }
            else
            {
                foreach (var l in letters)
                {
                    if (await serialsService.GetID(l.Id_film) != null)
                    {
                        MarkOfUser m = new MarkOfUser();
                        m = marks.Where(x => x.Id_film == l.Id_film).FirstOrDefault();
                        Serial s = await serialsService.GetID(l.Id_film);
                        s.Poster = s.Poster.Replace("data:image/jpeg;base64,", "");
                        byte[] Base64Stream = Convert.FromBase64String(s.Poster);
                        s.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                        s.Mark = m.Mark;
                        s.Time = l.Time.ToString("d");
                        films.Add(s);
                    }
                }
            }
        }
    }
}
