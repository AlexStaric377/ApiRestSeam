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
    [Route("api/GrupDiagnozController")]
    [ApiController]
    public class GrupDiagnozController : ControllerBase
    {


        DbContextSeam db;
        public GrupDiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<GrupDiagnozController>
        [HttpGet]
        public async Task<ActionResult<GrupDiagnoz>> Get()
        {
            List<GrupDiagnoz> _detailing = await db.GrupDiagnozs.OrderBy(x => x.IcdGrDiagnoz).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<GrupDiagnozController>/5
        
        [HttpGet("{IcdGrDiagnoz}/{PoiskGrDiagnoz}")]
        public async Task<ActionResult<GrupDiagnoz>> Get(string IcdGrDiagnoz, string PoiskGrDiagnoz)
        {

            if (IcdGrDiagnoz.Trim() == "0" && PoiskGrDiagnoz.Trim() == "0") { return NotFound(); }
            if (IcdGrDiagnoz.Trim() == "0")
            {
                List<GrupDiagnoz> _listGrupDiagnoz = await db.GrupDiagnozs.Where(x => x.NameGrDiagnoz.Contains(PoiskGrDiagnoz)).ToListAsync();
                return Ok(_listGrupDiagnoz);
            }
            GrupDiagnoz _detailing = await db.GrupDiagnozs.FirstOrDefaultAsync(x => x.IcdGrDiagnoz == IcdGrDiagnoz);
            return Ok(_detailing);

        }

        // POST api/<GrupDiagnozController>
        [HttpPost]
        public async Task<ActionResult<GrupDiagnoz>> Post(GrupDiagnoz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.GrupDiagnozs.Add(_detailing);
                await db.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.GrupDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<GrupDiagnozController>/5
        [HttpPut]
        public async Task<ActionResult<GrupDiagnoz>> Put(GrupDiagnoz _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.GrupDiagnozs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.GrupDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);

        }

        // DELETE api/<GrupDiagnozController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GrupDiagnoz>> Delete(string id)
        {
            GrupDiagnoz _detailing = new GrupDiagnoz();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.GrupDiagnozs.ToListAsync();
                foreach (GrupDiagnoz str in _compl)
                {
                    _detailing = await db.GrupDiagnozs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.GrupDiagnozs.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.GrupDiagnozs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.GrupDiagnozs.Remove(_detailing);
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.GrupDiagnozs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }

            return Ok(_detailing);
        }
    }
}
