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
    public class MarkRepositorySQL : IRepository<Mark> //репозиторий оценок
    {
        private Kinocat db; //контекст базы данных

        public MarkRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Mark> GetList() //получение списка
        {
            List<Mark> marks = db.Mark.ToList();
            db.Dispose();
            return marks;
        }

        public Mark GetItem(int id) //получение по id
        {
            return db.Mark.Find(id);
        }

        public Mark Create(Mark m) //добавление нового
        {
            return db.Mark.Add(m).Entity;
        }

        public void Update(Mark m) //обновление
        {
            db.Entry(m).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Mark m = db.Mark.Find(id);
            if (m != null)
                db.Mark.Remove(m);
        }
    }
}
