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
    public class AdminsController : ControllerBase
    {
        IDbCrud crudServ;
        public AdminsController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetAdmin([FromRoute] int id) //получение по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var admin = crudServ.GetAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] AdminModel admin) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateAdmin(admin);
            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteAdmin(id);
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<AdminModel> GetAllAdmins() //получение списка
        {
            return crudServ.GetAllAdmins();
        }
    }
}
