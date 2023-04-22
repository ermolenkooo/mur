using kinocat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using kinocat.Services;
using System.Linq;
using kinocat.Views;

namespace kinocat.ViewModels
{
    public class ProfilViewModel : BaseViewModel
    {
        public Command FollowingCommand { get; }
        public Command FollowersCommand { get; }
        public Command MarksCommand { get; }
        public Command WatchlistCommand { get; }
        public Command LoveCommand { get; }
        public Command LettersCommand { get; }
        public Command SearchCommand { get; }
        public Command FilmOrSerialCommand { get; }
        public Command FollowCommand { get; }
        public Command StatCommand { get; }
        public Command MyProfilCommand { get; }

        public INavigation Navigation { get; set; }

        User user, authoUser;
        bool isFilm = true;
        bool isMe = false;
        FollowersService followersService = new FollowersService();

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

        public bool IsFilm
        {
            get { return isFilm; }
            set
            {
                if (isFilm != value)
                {
                    isFilm = value;
                    OnPropertyChanged("IsFilm");
                }
            }
        }

        public bool IsMe
        {
            get { return isMe; }
            set
            {
                if (isMe != value)
                {
                    isMe = value;
                    OnPropertyChanged("IsMe");
                }
            }
        }

        public ProfilViewModel(User u, User authorizeUser)
        {
            AuthoUser = authorizeUser;
            User = u;
            User.Photo = User.Photo.Replace("data:image/jpeg;base64,", "");
            byte[] Base64Stream = Convert.FromBase64String(User.Photo);
            User.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            if (u.Id == authorizeUser.Id)
                IsMe = true;

            GetCount();

            FollowingCommand = new Command(OnFollowingClicked);
            FollowersCommand = new Command(OnFollowersClicked);
            MarksCommand = new Command(OnMarksClicked);
            WatchlistCommand = new Command(OnWatchlistClicked);
            LoveCommand = new Command(OnLoveClicked);
            LettersCommand = new Command(OnLettersClicked);
            SearchCommand = new Command(OnSearchClicked);
            FilmOrSerialCommand = new Command(OnFilmOrSerialClicked);
            FollowCommand = new Command(OnFollowClicked);
            StatCommand = new Command(OnStatClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(AuthoUser, AuthoUser));
        }

        private async void GetCount()
        {
            var followers = await followersService.GetFollowers(User.Id);
            var following = await followersService.GetFollowings(User.Id);
            User.CountOfFollowers = followers.Count();
            User.CountOfFollowing = following.Count();
        }

        private void OnFilmOrSerialClicked(object obj)
        {
            IsFilm = !IsFilm;
        }

        private async void OnFollowClicked(object obj)
        {
            if (User.ReadMe)
            {
                await followersService.Delete(AuthoUser.Id, User.Id);
                User.CountOfFollowers--;
            }
            else
            {
                await followersService.Add(new Following { Id_follower = AuthoUser.Id, Id_following = User.Id });
                User.CountOfFollowers++;
            }
            User.ReadMe = !User.ReadMe;
        }

        private void OnStatClicked(object obj)
        {
            //Navigation.PushAsync(new StatPage(User));
        }

        private void OnFollowingClicked(object obj)
        {
            Navigation.PushAsync(new FollowingPage(User, authoUser, "Подписки"));
        }

        private void OnFollowersClicked(object obj)
        {
            Navigation.PushAsync(new FollowingPage(User, authoUser, "Подписчики"));
        }

        private void OnMarksClicked(object obj)
        {
            if (isFilm)
                Navigation.PushAsync(new MarksPage(User, authoUser, "Фильмы"));
            else
                Navigation.PushAsync(new MarksPage(User, authoUser, "Сериалы"));
        }

        private void OnWatchlistClicked(object obj)
        {
            if (isFilm)
                Navigation.PushAsync(new WatchlistPage(User, authoUser, "Фильмы"));
            else
                Navigation.PushAsync(new WatchlistPage(User, authoUser, "Сериалы"));
        }

        private void OnLoveClicked(object obj)
        {
            //Navigation.PushAsync(new LovePage(User, User, false));
        }

        private void OnLettersClicked(object obj)
        {
            //Navigation.PushAsync(new LettersOfUserPage(User, User, false));
        }

        private void OnSearchClicked(object obj)
        {
            //Navigation.PushAsync(new SearchPage(User));
        }
    }
}
