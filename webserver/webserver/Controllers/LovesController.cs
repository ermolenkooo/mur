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
    public class LovesController : ControllerBase
    {
        IDbCrud crudServ;
        public LovesController()
        {
            crudServ = new DbDataOperation();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] LoveModel love) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateLove(love);
            return CreatedAtAction("GetLove", new { id = love.Id }, love);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{userid}/{filmid}")]
        public IActionResult Delete([FromRoute] int userid, int filmid) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteLove(userid, filmid);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IEnumerable<LoveModel> GetAllLovesOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllLovesOfUser(id);
        }
    }
}
