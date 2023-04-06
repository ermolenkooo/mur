using System;
using System.Collections.Generic;
using System.Text;

namespace kinocat.Models
{
    public class Serial : Film
    {
        string seasons;

        public string Seasons
        {
            get { return seasons; }
            set
            {
                seasons = value;
                OnPropertyChanged("Seasons");
            }
        }
    }
}
