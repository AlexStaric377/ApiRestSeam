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
    [Route("api/LifePacientController")]
    [ApiController]
    public class LifePacientController : ControllerBase
    {
        DbContextSeam db;
        public LifePacientController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<LifePacientController>
        [HttpGet]
        public async Task<ActionResult<LifePacient>> Get()
        {
            List<LifePacient> _detailing = await db.LifePacients.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<LifePacientController>/5
        [HttpGet("{KodPacient}/{ KodDoctor}")]
        public async Task<ActionResult<LifePacient>> Get(string KodPacient, string KodDoctor)
        {
            LifePacient _detailing = new LifePacient();
            if (KodPacient.Trim() == "0")
            {
                if (KodDoctor.Trim() == "0") { return NotFound(); }
                _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctor);
            }
            else
            {
                _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.KodPacient == KodPacient);
            }
            return Ok(_detailing);

        }

        // POST api/<LifePacientController>
        [HttpPost]
        public async Task<ActionResult<LifePacient>> Post(LifePacient _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.LifePacients.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LifePacients.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<LifePacientController>/5
        [HttpPut]
        public async Task<ActionResult<LifePacient>> Put(LifePacient _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.LifePacients.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LifePacients.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<LifePacientController>/5
        [HttpDelete("{id}/{KodPacienta}/{KodDoctora}")]
        public async Task<ActionResult<LifePacient>> Delete(string id, string KodPacienta, string KodDoctora)
        {
            if (KodPacienta.Trim() == "0" && id == "0") { return NotFound(); }
            LifePacient _detailing = new LifePacient();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.LifePacients.ToListAsync();
                foreach (LifePacient str in _compl)
                {
                    _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    { 
                       db.LifePacients.Remove(_detailing);
                       await db.SaveChangesAsync();                    
                    }
 
                }

                _detailing.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                { 
                    _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_detailing == null) { return NotFound(); }
                    try
                    {
                        db.LifePacients.Remove(_detailing);
                        
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                            if (db.LifePacients.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                    }
                }
                if (KodPacienta.Trim() != "0")
                {

                    _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.LifePacients.RemoveRange(db.LifePacients.Where(x => x.KodPacient == KodPacienta));
                        await db.SaveChangesAsync();
                    }
                    if (KodDoctora.Trim() != "0")
                    {
                        _detailing = await db.LifePacients.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                        if (_detailing != null) db.LifePacients.RemoveRange(db.LifePacients.Where(x => x.KodDoctor == KodDoctora));
                    }
                }
                await db.SaveChangesAsync();
            }
            return Ok(_detailing);
        }
    }
}
