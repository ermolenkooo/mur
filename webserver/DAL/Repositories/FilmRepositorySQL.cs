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
    public class FilmRepositorySQL : IRepository<Film> //репозиторий фильмов
    {
        private Kinocat db; //контекст базы данных

        public FilmRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Film> GetList() //получение списка фильмов
        {
            List<Film> films = db.Film.ToList();
            //db.Dispose();
            return films;
        }

        public Film GetItem(int id) //получение фильма по id
        {
            return db.Film.Find(id);
        }

        public Film Create(Film f) //добавление нового фильма
        {
            return db.Film.Add(f).Entity;
        }

        public void Update(Film f) //обновление фильма 
        {
            db.Entry(f).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление фильма
        {
            Film f = db.Film.Find(id);
            if (f != null)
                db.Film.Remove(f);
        }
    }
}
