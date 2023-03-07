using BLL.Interfaces;
using BLL.Models;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase 
    {
        IDbCrud crudServ;
        public GenresController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetGenre([FromRoute] int id) //получение по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = crudServ.GetGenre(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpGet]
        public IEnumerable<GenreModel> GetAllGenres() //получение списка
        {
            return crudServ.GetAllGenres();
        }
    }
}
