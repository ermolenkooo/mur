using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LoveModel
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Id_film { get; set; }


        public LoveModel()
        {

        }

        public LoveModel(Love l)
        {
            Id = l.Id;
            Id_user = l.Id_user;
            Id_film = l.Id_film;
        }
    }
}
