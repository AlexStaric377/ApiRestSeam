using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRestSeam.Models;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/SobController")]
    [ApiController]
    public class SobController : ControllerBase
    {
        DbContextSeam db;
        public SobController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<SobController>
        [HttpGet]
        public async Task<ActionResult<Sob>> Get()
        {

            List<Sob> _detailing = await db.Sobs.OrderBy(x => x.Pind).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<SobController>/5
        [HttpGet("{KodObl}/{Id}/{PoiskObl}/{Pind}")]
        public async Task<ActionResult<Sob>> Get(string KodObl, string Id, string PoiskObl, string Pind)
        {
            
            if (KodObl.Trim() == "0" && Id.Trim() == "0" && PoiskObl.Trim() == "0" && Pind.Trim() == "0") { return NotFound(); }
            List<Sob> _listdetailing = new List<Sob>();
            if (Pind.Trim() != "0")
            {
                
                _listdetailing = await db.Sobs.Where(x => x.Pind.Contains(Pind)).ToListAsync();
                return Ok(_listdetailing);
            }
            if (PoiskObl.Trim() != "0")
            { 
                _listdetailing = await db.Sobs.Where(x => x.NameObl.Contains(PoiskObl)).ToListAsync();
                return Ok(_listdetailing);
            } 
            if (Id.Trim() != "0")
            {
                Sob _detailing = await db.Sobs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(Id));
                return Ok(_detailing);
            }
            else
            {
                _listdetailing = await db.Sobs.Where(x => x.KodObl == KodObl).OrderBy(x => x.KodObl).ToListAsync();
                return Ok(_listdetailing);
            }
        }

        // POST api/<SobController>
        [HttpPost]
        public async Task<ActionResult<Sob>> Post(Sob _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Sobs.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Sobs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<SobController>/5
        [HttpPut]
        public async Task<ActionResult<Sob>> Put(Sob _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Sobs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Sobs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
        }

        // DELETE api/<SobController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sob>> Delete(string id)
        {
            Sob _detailing = new Sob();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Sobs.ToListAsync();
                if (_compl != null)
                { 
                    foreach (Sob str in _compl)
                    {
                        _detailing = await db.Sobs.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_detailing != null)
                        {
                            db.Sobs.Remove(_detailing);
                            await db.SaveChangesAsync();
                        }
                    }                
                }

                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.Sobs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.Sobs.Remove(_detailing);
                    await db.SaveChangesAsync();
					_detailing.Id = 0;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Sobs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
