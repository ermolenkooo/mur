using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace kinocat.Models
{
    public class User : INotifyPropertyChanged
    {
        public int id;
        public string name;
        public string photo;
        public string email;
        public string password;
        public bool readMe;
        public int countOfFollowers;
        public int countOfFollowing;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public bool ReadMe
        {
            get { return readMe; }
            set
            {
                readMe = value;
                OnPropertyChanged("ReadMe");
            }
        }

        public int CountOfFollowers
        {
            get { return countOfFollowers; }
            set
            {
                countOfFollowers = value;
                OnPropertyChanged("CountOfFollowers");
            }
        }

        public int CountOfFollowing
        {
            get { return countOfFollowing; }
            set
            {
                countOfFollowing = value;
                OnPropertyChanged("CountOfFollowing");
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
