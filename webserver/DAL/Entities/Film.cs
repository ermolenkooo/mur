namespace DAL.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Film")]
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            Letter = new HashSet<Letter>();
            Love = new HashSet<Love>();
            Mark1 = new HashSet<Mark>();
            Watchlist = new HashSet<Watchlist>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Poster { get; set; }

        [StringLength(20)]
        public string Year { get; set; }

        [StringLength(5)]
        public string Age { get; set; }

        [StringLength(15)]
        public string Timing { get; set; }

        [StringLength(100)]
        public string Original { get; set; }

        public double? Mark { get; set; }

        public int? Id_country { get; set; }

        public int? Id_genre { get; set; }

        public virtual Country Country { get; set; }

        public virtual Genre Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Letter> Letter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Love> Love { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Mark1 { get; set; }

        public virtual Serial Serial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Watchlist> Watchlist { get; set; }
    }
}
