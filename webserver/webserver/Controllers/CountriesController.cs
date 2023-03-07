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
    public class CountriesController : ControllerBase 
    {
        IDbCrud crudServ;
        public CountriesController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry([FromRoute] int id) //получение по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = crudServ.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpGet]
        public IEnumerable<CountryModel> GetAllCountries() //получение списка
        {
            return crudServ.GetAllCountries();
        }
    }
}
