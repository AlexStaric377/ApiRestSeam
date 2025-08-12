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
    [Route("api/PacientMapAnalizController")]
    [ApiController]
    public class PacientMapAnalizController : ControllerBase
    {
        DbContextSeam db;
        public PacientMapAnalizController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<PacientMapAnalizController>
        [HttpGet]
        public async Task<ActionResult<PacientMapAnaliz>> Get()
        {
            List<PacientMapAnaliz> _detailing = await db.PacientMapAnalizs.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<PacientMapAnalizController>/5
        [HttpGet("{KodPacient}/{DateAnaliza}")]
        public async Task<ActionResult<PacientMapAnaliz>> Get(string KodPacient, string DateAnaliza)
        {
            List<PacientMapAnaliz> _detailing = new List<PacientMapAnaliz>();
            if (KodPacient.Trim() == "0")
            {
                //if (DateAnaliza == null) { return NotFound(); }
                _detailing = await db.PacientMapAnalizs.Where(x => x.DateAnaliza == DateAnaliza).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            else
            {
                //_detailing = await db.PacientMapAnalizs.FirstOrDefaultAsync(x => x.KodPacient == KodPacient);
                _detailing = await db.PacientMapAnalizs.Where(x => x.KodPacient == KodPacient).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            return Ok(_detailing);

        }

        // POST api/<PacientMapAnalizController>
        [HttpPost]
        public async Task<ActionResult<PacientMapAnaliz>> Post(PacientMapAnaliz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.PacientMapAnalizs.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientMapAnalizs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<PacientMapAnalizController>/5
        [HttpPut]
        public async Task<ActionResult<PacientMapAnaliz>> Put(PacientMapAnaliz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.PacientMapAnalizs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientMapAnalizs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<PacientMapAnalizController>/5
        [HttpDelete("{id}/{KodPacienta}")]
        public async Task<ActionResult<PacientMapAnaliz>> Delete(string id, string KodPacienta)
        {
            if (KodPacienta.Trim() == "0" && id == "0") { return NotFound(); }
            PacientMapAnaliz _detailing = new PacientMapAnaliz();
            try
            {
                if (id == "0")
                {
                    _detailing = await db.PacientMapAnalizs.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.PacientMapAnalizs.RemoveRange(db.PacientMapAnalizs.Where(x => x.KodPacient == KodPacienta));
                    }
                }
                else
                {
                    if (Convert.ToInt32(id) == -1)
                    {
                        var _compl = await db.PacientMapAnalizs.ToListAsync();
                        foreach (PacientMapAnaliz str in _compl)
                        {
                            _detailing = await db.PacientMapAnalizs.FirstOrDefaultAsync(x => x.Id == str.Id);
                            if (_detailing != null)
                            {
                                db.PacientMapAnalizs.Remove(_detailing);
                                await db.SaveChangesAsync();
                            }
                        }
                        _detailing.Id = 0;
                    }
                    else
                    {
                        _detailing = await db.PacientMapAnalizs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        db.PacientMapAnalizs.Remove(_detailing);
                    }
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Convert.ToInt32(id) > 0)
                {

                    if (db.PacientMapAnalizs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }
                else
                {
                    if (db.PacientMapAnalizs.Any(x => x.KodPacient == _detailing.KodPacient)) { return BadRequest(); }
                }
            }
            return Ok(_detailing);
        }
    }
}
