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
    public class LoveRepositorySQL : IRepository<Love> //репозиторий любимых фильмов
    {
        private Kinocat db; //контекст базы данных

        public LoveRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Love> GetList() //получение списка
        {
            List<Love> loves = db.Love.ToList();
            db.Dispose();
            return loves;
        }

        public Love GetItem(int id) //получение по id
        {
            return db.Love.Find(id);
        }

        public Love Create(Love l) //добавление нового
        {
            return db.Love.Add(l).Entity;
        }

        public void Update(Love l) //обновление
        {
            db.Entry(l).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Love l = db.Love.Find(id);
            if (l != null)
                db.Love.Remove(l);
        }
    }
}
