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
    public class CountryRepositorySQL : IRepository<Country> //репозиторий стран
    {
        private Kinocat db; //контекст базы данных

        public CountryRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Country> GetList() //получение списка
        {
            return db.Country.ToList();
        }

        public Country GetItem(int id) //получение по id
        {
            return db.Country.Find(id);
        }

        public Country Create(Country c) //добавление нового
        {
            return db.Country.Add(c).Entity;
        }

        public void Update(Country c) //обновление
        {
            db.Entry(c).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Country c = db.Country.Find(id);
            if (c != null)
                db.Country.Remove(c);
        }
    }
}
