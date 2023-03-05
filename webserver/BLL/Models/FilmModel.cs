using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Id_genre { get; set; }
        public int? Id_country { get; set; }
        public string Timing { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Year { get; set; }
        public string Age { get; set; }
        public string Original { get; set; }
        public double? Mark { get; set; }

        public FilmModel()
        {

        }

        public FilmModel(Film f)
        {
            Id = f.Id;
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
        }
    }
}
