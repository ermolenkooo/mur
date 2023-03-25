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
    public class UserRepositorySQL : IRepository<User> //репозиторий пользователей
    {
        private Kinocat db; //контекст базы данных

        public UserRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<User> GetList() //получение списка
        {
            List<User> users = db.User.ToList();
            //db.Dispose();
            return users;
        }

        public User GetItem(int id) //получение по id
        {
            return db.User.Find(id);
        }

        public User Create(User u) //добавление нового
        {
            return db.User.Add(u).Entity;
        }

        public void Update(User u) //обновление
        {
            db.Entry(u).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            User u = db.User.Find(id);
            if (u != null)
                db.User.Remove(u);
        }
    }
}
