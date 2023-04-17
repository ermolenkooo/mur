using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IDbCrud //интерфейс для crud-функций
    {
        List<FilmModel> GetAllFilms(); //получение списка фильмов
        List<CountryModel> GetAllCountries(); //получение списка стран
        List<GenreModel> GetAllGenres(); //получение списка жанров
        List<AdminModel> GetAllAdmins(); //получение списка администраторов
        List<FollowingModel> GetAllFollowings(int id); //получение списка подписок пользователя
        List<FollowingModel> GetAllFollowers(int id); //получение списка подписчиков пользователя
        List<LetterModel> GetAllLettersOfUser(int id); //получение списка рецензий пользователя
        List<LetterModel> GetAllLettersOfFilm(int id); //получение списка рецензий фильма
        List<LoveModel> GetAllLovesOfUser(int id); //получение списка любимых фильмов пользователя
        List<MarkModel> GetAllMarksOfUser(int id); //получение списка оценок пользователя
        List<MarkModel> GetAllMarksOfFilm(int id); //получение списка оценок фильма
        List<SerialModel> GetAllSerials(); //получение списка сериалов
        List<UserModel> GetAllUsers(); //получение списка пользователей
        List<WatchlistModel> GetAllWatchlistsOfUser(int id); //получение списка вотчлистов пользователя

        FilmModel GetFilm(int Id); //получение фильма по id
        void CreateFilm(FilmModel f); //добавление нового фильма
        void UpdateFilm(FilmModel f); //обновление фильма
        void DeleteFilm(int id); //удаление фильма

        AdminModel GetAdmin(int id); //получение администратора по id
        void CreateAdmin(AdminModel a); //добавление нового администратора
        void DeleteAdmin(int id); //удаление администратора

        CountryModel GetCountry(int Id); //получение страны по id

        void CreateFollowing(FollowingModel f); //подписка
        void DeleteFollowing(int followerid, int followingid); //отписка

        GenreModel GetGenre(int Id); //получение жанра по id

        void CreateLetter(LetterModel l); //добавление новой рецензии
        void DeleteLetter(int userid, int filmid); //удаление рецензии фильма

        void CreateLove(LoveModel l); //добавление фильма в любимое
        void DeleteLove(int userid, int filmid); //удаление фильма из любимого

        MarkModel GetMark(int Id); //получение оценки фильма по id
        void CreateMark(MarkModel m); //добавление новой оценки фильма
        void UpdateMark(MarkModel m); //обновление оценки фильма
        void DeleteMark(int id); //удаление оценки фильма

        SerialModel GetSerial(int Id); //получение сериала по id
        void CreateSerial(SerialModel s); //добавление нового сериала
        void UpdateSerial(SerialModel s); //обновление сериала
        void DeleteSerial(int id); //удаление сериала

        UserModel GetUser(int Id); //получение пользователя по id
        User CreateUser(UserModel u); //добавление нового пользователя
        void UpdateUser(UserModel u); //обновление пользователя
        void DeleteUser(int id); //удаление пользователя

        void CreateWatchlist(WatchlistModel w); //добавление нового фильма в вотчлист
        void DeleteWatchlist(int userid, int filmid); //удаление фильма из вотчлиста
    }
}
