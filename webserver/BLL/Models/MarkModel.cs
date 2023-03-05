using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MarkModel
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Id_film { get; set; }
        public int Mark { get; set; }
        public DateTime Time { get; set; }


        public MarkModel()
        {

        }

        public MarkModel(Mark m)
        {
            Id = m.Id;
            Id_user = m.Id_user;
            Time = m.Time;
            Id_film = m.Id_film;
            Mark = m.Mark1;
        }
    }
}
