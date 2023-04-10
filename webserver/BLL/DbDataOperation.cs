using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BLL
{
    public class DbDataOperation : IDbCrud
    {
        IDbRepos db; // реализация репозитория

        public DbDataOperation()
        {
            try
            {
                db = new DbReposSQL(); //создание репозитория
            }
            catch
            {
                //ошибка и выход из приложения
            }
        }

        public List<FilmModel> GetAllFilms() //получение списка фильмов
        {
            List<FilmModel> films = db.Films.GetList().Select(i => new FilmModel(i)).ToList();
            var serials = db.Serials.GetList();
            foreach (var s in serials)
                films.Remove(films.Where(x => x.Id == s.Id).FirstOrDefault());
            return films;
        }
        public List<CountryModel> GetAllCountries() //получение списка стран
        {
            return db.Countries.GetList().Select(i => new CountryModel(i)).ToList();
        }
        public List<GenreModel> GetAllGenres() //получение списка жанров
        {
            return db.Genres.GetList().Select(i => new GenreModel(i)).ToList();
        }
        public List<AdminModel> GetAllAdmins() //получение списка администраторов
        {
            return db.Admins.GetList().Select(i => new AdminModel(i)).ToList();
        }
        public List<FollowingModel> GetAllFollowings(int id) //получение списка подписок пользователя
        {
            return db.Followings.GetList().Where(i => i.Id_follower == id).Select(i => new FollowingModel(i)).ToList();
        }
        public List<FollowingModel> GetAllFollowers(int id) //получение списка подписчиков пользователя
        {
            return db.Followings.GetList().Where(i => i.Id_following == id).Select(i => new FollowingModel(i)).ToList();
        }
        public List<LetterModel> GetAllLettersOfUser(int id) //получение списка рецензий пользователя
        {
            return db.Letters.GetList().Where(i => i.Id_user == id).Select(i => new LetterModel(i)).ToList();
        }
        public List<LetterModel> GetAllLettersOfFilm(int id) //получение списка рецензий фильма
        {
            return db.Letters.GetList().Where(i => i.Id_film == id).Select(i => new LetterModel(i)).ToList();
        }
        public List<LoveModel> GetAllLovesOfUser(int id) //получение списка любимых фильмов пользователя
        {
            return db.Loves.GetList().Where(i => i.Id_user == id).Select(i => new LoveModel(i)).ToList();
        }
        public List<MarkModel> GetAllMarksOfUser(int id) //получение списка оценок пользователя
        {
            return db.Marks.GetList().Where(i => i.Id_user == id).Select(i => new MarkModel(i)).ToList();
        }
        public List<MarkModel> GetAllMarksOfFilm(int id) //получение списка оценок фильма
        {
            return db.Marks.GetList().Where(i => i.Id_film == id).Select(i => new MarkModel(i)).ToList();
        }
        public List<SerialModel> GetAllSerials() //получение списка сериалов
        {
            var list = db.Serials.GetList();
            List<SerialModel> serials = new List<SerialModel>();
            foreach (var s in list)
                serials.Add(new SerialModel(s, db.Films.GetItem(s.Id)));
            return serials;
        }
        public List<UserModel> GetAllUsers() //получение списка пользователей
        {
            return db.Users.GetList().Select(i => new UserModel(i)).ToList();
        }
        public List<WatchlistModel> GetAllWatchlistsOfUser(int id) //получение списка вотчлистов пользователя
        {
            return db.Watchlists.GetList().Where(i => i.Id_user == id).Select(i => new WatchlistModel(i)).ToList();
        }

        public FilmModel GetFilm(int Id) //получение фильма по id
        {
            return new FilmModel(db.Films.GetItem(Id));
        }
        public void CreateFilm(FilmModel f) //добавление нового фильма
        {
            db.Films.Create(new Film() 
            {
                Age = f.Age, 
                Description = f.Description, 
                Id_country = f.Id_country,
                Id_genre = f.Id_genre, 
                Mark = f.Mark, 
                Name = f.Name, 
                Original = f.Original, 
                Poster = f.Poster, 
                Timing = f.Timing,
                Year = f.Year
            });
            Save();
        }
        public void UpdateFilm(FilmModel f) //обновление фильма
        {
            Film film = db.Films.GetItem(f.Id);
            film.Name = f.Name;
            film.Original = f.Original;
            film.Poster = f.Poster;
            film.Timing = f.Timing;
            film.Year = f.Year;
            film.Description = f.Description;
            film.Id_country = f.Id_country;
            film.Id_genre = f.Id_genre;
            film.Mark = f.Mark;
            film.Age = f.Age;
            db.Films.Update(film);
            Save();
        }
        public void DeleteFilm(int id) //удаление фильма
        {
            Film f = db.Films.GetItem(id);
            if (f != null)
            {
                var letters = db.Letters.GetList().Where(i => i.Id_film == id);
                var loves = db.Loves.GetList().Where(i => i.Id_film == id);
                var marks = db.Marks.GetList().Where(i => i.Id_film == id);
                var watchlists = db.Watchlists.GetList().Where(i => i.Id_film == id);
                foreach (var l in letters)
                    db.Letters.Delete(l.Id);
                foreach (var l in loves)
                    db.Loves.Delete(l.Id);
                foreach (var m in marks)
                    db.Marks.Delete(m.Id);
                foreach (var w in watchlists)
                    db.Watchlists.Delete(w.Id);
                db.Films.Delete(f.Id);
                Save();
            }
        }

        public AdminModel GetAdmin(int Id) //получение администратора по id
        {
            return new AdminModel(db.Admins.GetItem(Id));
        }
        public void CreateAdmin(AdminModel a) //добавление нового администратора
        {
            db.Admins.Create(new Admin()
            {
                Name = a.Name,
                Password = a.Password
            });
            Save();
        }
        public void DeleteAdmin(int id) //удаление администратора
        {
            Admin a = db.Admins.GetItem(id);
            if (a != null)
            {
                db.Admins.Delete(a.Id);
                Save();
            }
        }

        public CountryModel GetCountry(int Id) //получение страны по id
        {
            return new CountryModel(db.Countries.GetItem(Id));
        }

        public void CreateFollowing(FollowingModel f) //подписка
        {
            db.Followings.Create(new Following()
            {
                Id_follower = f.Id_follower,
                Id_following = f.Id_following
            });
            Save();
        }
        public void DeleteFollowing(int followerid, int followingid) //отписка
        {
            var f = db.Followings.GetList().Where(x => x.Id_follower == followerid && x.Id_following == followingid).First();
            if (f != null)
            {
                db.Followings.Delete(f.Id);
                Save();
            }
        }

        public GenreModel GetGenre(int Id) //получение жанра по id
        {
            return new GenreModel(db.Genres.GetItem(Id));
        }

        public void CreateLetter(LetterModel l) //добавление новой рецензии
        {
            db.Letters.Create(new Letter()
            {
                Time = l.Time,
                Text = l.Text,
                Id_film = l.Id_film,
                Id_user = l.Id_user
            });
            Save();
        }
        public void DeleteLetter(int id) //удаление рецензии фильма
        {
            Letter l = db.Letters.GetItem(id);
            if (l != null)
            {
                db.Letters.Delete(l.Id);
                Save();
            }
        }

        public void CreateLove(LoveModel l) //добавление фильма в любимое
        {
            db.Loves.Create(new Love()
            {
                Id_film = l.Id_film,
                Id_user = l.Id_user
            });
            Save();
        }
        public void DeleteLove(int id) //удаление фильма из любимого
        {
            Love l = db.Loves.GetItem(id);
            if (l != null)
            {
                db.Loves.Delete(l.Id);
                Save();
            }
        }

        public MarkModel GetMark(int Id) //получение оценки фильма по id
        {
            return new MarkModel(db.Marks.GetItem(Id));
        }
        public void CreateMark(MarkModel m) //добавление новой оценки фильма
        {
            db.Marks.Create(new Mark()
            {
                Time = m.Time,
                Mark1 = m.Mark,
                Id_film = m.Id_film,
                Id_user = m.Id_user
            });
            Save();
        }
        public void UpdateMark(MarkModel m) //обновление оценки фильма
        {
            Mark mark = db.Marks.GetItem(m.Id);
            mark.Time = m.Time;
            mark.Mark1 = m.Mark;
            db.Marks.Update(mark);
            Save();
        }
        public void DeleteMark(int id) //удаление оценки фильма
        {
            Mark m = db.Marks.GetItem(id);
            if (m != null)
            {
                db.Marks.Delete(m.Id);
                Save();
            }
        }

        public SerialModel GetSerial(int Id) //получение сериала по id
        {
            return new SerialModel(db.Serials.GetItem(Id), db.Films.GetItem(Id));
        }
        public void CreateSerial(SerialModel s) //добавление нового сериала
        {
            var f = db.Films.Create(new Film()
            {
                Age = s.Age,
                Description = s.Description,
                Id_country = s.Id_country,
                Id_genre = s.Id_genre,
                Mark = s.Mark,
                Name = s.Name,
                Original = s.Original,
                Poster = s.Poster,
                Timing = s.Timing,
                Year = s.Year
            });
            Save();
            db.Serials.Create(new Serial()
            {
                Id = f.Id,
                Seasons = s.Seasons
            });
            Save();
        }
        public void UpdateSerial(SerialModel s) //обновление сериала
        {
            Film film = db.Films.GetItem(s.Id);
            film.Name = s.Name;
            film.Original = s.Original;
            film.Poster = s.Poster;
            film.Timing = s.Timing;
            film.Year = s.Year;
            film.Description = s.Description;
            film.Id_country = s.Id_country;
            film.Id_genre = s.Id_genre;
            film.Mark = s.Mark;
            film.Age = s.Age;
            db.Films.Update(film);
            Serial serial = db.Serials.GetItem(s.Id);
            serial.Seasons = s.Seasons;
            db.Serials.Update(serial);
            Save();
        }
        public void DeleteSerial(int id) //удаление сериала
        {
            Serial serial = db.Serials.GetItem(id);
            if (serial != null)
            {
                db.Serials.Delete(id);
                Film f = db.Films.GetItem(id);
                var letters = db.Letters.GetList().Where(i => i.Id_film == id);
                var loves = db.Loves.GetList().Where(i => i.Id_film == id);
                var marks = db.Marks.GetList().Where(i => i.Id_film == id);
                var watchlists = db.Watchlists.GetList().Where(i => i.Id_film == id);
                foreach (var l in letters)
                    db.Letters.Delete(l.Id);
                foreach (var l in loves)
                    db.Loves.Delete(l.Id);
                foreach (var m in marks)
                    db.Marks.Delete(m.Id);
                foreach (var w in watchlists)
                    db.Watchlists.Delete(w.Id);
                db.Films.Delete(f.Id);
                Save();
            }
        }

        public UserModel GetUser(int Id) //получение пользователя по id
        {
            return new UserModel(db.Users.GetItem(Id));
        }
        public User CreateUser(UserModel u) //добавление нового пользователя
        {
            var user = db.Users.Create(new User()
            {
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                Photo = u.Photo
            });
            Save();
            return user;
        }
        public void UpdateUser(UserModel u) //обновление пользователя
        {
            User user = db.Users.GetItem(u.Id);
            user.Name = u.Name;
            user.Email = u.Email;
            user.Password = u.Password;
            user.Photo = u.Photo;
            db.Users.Update(user);
            Save();
        }
        public void DeleteUser(int id) //удаление пользователя
        {
            User u = db.Users.GetItem(id);
            if (u != null)
            {
                var letters = db.Letters.GetList().Where(i => i.Id_user == id);
                var loves = db.Loves.GetList().Where(i => i.Id_user == id);
                var marks = db.Marks.GetList().Where(i => i.Id_user == id);
                var watchlists = db.Watchlists.GetList().Where(i => i.Id_user == id);
                var followings = db.Followings.GetList().Where(i => i.Id_follower == id || i.Id_following == id);
                foreach (var l in letters)
                    db.Letters.Delete(l.Id);
                foreach (var l in loves)
                    db.Loves.Delete(l.Id);
                foreach (var m in marks)
                    db.Marks.Delete(m.Id);
                foreach (var w in watchlists)
                    db.Watchlists.Delete(w.Id);
                foreach (var f in followings)
                    db.Followings.Delete(f.Id);
                db.Users.Delete(u.Id);
                Save();
            }
        }

        public void CreateWatchlist(WatchlistModel w) //добавление нового фильма в вотчлист
        {
            db.Watchlists.Create(new Watchlist()
            {
                Id_film = w.Id_film,
                Id_user = w.Id_user
            });
            Save();
        }
        public void DeleteWatchlist(int id) //удаление фильма из вотчлиста
        {
            Watchlist w = db.Watchlists.GetItem(id);
            if (w != null)
            {
                db.Watchlists.Delete(w.Id);
                Save();
            }
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
