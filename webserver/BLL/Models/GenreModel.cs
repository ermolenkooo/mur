using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenreModel()
        {

        }

        public GenreModel(Genre g)
        {
            Id = g.Id;
            Name = g.Name;
        }
    }
}
