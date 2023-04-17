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
    public class MarksController : ControllerBase
    {
        IDbCrud crudServ;
        public MarksController()
        {
            crudServ = new DbDataOperation();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] MarkModel mark) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateMark(mark);
            return CreatedAtAction("GetMark", new { id = mark.Id }, mark);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MarkModel mark) //обновление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = crudServ.GetMark(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Mark = mark.Mark;
            item.Id_film = mark.Id_film;
            item.Time = mark.Time;
            item.Id_user = mark.Id_user;
            crudServ.UpdateMark(item);
            return NoContent();
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteMark(id);
            return NoContent();
        }

        [HttpGet("marksofuser/{id}")]
        public IEnumerable<MarkModel> GetAllMarksOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllMarksOfUser(id);
        }

        [HttpGet("marksoffilm/{id}")]
        public IEnumerable<MarkModel> GetAllMarksOfFilm([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllMarksOfFilm(id);
        }
    }
}
