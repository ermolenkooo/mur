using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetList(); //получение всех объектов
        T GetItem(int id); //получение одного объекта по id
        T Create(T item); //создание объекта
        void Update(T item); //обновление объекта
        void Delete(int id); //удаление объекта по id
    }
}
