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
    public class LetterRepositorySQL : IRepository<Letter> //репозиторий рецензий
    {
        private Kinocat db; //контекст базы данных

        public LetterRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Letter> GetList() //получение списка
        {
            List<Letter> letters = db.Letter.ToList();
            //db.Dispose();
            return letters;
        }

        public Letter GetItem(int id) //получение по id
        {
            return db.Letter.Find(id);
        }

        public Letter Create(Letter l) //добавление нового
        {
            return db.Letter.Add(l).Entity;
        }

        public void Update(Letter l) //обновление
        {
            db.Entry(l).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Letter l = db.Letter.Find(id);
            if (l != null)
                db.Letter.Remove(l);
        }
    }
}
