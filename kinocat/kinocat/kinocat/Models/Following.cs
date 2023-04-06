using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace kinocat.Models
{
    public class Following : INotifyPropertyChanged
    {
        int id;
        int id_follower;
        int id_following;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Id_follower
        {
            get { return id_follower; }
            set
            {
                id_follower = value;
                OnPropertyChanged("Id_follower");
            }
        }

        public int Id_following
        {
            get { return id_following; }
            set
            {
                id_following = value;
                OnPropertyChanged("Id_following");
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
