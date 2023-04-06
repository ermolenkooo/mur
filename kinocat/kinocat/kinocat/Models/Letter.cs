using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace kinocat.Models
{
    public class Letter : INotifyPropertyChanged
    {
        int id;
        int id_film;
        int id_user;
        string text;
        DateTime time;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Id_film
        {
            get { return id_film; }
            set
            {
                id_film = value;
                OnPropertyChanged("Id_film");
            }
        }

        public int Id_user
        {
            get { return id_user; }
            set
            {
                id_user = value;
                OnPropertyChanged("Id_user");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
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
