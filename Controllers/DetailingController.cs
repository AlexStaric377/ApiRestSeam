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
    [Route("api/DetailingController")]
    [ApiController]
    public class DetailingController : ControllerBase
    {
        DbContextSeam db;
        public DetailingController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<DetailingController>
        [HttpGet]
        public async Task<ActionResult<Detailing>>  Get()
        {
            List<Detailing> _detailing = await db.Detailings.OrderBy(x => x.KodDetailing).ToListAsync();
            //var _detailing = new JsonResult(await db.Detailings.ToListAsync());
            return Ok(_detailing);
            
        }
       
        // GET api/<DetailingController>/5
        [HttpGet("{KodDetailing}/{KeyFeature}/{PoiskNameDetailing}")]
        public async Task<ActionResult<Detailing>> Get(string KodDetailing, string KeyFeature, string PoiskNameDetailing)
        {
            if (KodDetailing.Trim() != "0")
            {
                Detailing _detailingk = await db.Detailings.FirstOrDefaultAsync(x => x.KodDetailing == KodDetailing);
                return Ok(_detailingk);
            }

            if (KeyFeature.Trim() != "0")
            {
                List<Detailing> _listdetailing = await db.Detailings.Where(x => x.KeyFeature == KeyFeature).OrderBy(x => x.KodDetailing).ToListAsync();
                return Ok(_listdetailing);
            }
            if (PoiskNameDetailing.Trim() != "0")
            {
                List<Detailing> _listdetailing = await db.Detailings.Where(x => x.NameDetailing.Contains(PoiskNameDetailing) == true).ToListAsync();
                return Ok(_listdetailing);
            }

            var _detailing = new JsonResult(await db.Detailings.OrderBy(x => x.KodDetailing).ToListAsync());
            return Ok(_detailing);

        }

        // POST api/<DetailingController>
        [HttpPost]
        public async Task<ActionResult<Detailing>> Post(Detailing _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Detailings.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Detailings.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
 
            return Ok(_detailing);
        }

        // PUT api/<DetailingController>/5
        [HttpPut]
        public async Task<ActionResult<Detailing>> Put( Detailing _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Detailings.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Detailings.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<DetailingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Detailing>> Delete(string id)
        {
            Detailing _detailing = new Detailing();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Detailings.ToListAsync();
                foreach (Detailing str in _compl)
                {
                    _detailing = await db.Detailings.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.Detailings.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.Detailings.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.Detailings.Remove(_detailing);
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Detailings.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
