using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Repositories
{
    public class GenreRepositorySQL : IRepository<Genre> //репозиторий жанров
    {
        private Kinocat db; //контекст базы данных

        public GenreRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Genre> GetList() //получение списка
        {
            return db.Genre.ToList();
        }

        public Genre GetItem(int id) //получение по id
        {
            return db.Genre.Find(id);
        }

        public Genre Create(Genre g) //добавление нового
        {
            return db.Genre.Add(g).Entity;
        }

        public void Update(Genre g) //обновление
        {
            db.Entry(g).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Genre g = db.Genre.Find(id);
            if (g != null)
                db.Genre.Remove(g);
        }
    }
}
