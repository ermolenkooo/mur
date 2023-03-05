using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using static System.Collections.Specialized.BitVector32;

namespace DAL.Interfaces 
{
    public interface IDbRepos //интерфейс для взаимодействия с репозиториями
    {
        IRepository<Country> Countries { get; }
        IRepository<Film> Films { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Admin> Admins { get; }
        IRepository<Following> Followings { get; }
        IRepository<Letter> Letters { get; }
        IRepository<Love> Loves { get; }
        IRepository<Mark> Marks { get; }
        IRepository<Serial> Serials { get; }
        IRepository<User> Users { get; }
        IRepository<Watchlist> Watchlists { get; }
        int Save();
    }
}
