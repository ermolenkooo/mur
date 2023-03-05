using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class Kinocat : DbContext
    {
        public Kinocat()
            : base("name=DbContext")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Following> Following { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Letter> Letter { get; set; }
        public virtual DbSet<Love> Love { get; set; }
        public virtual DbSet<Mark> Mark { get; set; }
        public virtual DbSet<Serial> Serial { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Watchlist> Watchlist { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Film)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.Id_country);

            modelBuilder.Entity<Film>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Poster)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Timing)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Original)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Letter)
                .WithRequired(e => e.Film)
                .HasForeignKey(e => e.Id_film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Love)
                .WithRequired(e => e.Film)
                .HasForeignKey(e => e.Id_film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Mark1)
                .WithRequired(e => e.Film)
                .HasForeignKey(e => e.Id_film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasOptional(e => e.Serial)
                .WithRequired(e => e.Film);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Watchlist)
                .WithRequired(e => e.Film)
                .HasForeignKey(e => e.Id_film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Film)
                .WithOptional(e => e.Genre)
                .HasForeignKey(e => e.Id_genre);

            modelBuilder.Entity<Letter>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Serial>()
                .Property(e => e.Seasons)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Following)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Following1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.Id_following)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Letter)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Love)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Mark)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Watchlist)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_user)
                .WillCascadeOnDelete(false);
        }
    }
}
