namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Love")]
    public partial class Love
    {
        public int Id { get; set; }

        public int Id_user { get; set; }

        public int Id_film { get; set; }

        public virtual Film Film { get; set; }

        public virtual User User { get; set; }
    }
}
