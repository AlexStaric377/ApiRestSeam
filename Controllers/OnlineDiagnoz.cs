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
    [Route("api/OnlineDiagnozController")]
    [ApiController]
    public class OnlineDiagnozController : ControllerBase
    {
        DbContextSeam db;

        public OnlineDiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<OnlineDiagnoz>
        [HttpGet]
        public async Task<ActionResult<ColectionInterview>> Get()
        {
            List<ColectionInterview> _doctor = await db.ColectionInterviews.ToListAsync();
            return Ok(_doctor);
        }

        // GET api/<OnlineDiagnoz>/5
        [HttpGet("{KodProtokola}")]
        public async Task<ActionResult<ColectionInterview>> Get(string KodProtokola)
        {

            if (KodProtokola.Trim().Length == 0) { return NotFound(); }
            ColectionInterview _doctor = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.KodProtokola == KodProtokola);
            return Ok(_doctor);
        }

        // POST api/<OnlineDiagnoz>
        [HttpPost]
        public async Task<ActionResult<ColectionInterview>> Post(ColectionInterview _doctor)
        {
            if (_doctor == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.ColectionInterviews.Add(_doctor);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ColectionInterviews.Any(x => x.Id != _doctor.Id)) { return NotFound(); }

            }
            return Ok(_doctor);
        }

        // PUT api/<OnlineDiagnoz>/5
        [HttpPut]
        public async Task<ActionResult<ColectionInterview>> Put(ColectionInterview _doctor)
        {

            if (_doctor == null) { return BadRequest(); }
            try
            {
                db.ColectionInterviews.Update(_doctor);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ColectionInterviews.Any(x => x.Id != _doctor.Id)) { return NotFound(); }

            }
            return Ok(_doctor);
        }

        // DELETE api/<OnlineDiagnoz>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ColectionInterview>> Delete(string id)
        {
            ColectionInterview _doctor = new ColectionInterview();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.ColectionInterviews.ToListAsync();
                if (_compl != null)
                {
                    foreach (ColectionInterview str in _compl)
                    {
                        _doctor = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_doctor != null)
                        {
                            db.ColectionInterviews.Remove(_doctor);
                            await db.SaveChangesAsync();
                        }
                    }
                }

                _doctor.Id = 0;
            }
            else
            {
                _doctor = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_doctor == null) { return NotFound(); }
                try
                {
                    db.ColectionInterviews.Remove(_doctor);
                    await db.SaveChangesAsync();
                    _doctor.Id = 0;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.ColectionInterviews.Any(x => x.Id == _doctor.Id)) { return BadRequest(); }

                }
            }

            return Ok(_doctor);
        }
    }
}
