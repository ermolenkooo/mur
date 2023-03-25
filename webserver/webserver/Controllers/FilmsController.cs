using BLL.Interfaces;
using BLL.Models;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase //контроллер фильмов
    {
        IDbCrud crudServ;
        public FilmsController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetFilm([FromRoute] int id) //получение фильма по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var film = crudServ.GetFilm(id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] FilmModel film) //добавление нового фильма
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string base64ImageRepresentation = "data:image/jpeg;base64,";
            byte[] imageArray = System.IO.File.ReadAllBytes(film.Poster);
            base64ImageRepresentation += Convert.ToBase64String(imageArray);
            film.Poster = base64ImageRepresentation;
            crudServ.CreateFilm(film);
            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] FilmModel film) //обновление фильма
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = crudServ.GetFilm(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = film.Name;
            item.Id_genre = film.Id_genre;
            item.Id_country = film.Id_country;
            item.Timing = film.Timing;
            item.Description = film.Description;
            string base64ImageRepresentation = "data:image/jpeg;base64,";
            byte[] imageArray = System.IO.File.ReadAllBytes(film.Poster);
            base64ImageRepresentation += Convert.ToBase64String(imageArray);
            item.Poster = base64ImageRepresentation;
            item.Year = film.Year;
            item.Age = film.Age;
            item.Original = film.Original;
            item.Mark = film.Mark;
            crudServ.UpdateFilm(item);
            return NoContent();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление фильма
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteFilm(id);
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<FilmModel> GetAllFilms() //получение списка фильмов
        {
            return crudServ.GetAllFilms();
        }
    }
}
