namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mark")]
    public partial class Mark
    {
        public int Id { get; set; }

        [Column("Mark")]
        public int Mark1 { get; set; }

        public int Id_user { get; set; }

        public int Id_film { get; set; }

        public DateTime Time { get; set; }

        public virtual Film Film { get; set; }

        public virtual User User { get; set; }
    }
}
