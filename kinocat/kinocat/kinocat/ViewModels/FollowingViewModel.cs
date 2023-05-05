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
    public class FollowingViewModel : BaseViewModel
    {
        public Command BackCommand { get; }
        public Command ClickCommand { get; }
        public Command SearchCommand { get; }
        public Command MyProfilCommand { get; }

        private User selectedUser;
        private User authoUser;
        private ObservableCollection<User> follow;
        private string type;

        public INavigation Navigation { get; set; }
        UsersService usersService = new UsersService();
        FollowersService followersService = new FollowersService();

        public FollowingViewModel(User u, User authoUser, string type)
        {
            selectedUser = u;
            this.authoUser = authoUser;
            this.type = type;
            BackCommand = new Command(OnBackClicked);
            ClickCommand = new Command(OnButtonClicked);
            SearchCommand = new Command(OnSearchClicked);
            MyProfilCommand = new Command(OnMyProfilClicked);
            follow = new ObservableCollection<User>();

            GetFollow();
        }

        private void OnMyProfilClicked(object obj)
        {
            Navigation.PushAsync(new ProfilPage(AuthoUser, AuthoUser));
        }

        private void OnSearchClicked(object obj)
        {
            Navigation.PushAsync(new SearchPage(AuthoUser));
        }

        private async void GetFollow()
        {
            var followingsAuthoUser = await followersService.GetFollowings(AuthoUser.Id);
            if (type == "Подписки") 
            {
                var followingsSelectedUser = await followersService.GetFollowings(SelectedUser.Id);
                foreach(var f in followingsSelectedUser)
                {
                    User u = await usersService.GetID(f.Id_following);
                    if (u.Id == AuthoUser.Id)
                        u.IsMe = true;
                    u.Photo = u.Photo.Replace("data:image/jpeg;base64,", "");
                    byte[] Base64Stream = Convert.FromBase64String(u.Photo);
                    u.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                    if (followingsAuthoUser.Where(x => x.Id_following == f.Id_following).Any())
                        u.ReadMe = true;
                    Follow.Add(u);
                }
            }
            else
                if (type == "Подписчики")
                {
                    var followersSelectedUser = await followersService.GetFollowers(SelectedUser.Id);
                    foreach (var f in followersSelectedUser)
                    {
                        User u = await usersService.GetID(f.Id_follower);
                        if (u.Id == AuthoUser.Id)
                            u.IsMe = true;
                        u.Photo = u.Photo.Replace("data:image/jpeg;base64,", "");
                        byte[] Base64Stream = Convert.FromBase64String(u.Photo);
                        u.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                        if (followingsAuthoUser.Where(x => x.Id_following == f.Id_follower).Any())
                            u.ReadMe = true;
                        Follow.Add(u);
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
                    User tempUser = value;
                    selectedUser = null;
                    OnPropertyChanged("SelectedUser");
                    Navigation.PushAsync(new ProfilPage(tempUser, AuthoUser));
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
                    OnPropertyChanged("Type");
                }
            }
        }

        public ObservableCollection<User> Follow
        {
            get { return follow; }
            set
            {
                if (follow != value)
                {
                    follow = value;
                    OnPropertyChanged("Follow");
                }
            }
        }

        private void OnBackClicked(object obj) //стрелка назад
        {
            Navigation.PopAsync();
        }

        private async void OnButtonClicked(object obj) //клик на кнопку отписаться/подписаться
        {
            foreach (var f in follow)
            {
                if (f.Id == (int)obj)
                {
                    if (f.ReadMe)
                        await followersService.Delete(AuthoUser.Id, f.Id);
                    else
                        await followersService.Add(new Following { Id_follower = AuthoUser.Id, Id_following = f.Id });
                    f.ReadMe = !f.ReadMe;
                }
            }
        }
    }
}
