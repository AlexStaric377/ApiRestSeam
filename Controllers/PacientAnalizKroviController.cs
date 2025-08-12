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
    [Route("api/PacientAnalizKroviController")]
    [ApiController]
    public class PacientAnalizKroviController : ControllerBase
    {

        DbContextSeam db;
        public PacientAnalizKroviController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }


        // GET: api/<PacientAnalizKroviController>
        [HttpGet]
        public async Task<ActionResult<PacientAnalizKrovi>> Get()
        {
            List<PacientAnalizKrovi> _detailing = await db.PacientAnalizKrovis.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<PacientAnalizKroviController>/5
        [HttpGet("{KodPacient}/{DateAnaliza}")]
        public async Task<ActionResult<PacientAnalizKrovi>> Get(string KodPacient, string DateAnaliza)
        {
            List<PacientAnalizKrovi> _detailing = new List<PacientAnalizKrovi>();
            if (KodPacient.Trim() == "0")
            {
                _detailing = await db.PacientAnalizKrovis.Where(x => x.DateAnaliza == DateAnaliza).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            else
            {
                _detailing = await db.PacientAnalizKrovis.Where(x => x.KodPacient == KodPacient).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            return Ok(_detailing);

        }

        // POST api/<PacientAnalizKroviController>
        [HttpPost]
        public async Task<ActionResult<PacientAnalizKrovi>> Post(PacientAnalizKrovi _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.PacientAnalizKrovis.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientAnalizKrovis.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<PacientAnalizKroviController>/5
        [HttpPut]
        public async Task<ActionResult<PacientAnalizKrovi>> Put(PacientAnalizKrovi _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.PacientAnalizKrovis.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientAnalizKrovis.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<PacientAnalizKroviController>/5
        [HttpDelete("{id}/{KodPacienta}")]
        public async Task<ActionResult<PacientAnalizKrovi>> Delete(string id, string KodPacienta)
        {
            if (KodPacienta.Trim() == "0" && id == "0") { return NotFound(); }
            PacientAnalizKrovi _detailing = new PacientAnalizKrovi();
            try
            {
                if (id == "0")
                {
                    _detailing = await db.PacientAnalizKrovis.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.PacientAnalizKrovis.RemoveRange(db.PacientAnalizKrovis.Where(x => x.KodPacient == KodPacienta));
                    }
                }
                else
                {
                    if (Convert.ToInt32(id) == -1)
                    {
                        var _compl = await db.PacientAnalizKrovis.ToListAsync();
                        foreach (PacientAnalizKrovi str in _compl)
                        {
                            _detailing = await db.PacientAnalizKrovis.FirstOrDefaultAsync(x => x.Id == str.Id);
                            if (_detailing != null)
                            {
                                db.PacientAnalizKrovis.Remove(_detailing);
                                await db.SaveChangesAsync();
                            }
                        }
                        _detailing.Id = 0;
                    }
                    else
                    {
                        _detailing = await db.PacientAnalizKrovis.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        db.PacientAnalizKrovis.Remove(_detailing);
                    }
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Convert.ToInt32(id) != 0)
                {
                    if (db.PacientAnalizKrovis.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }
                else
                {
                    if (db.PacientAnalizKrovis.Any(x => x.KodPacient == _detailing.KodPacient)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
