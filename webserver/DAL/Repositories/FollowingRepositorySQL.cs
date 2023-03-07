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
    public class FollowingRepositorySQL : IRepository<Following> //репозиторий подписок
    {
        private Kinocat db; //контекст базы данных

        public FollowingRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Following> GetList() //получение списка
        {
            List<Following> followings = db.Following.ToList();
            db.Dispose();
            return followings;
        }

        public Following GetItem(int id) //получение по id
        {
            return db.Following.Find(id);
        }

        public Following Create(Following f) //добавление нового
        {
            return db.Following.Add(f).Entity;
        }

        public void Update(Following f) //обновление
        {
            db.Entry(f).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Following f = db.Following.Find(id);
            if (f != null)
                db.Following.Remove(f);
        }
    }
}
