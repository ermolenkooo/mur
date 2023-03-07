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
    public class FollowingsController : ControllerBase 
    {
        IDbCrud crudServ;
        public FollowingsController()
        {
            crudServ = new DbDataOperation();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] FollowingModel following) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateFollowing(following);
            return CreatedAtAction("GetFollowing", new { id = following.Id }, following);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteFollowing(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IEnumerable<FollowingModel> GetFollowingsOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllFollowings(id);
        }

        [HttpGet("{id}")]
        public IEnumerable<FollowingModel> GetFollowersOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllFollowers(id);
        }
    }
}
