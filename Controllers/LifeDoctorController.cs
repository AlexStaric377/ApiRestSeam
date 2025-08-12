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
    [Route("api/LifeDoctorController")]
    [ApiController]
    public class LifeDoctorController : ControllerBase
    {
        DbContextSeam db;
        public LifeDoctorController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<LifeDoctorController>
        [HttpGet]
        public async Task<ActionResult<LifeDoctor>> Get()
        {
            List<LifeDoctor> _detailing = await db.LifeDoctors.OrderBy(x => x.KodDoctor).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<LifeDoctorController>/5
        [HttpGet("{KodDoctor}/{ KodPacient}")]
        public async Task<ActionResult<LifeDoctor>> Get(string KodDoctor, string KodPacient)
        {
            LifeDoctor _detailing = new LifeDoctor();
            if (KodPacient.Trim() == "0")
            {
                if (KodDoctor.Trim() == "0") { return NotFound(); }
                _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctor);
            }
            else
            {
                _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.KodPacient == KodPacient);
            }
            return Ok(_detailing);

        }

        // POST api/<LifeDoctorController>
        [HttpPost]
        public async Task<ActionResult<LifeDoctor>> Post(LifeDoctor _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.LifeDoctors.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LifeDoctors.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<LifeDoctorController>/5
        [HttpPut]
        public async Task<ActionResult<LifeDoctor>> Put(LifeDoctor _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.LifeDoctors.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LifeDoctors.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<LifeDoctorController>/5
        [HttpDelete("{id}/{KodPacienta}/{KodDoctora}")]
        public async Task<ActionResult<LifeDoctor>> Delete(string id, string KodPacienta, string KodDoctora)
        {
            if (id == "0" && KodDoctora.Trim() == "0" && KodPacienta.Trim() == "0") { return NotFound(); }
            
            LifeDoctor _detailing = new LifeDoctor();
            try
            {

                if (Convert.ToInt32(id) == -1)
                {
                    var _compl = await db.LifeDoctors.ToListAsync();
                    foreach (LifeDoctor str in _compl)
                    {
                        _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_detailing != null)
                        {
                            db.LifeDoctors.Remove(_detailing);
                            await db.SaveChangesAsync();
                        }
                    }
                    _detailing.Id = 0;
                }
                else
                {
                    if (Convert.ToInt32(id) > 0)
                    {
                        _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        if (_detailing != null)
                        { 
                             db.LifeDoctors.Remove(_detailing);
                        }
                    }
                    if (KodDoctora.Trim() != "0")
                    {
                        _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                        if (_detailing != null) db.LifeDoctors.RemoveRange(db.LifeDoctors.Where(x => x.KodDoctor == KodDoctora));
                    }
                    if (KodPacienta.Trim() != "0")
                    {
                        _detailing = await db.LifeDoctors.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                        if (_detailing != null)
                        {
                            db.LifeDoctors.RemoveRange(db.LifeDoctors.Where(x => x.KodPacient == KodPacienta));
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LifeDoctors.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

            }
            
            return Ok(_detailing);
        }
    }
}
