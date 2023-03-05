using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class WatchlistModel
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Id_film { get; set; }


        public WatchlistModel()
        {

        }

        public WatchlistModel(Watchlist w)
        {
            Id = w.Id;
            Id_user = w.Id_user;
            Id_film = w.Id_film;
        }
    }
}
