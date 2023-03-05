using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CountryModel()
        {

        }

        public CountryModel(Country c)
        {
            Id = c.Id;
            Name = c.Name;
        }
    }
}
