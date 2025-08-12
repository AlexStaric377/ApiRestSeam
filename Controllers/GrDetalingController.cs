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
    [Route("api/GrDetalingController")]
    [ApiController]
    public class GrDetalingController : ControllerBase
    {
        DbContextSeam db;
        public GrDetalingController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<GrDetalingController>
        [HttpGet]
        public async Task<ActionResult<GrDetailing>> Get()
        {
            List<GrDetailing> _detailing = await db.GrDetailings.OrderBy(x => x.KodDetailing).ToListAsync();
            return Ok(_detailing);
        }
  
        // GET api/<GrDetalingController>/5
        [HttpGet("{KodDetailing}/{KeyGrDetailing}/{PoiskNameGrDetailing}")]
        public async Task<ActionResult<GrDetailing>> Get(string KodDetailing, string KeyGrDetailing, string PoiskNameGrDetailing)
        {
            if (KodDetailing.Trim() == "0" && KeyGrDetailing.Trim() == "0" && PoiskNameGrDetailing.Trim() == "0") { return NotFound(); }
            if (KeyGrDetailing.Trim() != "0")
            {
               List<GrDetailing> _listdetailing = await db.GrDetailings.Where(x => x.KeyGrDetailing == KeyGrDetailing).OrderBy(x => x.KodDetailing).ToListAsync();
               return Ok(_listdetailing);
 
            }
            if (KodDetailing.Trim() != "0")
            {
                GrDetailing _detailingk = await db.GrDetailings.FirstOrDefaultAsync(x => x.KodDetailing == KodDetailing);
                return Ok(_detailingk);
            }

            if (PoiskNameGrDetailing.Trim() != "0")
            {
                List<GrDetailing> _listdetailing = await db.GrDetailings.Where(x => x.NameGrDetailing.Contains(PoiskNameGrDetailing) == true).ToListAsync();
                return Ok(_listdetailing);
            }

            List<GrDetailing> _detailing = await db.GrDetailings.OrderBy(x => x.KodDetailing).ToListAsync();
            return Ok(_detailing);
        }

        // POST api/<GrDetalingController> создание новых  строк описания ргупповой детализации симптома
        [HttpPost]
        public async Task<ActionResult<GrDetailing>> Post(GrDetailing _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.GrDetailings.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.GrDetailings.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
        }

        // PUT api/<GrDetalingController>/5 модификация строк описания ргупповой детализации симптома
        [HttpPut]
        public async Task<ActionResult<GrDetailing>> Put( GrDetailing _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.GrDetailings.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.GrDetailings.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
 
        }

        // DELETE api/<GrDetalingController>/5 удаление строк описания ргупповой детализации симптома
        [HttpDelete("{id}")]
        public async Task<ActionResult<GrDetailing>> Delete(string id)
        {

            GrDetailing _detailing = new GrDetailing();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.GrDetailings.ToListAsync();
                foreach (GrDetailing str in _compl)
                {
                    _detailing = await db.GrDetailings.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.GrDetailings.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                _detailing = await db.GrDetailings.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.GrDetailings.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.GrDetailings.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }            
            }

            return Ok(_detailing);
        }
    }
}
