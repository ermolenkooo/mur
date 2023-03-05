using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using static System.Collections.Specialized.BitVector32;
using System.Net.Sockets;

namespace DAL.Repositories
{
    public class DbReposSQL : IDbRepos
    {
        private Kinocat db; //контекст базы данных
        private FilmRepositorySQL filmRepository; //репозитории
        private CountryRepositorySQL countryRepository;
        private GenreRepositorySQL genreRepository;
        private AdminRepositorySQL adminRepository;
        private FollowingRepositorySQL followingRepository;
        private LetterRepositorySQL letterRepository;
        private LoveRepositorySQL loveRepository;
        private MarkRepositorySQL markRepository;
        private SerialRepositorySQL serialRepository;
        private UserRepositorySQL userRepository;
        private WatchlistRepositorySQL watchlistRepository;

        public DbReposSQL()
        {
            db = new Kinocat(); //создание контекста
        }

        public IRepository<Film> Films //создание репозитория фильмов
        {
            get
            {
                if (filmRepository == null)
                    filmRepository = new FilmRepositorySQL(db);
                return filmRepository;
            }
        }

        public IRepository<Country> Countries //создание репозитория стран
        {
            get
            {
                if (countryRepository == null)
                    countryRepository = new CountryRepositorySQL(db);
                return countryRepository;
            }
        }

        public IRepository<Genre> Genres //создание репозитория жанров
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepositorySQL(db);
                return genreRepository;
            }
        }

        public IRepository<Admin> Admins //создание репозитория администраторов
        {
            get
            {
                if (adminRepository == null)
                    adminRepository = new AdminRepositorySQL(db);
                return adminRepository;
            }
        }

        public IRepository<Following> Followings //создание репозитория подписок
        {
            get
            {
                if (followingRepository == null)
                    followingRepository = new FollowingRepositorySQL(db);
                return followingRepository;
            }
        }

        public IRepository<Letter> Letters //создание репозитория рецензий
        {
            get
            {
                if (letterRepository == null)
                    letterRepository = new LetterRepositorySQL(db);
                return letterRepository;
            }
        }

        public IRepository<Love> Loves //создание репозитория любимых фильмов
        {
            get
            {
                if (loveRepository == null)
                    loveRepository = new LoveRepositorySQL(db);
                return loveRepository;
            }
        }

        public IRepository<Mark> Marks //создание репозитория оценок
        {
            get
            {
                if (markRepository == null)
                    markRepository = new MarkRepositorySQL(db);
                return markRepository;
            }
        }

        public IRepository<Serial> Serials //создание репозитория сериалов
        {
            get
            {
                if (serialRepository == null)
                    serialRepository = new SerialRepositorySQL(db);
                return serialRepository;
            }
        }

        public IRepository<User> Users //создание репозитория пользователей
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepositorySQL(db);
                return userRepository;
            }
        }

        public IRepository<Watchlist> Watchlists //создание репозитория вотчлистов
        {
            get
            {
                if (watchlistRepository == null)
                    watchlistRepository = new WatchlistRepositorySQL(db);
                return watchlistRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
