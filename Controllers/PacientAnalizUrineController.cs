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
    [Route("api/PacientAnalizUrineController")]
    [ApiController]
    public class PacientAnalizUrineController : ControllerBase
    {

        DbContextSeam db;
        public PacientAnalizUrineController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<PacientAnalizUrineController>
        [HttpGet]
        public async Task<ActionResult<PacientAnalizUrine>> Get()
        {
            List<PacientAnalizUrine> _detailing = await db.PacientAnalizUrines.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<PacientAnalizUrineController>/5
        [HttpGet("{KodPacient}/{DateAnaliza}")]
        public async Task<ActionResult<PacientAnalizUrine>> Get(string KodPacient, string DateAnaliza)
        {
            List<PacientAnalizUrine> _detailing = new List<PacientAnalizUrine>();
            if (KodPacient.Trim() == "0")
            {
                //if (DateAnaliza == null) { return NotFound(); }
                _detailing = await db.PacientAnalizUrines.Where(x => x.DateAnaliza == DateAnaliza).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            else
            {
                //_detailing = await db.PacientMapAnalizs.FirstOrDefaultAsync(x => x.KodPacient == KodPacient);
                _detailing = await db.PacientAnalizUrines.Where(x => x.KodPacient == KodPacient).OrderBy(x => x.DateAnaliza).ToListAsync();
            }
            return Ok(_detailing);

        }

        // POST api/<PacientAnalizUrineController>
        [HttpPost]
        public async Task<ActionResult<PacientAnalizUrine>> Post(PacientAnalizUrine _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.PacientAnalizUrines.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientAnalizUrines.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<PacientAnalizUrineController>/5
        [HttpPut]
        public async Task<ActionResult<PacientAnalizUrine>> Put(PacientAnalizUrine _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.PacientAnalizUrines.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.PacientAnalizUrines.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<PacientAnalizUrineController>/5
        [HttpDelete("{id}/{KodPacienta}")]
        public async Task<ActionResult<PacientAnalizUrine>> Delete(string id, string KodPacienta)
        {
            if (KodPacienta.Trim() == "0" && id == "0") { return NotFound(); }
            PacientAnalizUrine _detailing = new PacientAnalizUrine();
            try
            {
                if (id == "0")
                {
                    _detailing = await db.PacientAnalizUrines.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.PacientAnalizUrines.RemoveRange(db.PacientAnalizUrines.Where(x => x.KodPacient == KodPacienta));
                    }
                }
                else
                {
                    if (Convert.ToInt32(id) == -1)
                    {
                        var _compl = await db.PacientAnalizUrines.ToListAsync();
                        foreach (PacientAnalizUrine str in _compl)
                        {
                            _detailing = await db.PacientAnalizUrines.FirstOrDefaultAsync(x => x.Id == str.Id);
                            if (_detailing != null)
                            {
                                db.PacientAnalizUrines.Remove(_detailing);
                                await db.SaveChangesAsync();
                            }
                        }
                        _detailing.Id = 0;
                    }
                    else
                    {
                        _detailing = await db.PacientAnalizUrines.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        if (_detailing != null) db.PacientAnalizUrines.Remove(_detailing);
                    }
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Convert.ToInt32(id) != 0)
                {
                    if (db.PacientAnalizUrines.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }
                else
                {
                    if (db.PacientAnalizUrines.Any(x => x.KodPacient == _detailing.KodPacient)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
