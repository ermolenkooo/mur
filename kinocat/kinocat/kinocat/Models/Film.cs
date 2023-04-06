using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace kinocat.Models
{
    public class Film : INotifyPropertyChanged
    {
        int id;
        string name;
        string poster;
        double mark;
        string description;
        string year;
        int id_genre;
        int id_country;
        string age;
        string timing;
        string original;

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

        public string Poster
        {
            get { return poster; }
            set
            {
                poster = value;
                OnPropertyChanged("Poster");
            }
        }

        public double Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        public int Id_genre
        {
            get { return id_genre; }
            set
            {
                id_genre = value;
                OnPropertyChanged("Id_genre");
            }
        }

        public int Id_country
        {
            get { return id_country; }
            set
            {
                id_country = value;
                OnPropertyChanged("Id_country");
            }
        }

        public string Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Timing
        {
            get { return timing; }
            set
            {
                timing = value;
                OnPropertyChanged("Timing");
            }
        }

        public string Original
        {
            get { return original; }
            set
            {
                original = value;
                OnPropertyChanged("Original");
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
