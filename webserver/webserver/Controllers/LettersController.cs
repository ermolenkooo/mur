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
    public class LettersController : ControllerBase
    {
        IDbCrud crudServ;
        public LettersController()
        {
            crudServ = new DbDataOperation();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] LetterModel letter) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateLetter(letter);
            return CreatedAtAction("GetLetter", new { id = letter.Id }, letter);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteLetter(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IEnumerable<LetterModel> GetAllLettersOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllLettersOfUser(id);
        }

        [HttpGet("{id}")]
        public IEnumerable<LetterModel> GetAllLettersOfFilm([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllLettersOfFilm(id);
        }
    }
}
