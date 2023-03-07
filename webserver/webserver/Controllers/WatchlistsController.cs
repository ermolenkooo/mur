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
    public class WatchlistsController : ControllerBase
    {
        IDbCrud crudServ;
        public WatchlistsController()
        {
            crudServ = new DbDataOperation();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] WatchlistModel watchlist) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.CreateWatchlist(watchlist);
            return CreatedAtAction("GetWatchlist", new { id = watchlist.Id }, watchlist);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id) //удаление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            crudServ.DeleteWatchlist(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IEnumerable<WatchlistModel> GetAllWatchlistsOfUser([FromRoute] int id) //получение списка
        {
            return crudServ.GetAllWatchlistsOfUser(id);
        }
    }
}
