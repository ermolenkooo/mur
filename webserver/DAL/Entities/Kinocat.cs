using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Sockets;

namespace DAL.Entities
{
    public partial class Kinocat : DbContext
    {
        /*public Kinocat()
            : base("name=DbContext")
        {
        }*/
        public Kinocat(DbContextOptions<Kinocat> options) : base(options)
        { }
        public Kinocat() 
        { }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //подключение к базе данных
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-NE8A180M\\SQLEXPRESS;Database=kinocat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Genre)
                .WithMany(p => p.Film)
                .HasForeignKey(d => d.Id_genre);
                entity.HasOne(d => d.Country)
                .WithMany(p => p.Film)
                .HasForeignKey(d => d.Id_country);
                entity.HasOne(f => f.Serial)
                .WithOne(s => s.Film)
                .HasForeignKey<Serial>(s => s.Id);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            /*modelBuilder.Entity<Serial>(entity =>
            {
                entity.HasOne(d => d.Film)
                .WithOne(p => p.Serial)
                .HasForeignKey(d => d.Id);
            });*/                

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Photo).IsRequired();
            });

            modelBuilder.Entity<Following>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.Following)
                .HasForeignKey(d => d.Id_follower);
                entity.HasOne(d => d.User1)
                .WithMany(p => p.Following1)
                .HasForeignKey(d => d.Id_following);
            });

            modelBuilder.Entity<Watchlist>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.Watchlist)
                .HasForeignKey(d => d.Id_user);
                entity.HasOne(d => d.Film)
                .WithMany(p => p.Watchlist)
                .HasForeignKey(d => d.Id_film);
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.Mark)
                .HasForeignKey(d => d.Id_user);
                entity.HasOne(d => d.Film)
                .WithMany(p => p.Mark1)
                .HasForeignKey(d => d.Id_film);
            });

            modelBuilder.Entity<Love>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.Love)
                .HasForeignKey(d => d.Id_user);
                entity.HasOne(d => d.Film)
                .WithMany(p => p.Love)
                .HasForeignKey(d => d.Id_film);
            });

            modelBuilder.Entity<Letter>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.Letter)
                .HasForeignKey(d => d.Id_user);
                entity.HasOne(d => d.Film)
                .WithMany(p => p.Letter)
                .HasForeignKey(d => d.Id_film);
                entity.Property(e => e.Text).IsRequired();
            });

            /*modelBuilder.Entity<Admin>()
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
                .WillCascadeOnDelete(false);*/
        }
    }
}
