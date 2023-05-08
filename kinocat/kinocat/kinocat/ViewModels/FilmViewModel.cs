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
    class FilmViewModel : BaseViewModel
    {
        public Command UserCommand { get; }
        public Command ClickCommand { get; }
        public Command LoveCommand { get; }
        public Command WantCommand { get; }
        public Command LetterCommand { get; }
        public Command Star1Command { get; }
        public Command Star2Command { get; }
        public Command Star3Command { get; }
        public Command Star4Command { get; }
        public Command Star5Command { get; }

        private Serial selectedFilm;
        private User selectedUser;
        private MarkOfUser mark;
        private Country country;
        private Genre genre;
        private ObservableCollection<MarkOfUser> marks;
        private bool isLove;
        private bool isWant;
        private bool star1;
        private bool star2;
        private bool star3;
        private bool star4;
        private bool star5;

        IEnumerable<MarkOfUser> marksofuser;
        IEnumerable<MarkOfUser> marksoffilm;
        IEnumerable<Following> followings;
        IEnumerable<Love> loves;
        IEnumerable<Watchlist> watch;

        UsersService usersService = new UsersService();
        FollowersService followersService = new FollowersService();
        CountriesService countriesService = new CountriesService();
        GenresService genresService = new GenresService();
        LovesService lovesService = new LovesService();
        MarksService marksService = new MarksService();
        WatchlistsService watchlistsService = new WatchlistsService();

        public INavigation Navigation { get; set; }

        public FilmViewModel(User u, Serial f)
        {
            marks = new ObservableCollection<MarkOfUser>();
            SelectedUser = u;
            SelectedFilm = f;
            UserCommand = new Command(UserClicked);
            LoveCommand = new Command(OnLoveClicked);
            WantCommand = new Command(OnWantClicked);
            LetterCommand = new Command(OnLetterClicked);
            ClickCommand = new Command(OnClicked);
            Star1Command = new Command(OnStar1Clicked);
            Star2Command = new Command(OnStar2Clicked);
            Star3Command = new Command(OnStar3Clicked);
            Star4Command = new Command(OnStar4Clicked);
            Star5Command = new Command(OnStar5Clicked);

            GetData();
        }

        private async void UserClicked(object obj) //переходим к пользователю
        {
            User user = await usersService.GetID((int)obj); 
            await Navigation.PushAsync(new ProfilPage(user, selectedUser));
        }

        private async void GetData()
        {
            marksofuser = await marksService.GetMarksOfUser(selectedUser.Id);
            marksoffilm = await marksService.GetMarksOfFilm(selectedFilm.Id);
            followings = await followersService.GetFollowings(selectedUser.Id);
            Country = await countriesService.GetID(SelectedFilm.Id_country);
            Genre = await genresService.GetID(SelectedFilm.Id_genre);
            Country.Name = Country.Name.TrimEnd(' ');
            Genre.Name = Genre.Name.TrimEnd(' ');

            mark = marksofuser.Where(x => x.Id_film == SelectedFilm.Id).FirstOrDefault();
            if (mark == null)
                selectedFilm.Mark = 0;
            else
                selectedFilm.Mark = mark.Mark;

            List<User> users = new List<User>();
            foreach (var follow in followings)
                users.Add(await usersService.GetID(follow.Id_following));

            foreach (var m in marksoffilm)
                foreach (var us in users)
                    if (m.Id_user == us.Id)
                    {
                        us.Photo = us.Photo.Replace("data:image/jpeg;base64,", "");
                        byte[] Base64Stream = Convert.FromBase64String(us.Photo);
                        us.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                        marks.Add(new MarkOfUser { Mark = m.Mark, Source = us.Source, Id_user = us.Id });
                        break;
                    }

            loves = await lovesService.Get(SelectedUser.Id);
            watch = await watchlistsService.Get(SelectedUser.Id);

            if (loves.Where(x => x.Id_film == selectedFilm.Id).FirstOrDefault() != null)
                IsLove = true;
            else
                IsLove = false;

            if (watch.Where(x => x.Id_film == selectedFilm.Id).FirstOrDefault() != null)
                IsWant = true;
            else
                IsWant = false;

            if (selectedFilm.Mark == 0)
            {
                Star1 = false;
                Star2 = false;
                Star3 = false;
                Star4 = false;
                Star5 = false;
            }
            if (selectedFilm.Mark == 1)
            {
                Star1 = true;
                Star2 = false;
                Star3 = false;
                Star4 = false;
                Star5 = false;
            }
            if (selectedFilm.Mark == 2)
            {
                Star1 = true;
                Star2 = true;
                Star3 = false;
                Star4 = false;
                Star5 = false;
            }
            if (selectedFilm.Mark == 3)
            {
                Star1 = true;
                Star2 = true;
                Star3 = true;
                Star4 = false;
                Star5 = false;
            }
            if (selectedFilm.Mark == 4)
            {
                Star1 = true;
                Star2 = true;
                Star3 = true;
                Star4 = true;
                Star5 = false;
            }
            if (selectedFilm.Mark == 5)
            {
                Star1 = true;
                Star2 = true;
                Star3 = true;
                Star4 = true;
                Star5 = true;
            }
        }

        public ObservableCollection<MarkOfUser> Marks
        {
            get { return marks; }
            set
            {
                if (marks != value)
                {
                    marks = value;
                    OnPropertyChanged("Marks");
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
                    selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }

        public Country Country
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

        public Genre Genre
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

        public bool IsLove
        {
            get { return isLove; }
            set
            {
                if (isLove != value)
                {
                    isLove = value;
                    OnPropertyChanged("IsLove");
                }
            }
        }

        public bool IsWant
        {
            get { return isWant; }
            set
            {
                if (isWant != value)
                {
                    isWant = value;
                    OnPropertyChanged("IsWant");
                }
            }
        }

        public bool Star1
        {
            get { return star1; }
            set
            {
                if (star1 != value)
                {
                    star1 = value;
                    OnPropertyChanged("Star1");
                }
            }
        }

        public bool Star2
        {
            get { return star2; }
            set
            {
                if (star2 != value)
                {
                    star2 = value;
                    OnPropertyChanged("Star2");
                }
            }
        }

        public bool Star3
        {
            get { return star3; }
            set
            {
                if (star3 != value)
                {
                    star3 = value;
                    OnPropertyChanged("Star3");
                }
            }
        }

        public bool Star4
        {
            get { return star4; }
            set
            {
                if (star4 != value)
                {
                    star4 = value;
                    OnPropertyChanged("Star4");
                }
            }
        }

        public bool Star5
        {
            get { return star5; }
            set
            {
                if (star5 != value)
                {
                    star5 = value;
                    OnPropertyChanged("Star5");
                }
            }
        }

        private async void OnLoveClicked(object obj)
        {
            if (IsLove == true)
            {
                IsLove = false;
                await lovesService.Delete(SelectedUser.Id, SelectedFilm.Id);
            }
            else
            {
                IsLove = true;
                await lovesService.Add(new Love { Id_film = SelectedFilm.Id, Id_user = SelectedUser.Id });
            }
        }

        private async void OnWantClicked(object obj)
        {
            if (IsWant == true)
            {
                IsWant = false;
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
            }
            else
            {
                IsWant = true;
                await watchlistsService.Add(new Watchlist { Id_film = SelectedFilm.Id, Id_user = SelectedUser.Id });
            }
        }

        private void OnLetterClicked(object obj) //написать рецензию
        {
            //Navigation.PushAsync(new WriteLetterPage(SelectedUser, SelectedFilm));
        }

        private void OnClicked(object obj) //рецензии
        {
            Navigation.PushAsync(new LettersOfFilmPage(SelectedFilm, SelectedUser));
        }

        private async void OnStar1Clicked(object obj)
        {
            Star1 = true;
            Star2 = false;
            Star3 = false;
            Star4 = false;
            Star5 = false;

            if (mark == null)
            {
                mark = new MarkOfUser { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Mark = 1, Time = DateTime.Now };
                await marksService.Add(mark);
            }
            else
            {
                mark.Mark = 1;
                mark.Time = DateTime.Now;
                await marksService.Update(mark);
            }
            
            if (isWant)
            {
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
                IsWant = false;
            }
        }

        private async void OnStar2Clicked(object obj)
        {
            Star1 = true;
            Star2 = true;
            Star3 = false;
            Star4 = false;
            Star5 = false;

            if (mark == null)
            {
                mark = new MarkOfUser { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Mark = 2, Time = DateTime.Now };
                await marksService.Add(mark);
            }
            else
            {
                mark.Mark = 2;
                mark.Time = DateTime.Now;
                await marksService.Update(mark);
            }
            if (isWant)
            {
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
                IsWant = false;
            }
        }

        private async void OnStar3Clicked(object obj)
        {
            Star1 = true;
            Star2 = true;
            Star3 = true;
            Star4 = false;
            Star5 = false;

            if (mark == null)
            {
                mark = new MarkOfUser { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Mark = 3, Time = DateTime.Now };
                await marksService.Add(mark);
            }
            else
            {
                mark.Mark = 3;
                mark.Time = DateTime.Now;
                await marksService.Update(mark);
            }
            if (isWant)
            {
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
                IsWant = false;
            }
        }

        private async void OnStar4Clicked(object obj)
        {
            Star1 = true;
            Star2 = true;
            Star3 = true;
            Star4 = true;
            Star5 = false;

            if (mark == null)
            {
                mark = new MarkOfUser { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Mark = 4, Time = DateTime.Now };
                await marksService.Add(mark);
            }
            else
            {
                mark.Mark = 4;
                mark.Time = DateTime.Now;
                await marksService.Update(mark);
            }
            if (isWant)
            {
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
                IsWant = false;
            }
        }

        private async void OnStar5Clicked(object obj)
        {
            Star1 = true;
            Star2 = true;
            Star3 = true;
            Star4 = true;
            Star5 = true;

            if (mark == null)
            {
                mark = new MarkOfUser { Id_film = SelectedFilm.Id, Id_user = selectedUser.Id, Mark = 5, Time = DateTime.Now };
                await marksService.Add(mark);
            }
            else
            {
                mark.Mark = 5;
                mark.Time = DateTime.Now;
                await marksService.Update(mark);
            }
            if (isWant)
            {
                await watchlistsService.Delete(SelectedUser.Id, SelectedFilm.Id);
                IsWant = false;
            }
        }
    }
}
