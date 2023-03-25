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
    public class SerialRepositorySQL : IRepository<Serial> //репозиторий сериалов
    {
        private Kinocat db; //контекст базы данных

        public SerialRepositorySQL(Kinocat dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Serial> GetList() //получение списка
        {
            List<Serial> serials = db.Serial.ToList();
            //db.Dispose();
            return serials;
        }

        public Serial GetItem(int id) //получение по id
        {
            return db.Serial.Find(id);
        }

        public Serial Create(Serial s) //добавление нового
        {
            return db.Serial.Add(s).Entity;
        }

        public void Update(Serial s) //обновление
        {
            db.Entry(s).State = EntityState.Modified;
        }

        public void Delete(int id) //удаление
        {
            Serial s = db.Serial.Find(id);
            if (s != null)
                db.Serial.Remove(s);
        }
    }
}
