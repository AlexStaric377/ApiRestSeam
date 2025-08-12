using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/GroupQualificationController")]
    [ApiController]
    public class GroupQualificationController : ControllerBase
    {
        DbContextSeam db;
        public GroupQualificationController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<GroupQualificationController>
        [HttpGet]
        public async Task<ActionResult<ListGroupQualification>> Get()
        {
            List<ListGroupQualification> _detailing = await db.ListGroupQualifications.OrderBy(x => x.KodGroupQualification).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<GroupQualificationController>/5
        [HttpGet("{KodGroupQualification}/{PoiskGroupQualification}")]
        public async Task<ActionResult<ListGroupQualification>> Get(string KodGroupQualification, string PoiskGroupQualification)
        {

            if (KodGroupQualification.Trim() == "0" && PoiskGroupQualification.Trim() == "0") { return NotFound(); }
            ListGroupQualification _detailing = new ListGroupQualification();
            if (PoiskGroupQualification.Trim() != "0")
            {
                List<ListGroupQualification> _listGroupQualification = await db.ListGroupQualifications.Where(x => x.NameGroupQualification.Contains(PoiskGroupQualification) == true).ToListAsync();
                return Ok(_listGroupQualification);
            }
            if (KodGroupQualification.Trim() != "0")_detailing = await db.ListGroupQualifications.FirstOrDefaultAsync(x => x.KodGroupQualification == KodGroupQualification);  
            return Ok(_detailing); ;

        }

        // POST api/<GroupQualificationController>
        [HttpPost]
        public async Task<ActionResult<ListGroupQualification>> Post(ListGroupQualification _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.ListGroupQualifications.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ListGroupQualifications.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
        }

        // PUT api/<GroupQualificationController>/5
        [HttpPut]
        public async Task<ActionResult<ListGroupQualification>> Put(ListGroupQualification _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.ListGroupQualifications.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ListGroupQualifications.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<GroupQualificationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ListGroupQualification>> Delete(string id)
        {
            ListGroupQualification _detailing = new ListGroupQualification();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.ListGroupQualifications.ToListAsync();
                foreach (ListGroupQualification str in _compl)
                {
                    _detailing = await db.ListGroupQualifications.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.ListGroupQualifications.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                _detailing = await db.ListGroupQualifications.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.ListGroupQualifications.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.ListGroupQualifications.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }            
            }

            return Ok(_detailing);
        }
    }
}
