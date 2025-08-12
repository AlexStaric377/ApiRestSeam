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
    [Route("api/ApiControllerVisitingDays")]
    [ApiController]
    public class ApiControllerVisitingDays : ControllerBase
    {
        DbContextSeam db;
        public ApiControllerVisitingDays(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<ApiControllerVisitingDays>
        [HttpGet]
        public async Task<ActionResult<VisitingDays>> Get()
        {
            List<VisitingDays> _detailing = await db.VisitingDayss.OrderBy(x => x.KodDoctor).OrderBy(x => x.DateVizita).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<ApiControllerVisitingDays>/5
        [HttpGet("{KodDoctor}/{DataVisita}")]
        public async Task<ActionResult<VisitingDays>> Get(string KodDoctor ="", string DataVisita ="")
        {
            List<VisitingDays> _Days = new List<VisitingDays>();
            if (KodDoctor != "0" && DataVisita == "0")
            {
                _Days = await db.VisitingDayss.Where(x => x.KodDoctor == KodDoctor).ToListAsync(); 
                    
            }
            if (KodDoctor != "0" && DataVisita != "0")
            {
                _Days = await db.VisitingDayss.Where(x => x.KodDoctor == KodDoctor && x.DateVizita.Contains(DataVisita) == true).ToListAsync();
                
            }
            return Ok(_Days);

        }

        // POST api/<ApiControllerVisitingDays>
        [HttpPost]
        public async Task<ActionResult<VisitingDays>> Post(VisitingDays _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.VisitingDayss.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.VisitingDayss.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<ApiControllerVisitingDays>/5
        [HttpPut]
        public async Task<ActionResult<VisitingDays>> Put(VisitingDays _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.VisitingDayss.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.VisitingDayss.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<ApiControllerVisitingDays>/5
        [HttpDelete("{id}/{KodDoctor}/{DataVisita}")]
        public async Task<ActionResult<VisitingDays>> Delete(string id, string KodDoctor="", string DataVisita = "")
        {
            VisitingDays _detailing = new VisitingDays();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.VisitingDayss.ToListAsync();
                foreach (VisitingDays str in _compl)
                {
                    _detailing = await db.VisitingDayss.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.VisitingDayss.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                if (KodDoctor != "" && DataVisita == "")
                {
                    
                    List<VisitingDays> _listdoctor = await db.VisitingDayss.Where(x => x.KodDoctor == KodDoctor).ToListAsync();
                    db.VisitingDayss.RemoveRange(_listdoctor);
                    await db.SaveChangesAsync();
                    return Ok(_listdoctor);
                }

                if (KodDoctor != "" && DataVisita != "")
                {
                    List<VisitingDays> _listdoctor = await db.VisitingDayss.Where(x => x.KodDoctor == KodDoctor && x.DateVizita.Contains(DataVisita) == true).ToListAsync();
                    db.VisitingDayss.RemoveRange(_listdoctor);
                    await db.SaveChangesAsync();
                    return Ok(_listdoctor);
                }

                _detailing = await db.VisitingDayss.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.VisitingDayss.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.VisitingDayss.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
