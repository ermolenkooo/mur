using BLL.Interfaces;
using BLL.Models;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase //контроллер фильмов
    {
        IDbCrud crudServ;
        public UsersController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id) //получение по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = crudServ.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] UserModel user) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateUser(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserModel user) //обновление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = crudServ.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = user.Name;
            item.Email = user.Email;
            item.Photo = user.Photo;
            item.Password = user.Password;
            crudServ.UpdateUser(item);
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
            crudServ.DeleteUser(id);
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<UserModel> GetAllUsers() //получение списка
        {
            return crudServ.GetAllUsers();
        }
    }
}
