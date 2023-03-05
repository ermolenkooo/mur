using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FollowingModel
    {
        public int Id { get; set; }
        public int Id_follower { get; set; }
        public int Id_following { get; set; }

        public FollowingModel()
        {

        }

        public FollowingModel(Following f)
        {
            Id = f.Id;
            Id_follower = f.Id_follower;
            Id_following = f.Id_following;
        }
    }
}
