using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class AdminRepositorySQL : IRepository<Admin> //репозиторий администраторов
    {
        private Kinocat db; //контекст базы данных

        public AdminRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Admin> GetList() //получение списка
        {
            List<Admin> admins = db.Admin.ToList();
            db.Dispose();
            return admins;
        }

        public Admin GetItem(int id) //получение по id
        {
            return db.Admin.Find(id);
        }

        public Admin Create(Admin a) //добавление нового
        {
            return db.Admin.Add(a).Entity;
        }

        public void Update(Admin a) //обновление
        {
            db.Entry(a).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Admin a = db.Admin.Find(id);
            if (a != null)
                db.Admin.Remove(a);
        }
    }
}
