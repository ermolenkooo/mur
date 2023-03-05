using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public AdminModel()
        {

        }

        public AdminModel(Admin a)
        {
            Id = a.Id;
            Name = a.Name;
            Password = a.Password;
        }
    }
}
