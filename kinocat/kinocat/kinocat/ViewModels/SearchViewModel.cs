using kinocat.Models;
using kinocat.Services;
using kinocat.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace kinocat.ViewModels
{
    class SearchViewModel : BaseViewModel
    {
        public Command FilmCommand { get; }
        public Command SerialCommand { get; }
        public Command UserCommand { get; }
        public Command MyProfilCommand { get; }
        public Command ClickCommand { get; }

        string search = "";
        string type = "Фильмы";

        string selectedGenre;
        string selectedCountry;
        ObservableCollection<string> countries;
        ObservableCollection<string> genres;
        ObservableCollection<Country> allCountries;
        ObservableCollection<Genre> allGenres;

        Serial selectedFilm = new Serial();
        ObservableCollection<Serial> films;
        ObservableCollection<Serial> serials;
        ObservableCollection<Serial> films1;
        ObservableCollection<Serial> films2;

        ObservableCollection<User> users;
        ObservableCollection<User> allUsers;
        User selectedUser = new User();
        User user;

        FilmsService filmsService = new FilmsService();
        SerialsService serialsService = new SerialsService();
        UsersService usersService = new UsersService();
        CountriesService countriesService = new CountriesService();
        GenresService genresService = new GenresService();
        FollowersService followersService = new FollowersService();

        public INavigation Navigation { get; set; }

        public SearchViewModel(User u)
        {
            GetData();

            countries = new ObservableCollection<string>();
            genres = new ObservableCollection<string>();
            allCountries = new ObservableCollection<Country>();
            allGenres = new ObservableCollection<Genre>();

            selectedFilm = new Serial();
            films = new ObservableCollection<Serial>();
            serials = new ObservableCollection<Serial>();
            films1 = new ObservableCollection<Serial>();
            films2 = new ObservableCollection<Serial>();

            users = new ObservableCollection<User>();
            allUsers = new ObservableCollection<User>();

            User = u;
            FilmCommand = new Command(OnFilmClicked);
            SerialCommand = new Command(OnSerialClicked);
            UserCommand = new Command(OnUserClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
            ClickCommand = new Command(OnButtonClicked);
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(User, User));
        }

        private void OnFilmClicked(object obj)
        {
            Type = "Фильмы";
            Films1.Clear();
            Films2.Clear();
            Searching();
        }

        private void OnSerialClicked(object obj)
        {
            Type = "Сериалы";
            Films1.Clear();
            Films2.Clear();
            Searching();
        }

        private void OnUserClicked(object obj)
        {
            Type = "Пользователи";
            Films1.Clear();
            Films2.Clear();
            Users.Clear();
            Searching();
        }

        public async void GetData()
        {
            var allFilms = await filmsService.Get();
            var allSerials = await serialsService.Get();
            var allUsers = await usersService.Get();
            var allCountries = await countriesService.Get();
            var allGenres = await genresService.Get();
            var followings = await followersService.GetFollowings(User.Id);

            countries.Add("Все страны");
            genres.Add("Все жанры");

            foreach (var f in allFilms)
            {
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
                    Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream))
                });
            }

            foreach (var s in allSerials)
            {
                s.Poster = s.Poster.Replace("data:image/jpeg;base64,", "");
                byte[] Base64Stream = Convert.FromBase64String(s.Poster);
                s.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                serials.Add(s);
            }

            foreach (var u in allUsers)
            {
                u.Photo = u.Photo.Replace("data:image/jpeg;base64,", "");
                byte[] Base64Stream = Convert.FromBase64String(u.Photo);
                u.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                if (followings.Where(x => x.Id_follower == User.Id && x.Id_following == u.Id).Any())
                    u.ReadMe = true;
                this.allUsers.Add(u);
            }

            foreach (var c in allCountries)
            {
                countries.Add(c.Name);
                this.allCountries.Add(c);
            }

            foreach (var g in allGenres)
            {
                genres.Add(g.Name);
                this.allGenres.Add(g);
            }

            selectedCountry = Countries[0];
            selectedGenre = Genres[0];
        }

        private async void OnButtonClicked(object obj) //клик на кнопку отписаться/подписаться
        {
            foreach (var u in Users)
            {
                if (u.Id == (int)obj)
                {
                    if (u.ReadMe)
                        await followersService.Delete(User.Id, u.Id);
                    else
                        await followersService.Add(new Following { Id_follower = User.Id, Id_following = u.Id });
                    u.ReadMe = !u.ReadMe;
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        public ObservableCollection<Serial> Films1
        {
            get { return films1; }
            set
            {
                films1 = value;
                OnPropertyChanged("Films1");
            }
        }

        public ObservableCollection<Serial> Films2
        {
            get { return films2; }
            set
            {
                films2 = value;
                OnPropertyChanged("Films2");
            }
        }

        public ObservableCollection<string> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }

        public ObservableCollection<string> Genres
        {
            get { return genres; }
            set
            {
                genres = value;
                OnPropertyChanged("Genres");
            }
        }

        public string Search
        {
            get { return search; }
            set
            {
                if (search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                    Searching();
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
                    OnPropertyChanged("Type");
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
                    Serial tmpfilm = value;
                    selectedFilm = null;
                    OnPropertyChanged("SelectedFilm");
                    Navigation.PushAsync(new FilmPage(User, tmpfilm));
                }
            }
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    User tmp = value;
                    selectedUser = null;
                    OnPropertyChanged("SelectedUser");
                    Navigation.PushAsync(new ProfilPage(tmp, User));
                }
            }
        }

        public string SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (selectedGenre != value)
                {
                    selectedGenre = value;
                    OnPropertyChanged("SelectedGenre");
                    Searching();
                }
            }
        }

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                    Searching();
                }
            }
        }

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

        private void Searching()
        {
            if (type == "Фильмы")
            {
                List<Serial> searchfilms = new List<Serial>();

                ObservableCollection<Serial> f1 = new ObservableCollection<Serial>();
                ObservableCollection<Serial> f2 = new ObservableCollection<Serial>();

                var c = allCountries.Where(x => x.Name == SelectedCountry).FirstOrDefault();
                var g = allGenres.Where(x => x.Name == SelectedGenre).FirstOrDefault();

                if (search != "")
                {
                    foreach (var f in films)
                        if (f.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase))
                            searchfilms.Add(f);
                }
                else
                {
                    foreach (var f in films)
                        searchfilms.Add(f);
                }

                if (c != null)
                    searchfilms = searchfilms.Where(x => x.Id_country == c.Id).ToList();

                if (g != null)
                    searchfilms = searchfilms.Where(x => x.Id_genre == g.Id).ToList();

                for (int i = 0; i < searchfilms.Count; i++)
                    if (i % 2 == 0)
                        f1.Add(searchfilms[i]);
                    else
                        f2.Add(searchfilms[i]);

                Films1 = f1;
                Films2 = f2;
            }
            else
            if (Type == "Сериалы")
            {
                List<Serial> searchfilms = new List<Serial>();

                ObservableCollection<Serial> f1 = new ObservableCollection<Serial>();
                ObservableCollection<Serial> f2 = new ObservableCollection<Serial>();

                var c = allCountries.Where(x => x.Name == SelectedCountry).FirstOrDefault();
                var g = allGenres.Where(x => x.Name == SelectedGenre).FirstOrDefault();

                if (search != "")
                {
                    foreach (var f in serials)
                        if (f.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase))
                            searchfilms.Add(f);
                }
                else
                {
                    foreach (var f in serials)
                        searchfilms.Add(f);
                }

                if (c != null)
                    searchfilms = searchfilms.Where(x => x.Id_country == c.Id).ToList();

                if (g != null)
                    searchfilms = searchfilms.Where(x => x.Id_genre == g.Id).ToList();

                for (int i = 0; i < searchfilms.Count; i++)
                    if (i % 2 == 0)
                        f1.Add(searchfilms[i]);
                    else
                        f2.Add(searchfilms[i]);

                Films1 = f1;
                Films2 = f2;
            }
            else
            {
                Users.Clear();
                foreach (var u in allUsers)
                    if (Search == "" || u.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase))
                        if (u.Id != User.Id)
                            Users.Add(u);
            }
        }
    }
}
