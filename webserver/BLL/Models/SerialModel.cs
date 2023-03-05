using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SerialModel : FilmModel
    {
        public string Seasons { get; set; }

        public SerialModel()
        {

        }

        public SerialModel(Serial s, Film f)
        {
            Id = s.Id;
            Name = f.Name;
            Id_genre = f.Id_genre;
            Id_country = f.Id_country;
            Timing = f.Timing;
            Description = f.Description;
            Poster = f.Poster;
            Year = f.Year;
            Age = f.Age;
            Original = f.Original;
            Mark = f.Mark;
            Seasons = s.Seasons;
        }
    }
}
