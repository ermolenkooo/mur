using BLL.Interfaces;
using BLL.Models;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using DAL.Entities;
using System;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialsController : ControllerBase
    {
        IDbCrud crudServ;
        public SerialsController()
        {
            crudServ = new DbDataOperation();
        }

        [HttpGet("{id}")]
        public IActionResult GetSerial([FromRoute] int id) //получение по id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serial = crudServ.GetSerial(id);
            if (serial == null)
            {
                return NotFound();
            }
            return Ok(serial);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] SerialModel serial) //добавление нового
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string base64ImageRepresentation = "data:image/jpeg;base64,";
            byte[] imageArray = System.IO.File.ReadAllBytes(serial.Poster);
            base64ImageRepresentation += Convert.ToBase64String(imageArray);
            serial.Poster = base64ImageRepresentation;
            crudServ.CreateSerial(serial);
            return CreatedAtAction("GetSerial", new { id = serial.Id }, serial);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] SerialModel serial) //обновление
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = crudServ.GetSerial(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = serial.Name;
            item.Id_genre = serial.Id_genre;
            item.Id_country = serial.Id_country;
            item.Timing = serial.Timing;
            item.Description = serial.Description;
            string base64ImageRepresentation = "data:image/jpeg;base64,";
            byte[] imageArray = System.IO.File.ReadAllBytes(serial.Poster);
            base64ImageRepresentation += Convert.ToBase64String(imageArray);
            item.Poster = base64ImageRepresentation;
            item.Year = serial.Year;
            item.Age = serial.Age;
            item.Original = serial.Original;
            item.Mark = serial.Mark;
            item.Seasons = serial.Seasons;
            crudServ.UpdateSerial(item);
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
            crudServ.DeleteSerial(id);
            return NoContent();
        }

        [HttpGet]
        public IEnumerable<SerialModel> GetAllSerials() //получение списка
        {
            return crudServ.GetAllSerials();
        }
    }
}
