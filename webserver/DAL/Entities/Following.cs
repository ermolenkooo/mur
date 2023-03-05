namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Following")]
    public partial class Following
    {
        public int Id { get; set; }

        public int Id_follower { get; set; }

        public int Id_following { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
