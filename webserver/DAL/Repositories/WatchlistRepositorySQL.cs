using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WatchlistRepositorySQL : IRepository<Watchlist> //репозиторий вотчлистов
    {
        private Kinocat db; //контекст базы данных

        public WatchlistRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Watchlist> GetList() //получение списка
        {
            List<Watchlist> watchlists = db.Watchlist.ToList();
            //db.Dispose();
            return watchlists;
        }

        public Watchlist GetItem(int id) //получение по id
        {
            return db.Watchlist.Find(id);
        }

        public Watchlist Create(Watchlist w) //добавление нового
        {
            var n = db.Watchlist.Add(w);
            return db.Watchlist.Add(w).Entity;
        }

        public void Update(Watchlist w) //обновление
        {
            db.Entry(w).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Watchlist w = db.Watchlist.Find(id);
            if (w != null)
                db.Watchlist.Remove(w);
        }
    }
}
