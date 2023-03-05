using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LetterModel
    {
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int Id_film { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }


        public LetterModel()
        {

        }

        public LetterModel(Letter l)
        {
            Id = l.Id;
            Id_user = l.Id_user;
            Text = l.Text;
            Time = l.Time;
            Id_film = l.Id_film;
        }
    }
}
