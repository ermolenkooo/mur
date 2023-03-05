using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public UserModel()
        {

        }

        public UserModel(User u)
        {
            Id = u.Id;
            Name = u.Name;
            Email = u.Email;
            Password = u.Password;
            Photo = u.Photo;
        }
    }
}
