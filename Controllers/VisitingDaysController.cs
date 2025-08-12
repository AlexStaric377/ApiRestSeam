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
    [Route("api/VisitingDaysController")]
    [ApiController]
    public class VisitingDaysController : ControllerBase
    {
        DbContextSeam db;
        public VisitingDaysController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }



        // GET: api/<VisitingDaysController>
        [HttpGet]
        public async Task<ActionResult<VisitingDays>> Get()
        {
            List<VisitingDays> _detailing = await db.VisitingDayss.OrderBy(x => x.KodDoctor).ToListAsync(); //.OrderBy(x => x.DateVizita)
            return Ok(_detailing);

        }

        // GET api/<VisitingDaysController>/5
        [HttpGet("{KodDoctor}/{DateVizita}")]
        public async Task<ActionResult<VisitingDays>> Get(string KodDoctor, string DateVizita = "0")
        {
            List<VisitingDays> _detailing = new List<VisitingDays>();
            if (KodDoctor.Trim() == "0") { return NotFound(); }
            if (DateVizita.Trim() != "0")
            {
 
                _detailing = await db.VisitingDayss.Where(x => x.DateVizita == DateVizita && x.KodDoctor == KodDoctor).OrderBy(x => x.TimeVizita).ToListAsync();
            }
            else
            {
                _detailing = await db.VisitingDayss.Where(x => x.KodDoctor == KodDoctor).OrderBy(x => x.DateVizita).ToListAsync();
            }
            return Ok(_detailing);

        }
        // POST api/<VisitingDaysController>
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

        // PUT api/<VisitingDaysController>/5
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

        // DELETE api/<VisitingDaysController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitingDays>> Delete(string id)
        {

            VisitingDays _detailing = new VisitingDays();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.VisitingDayss.ToListAsync();
                if (_compl != null)
                {
                    foreach (VisitingDays str in _compl)
                    {
                        _detailing = await db.VisitingDayss.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_detailing != null)
                        {
                            db.VisitingDayss.Remove(_detailing);
                            await db.SaveChangesAsync();
                        }
                    }
                }

                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.VisitingDayss.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.VisitingDayss.Remove(_detailing);
                    await db.SaveChangesAsync();
                    _detailing.Id = 0;
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
