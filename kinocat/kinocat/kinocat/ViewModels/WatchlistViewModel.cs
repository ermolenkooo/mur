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
using static System.Net.WebRequestMethods;

namespace kinocat.ViewModels
{
    public class WatchlistViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        public INavigation Navigation { get; set; }

        Serial selectedFilm;
        ObservableCollection<Serial> films1;
        ObservableCollection<Serial> films2;
        User user, authoUser;
        string type;
        WatchlistsService watchlistsService = new WatchlistsService();
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

        public ObservableCollection<Serial> Films1
        {
            get { return films1; }
            set
            {
                if (films1 != value)
                {
                    films1 = value;
                    OnPropertyChanged("Films1");
                }
            }
        }

        public ObservableCollection<Serial> Films2
        {
            get { return films2; }
            set
            {
                if (films2 != value)
                {
                    films2 = value;
                    OnPropertyChanged("Films2");
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
                    Serial tempf = value;
                    selectedFilm = null;
                    OnPropertyChanged("SelectedFilm");
                        //Navigation.PushAsync(new FilmPage(AuthoUser, film));
                }
            }
        }

        public WatchlistViewModel(User u, User authorizeUser, string type)
        {
            Type = type;
            AuthoUser = authorizeUser;
            User = u;

            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);

            selectedFilm = new Serial();
            films1 = new ObservableCollection<Serial>();
            films2 = new ObservableCollection<Serial>();

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
            List<Serial> films = new List<Serial>();
            var watchlist = await watchlistsService.Get(User.Id);
            if (Type == "Фильмы")
            {
                foreach (var w in watchlist)
                {
                    if (await serialsService.GetID(w.Id_film) == null)
                    {
                        Film f = await filmsService.GetID(w.Id_film);
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
                            Mark = f.Mark,
                            Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream))
                        });
                    }
                }
            }
            else
            {
                foreach (var w in watchlist)
                {
                    if (await serialsService.GetID(w.Id_film) != null)
                    {
                        Serial s = await serialsService.GetID(w.Id_film);
                        s.Poster =  s.Poster.Replace("data:image/jpeg;base64,", "");
                        byte[] Base64Stream = Convert.FromBase64String(s.Poster);
                        s.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                        films.Add(s);
                    }
                }
            }

            for (int i = 0; i < films.Count; i++)
                if (i % 2 == 0)
                    films1.Add(films[i]);
                else
                    films2.Add(films[i]);
        }

    }
}
