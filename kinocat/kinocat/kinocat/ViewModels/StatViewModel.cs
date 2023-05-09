using kinocat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using kinocat.Services;
using System.Linq;
using kinocat.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace kinocat.ViewModels
{
    public class StatViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        public INavigation Navigation { get; set; }

        User authoUser;
        public Stat films;
        public Stat serials;
        public string genre;
        public string country;

        CountriesService countriesService = new CountriesService();
        GenresService genresService = new GenresService();
        LovesService lovesService = new LovesService();
        MarksService marksService = new MarksService();
        WatchlistsService watchlistsService = new WatchlistsService();
        LettersService lettersService = new LettersService();
        FilmsService filmsService = new FilmsService();
        SerialsService serialsService = new SerialsService();

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
        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public Stat Films
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

        public Stat Serials
        {
            get { return serials; }
            set
            {
                if (serials != value)
                {
                    serials = value;
                    OnPropertyChanged("Serials");
                }
            }
        }

        public StatViewModel(User authorizeUser)
        {
            AuthoUser = authorizeUser;
            films = new Stat();
            serials = new Stat();
            GetData();

            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(AuthoUser, AuthoUser));
        }

        private async void GetData()
        {
            var allfilms = await filmsService.Get();
            var allserials = await serialsService.Get();

            var marks = await marksService.GetMarksOfUser(AuthoUser.Id);
            var loves = await lovesService.Get(AuthoUser.Id);
            var letters = await lettersService.GetLettersOfUser(AuthoUser.Id);
            var watchlist = await watchlistsService.Get(AuthoUser.Id);

            List<Serial> serialsofuser = new List<Serial>();
            List<Film> filmsofuser = new List<Film>();
            foreach (var m in marks)
            {
                if (!allserials.Where(x => x.Id == m.Id_film).Any())
                {
                    filmsofuser.Add(allfilms.Where(x => x.Id == m.Id_film).First());
                    Films.Mark += m.Mark;
                    Films.Marks++;
                }
                else
                {
                    serialsofuser.Add(allserials.Where(x => x.Id == m.Id_film).First());
                    Serials.Mark += m.Mark;
                    Serials.Marks++;
                }
            }
            if (Films.Marks != 0)
                Films.Mark /= Films.Marks;
            if (Serials.Marks != 0)
                Serials.Mark /= Serials.Marks;

            foreach (var l in loves)
            {
                if (!allserials.Where(x => x.Id == l.Id_film).Any())
                    Films.Loves++;
                else
                    Serials.Loves++;
            }

            foreach (var l in letters)
            {
                if (!allserials.Where(x => x.Id == l.Id_film).Any())
                    Films.Letters++;
                else
                    Serials.Letters++;
            }

            foreach (var w in watchlist)
            {
                if (!allserials.Where(x => x.Id == w.Id_film).Any())
                    Films.Watchlists++;
                else
                    Serials.Watchlists++;
            }

            foreach (var f in allfilms)
            {

            }

            var allgenres = await genresService.Get();
            var allcountries = await countriesService.Get();

            Dictionary<string, int> genres = new Dictionary<string, int>();
            foreach (var g in allgenres)
            {
                int count = filmsofuser.Where(x => x.Id_genre == g.Id).Count() + serialsofuser.Where(x => x.Id_genre == g.Id).Count();
                genres.Add(g.Name, count);
            }

            Dictionary<string, int> countries = new Dictionary<string, int>();
            foreach (var c in allcountries)
            {
                int count = filmsofuser.Where(x => x.Id_country == c.Id).Count() + serialsofuser.Where(x => x.Id_country == c.Id).Count();
                countries.Add(c.Name, count);
            }

            int maxGenre = genres.Values.Max();
            Genre = genres.Where(x => x.Value == maxGenre).FirstOrDefault().Key.Trim();

            var maxCountry = countries.Values.Max();
            Country = countries.Where(x => x.Value == maxCountry).FirstOrDefault().Key.Trim();
        }

        private void OnSearchClicked(object obj)
        {
            Navigation.PushAsync(new SearchPage(AuthoUser));
        }
    }

    public class Stat : INotifyPropertyChanged
    {
        public double mark;
        public int marks;
        public int loves;
        public int letters;
        public int watchlists;

        public double Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");
            }
        }

        public int Marks
        {
            get { return marks; }
            set
            {
                marks = value;
                OnPropertyChanged("Marks");
            }
        }

        public int Loves
        {
            get { return loves; }
            set
            {
                loves = value;
                OnPropertyChanged("Loves");
            }
        }

        public int Letters
        {
            get { return letters; }
            set
            {
                letters = value;
                OnPropertyChanged("Letters");
            }
        }

        public int Watchlists
        {
            get { return watchlists; }
            set
            {
                watchlists = value;
                OnPropertyChanged("Watchlists");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
