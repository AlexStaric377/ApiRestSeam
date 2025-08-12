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
    [Route("api/QualificationController")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        DbContextSeam db;
        public QualificationController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<QualificationController>
        [HttpGet]
        public async Task<ActionResult<Qualification>> Get()
        {
            List<Qualification> _detailing = await db.Qualifications.OrderBy(x => x.KodQualification).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<QualificationController>/5
        [HttpGet("{KodQualification}/{KodGroupQualification}/{PoisknameQualification}")]
        public async Task<ActionResult<Qualification>> Get(string KodQualification, string KodGroupQualification, string PoisknameQualification)
        {
            if (KodQualification.Trim() == "0" && KodGroupQualification == "0" && PoisknameQualification == "0") { return NotFound(); }

            Qualification _detailing = new Qualification();
 
            if (KodGroupQualification != "0")
            {
                List<Qualification> _grdetailing = await db.Qualifications.Where(x => x.KodGroupQualification == KodGroupQualification).OrderBy(x => x.KodQualification).ToListAsync();
                return Ok(_grdetailing);
            }
            if (PoisknameQualification.Trim() != "0")
            {
                List<Qualification> _listGroupQualification = await db.Qualifications.Where(x => x.NameQualification.Contains(PoisknameQualification) == true).ToListAsync();
                return Ok(_listGroupQualification);
            }
            if (KodQualification.Trim() != "0") _detailing = await db.Qualifications.FirstOrDefaultAsync(x => x.KodQualification == KodQualification);
            return Ok(_detailing);
        }

        // POST api/<QualificationController>
        [HttpPost]
        public async Task<ActionResult<Qualification>> Post(Qualification _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Qualifications.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Qualifications.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
        }

        // PUT api/<QualificationController>/5
        [HttpPut]
        public async Task<ActionResult<Qualification>> Put(Qualification _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Qualifications.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Qualifications.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<QualificationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Qualification>> Delete(string id)
        {
            Qualification _detailing = new Qualification();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Qualifications.ToListAsync();
                foreach (Qualification str in _compl)
                {
                    _detailing = await db.Qualifications.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.Qualifications.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.Qualifications.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.Qualifications.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Qualifications.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }

            return Ok(_detailing);
        }
    }
}
