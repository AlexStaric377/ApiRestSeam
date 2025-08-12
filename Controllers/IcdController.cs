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
    [Route("api/IcdController")]
    [ApiController]
    public class IcdController : ControllerBase
    {
        DbContextSeam db;
        public IcdController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<IcdController>
        [HttpGet]
        public async Task<ActionResult<Icd>> Get()
        {
            List<Icd> _detailing = await db.Icds.OrderBy(x => x.KeyIcd).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<IcdController>/5
        [HttpGet("{KeyIcd}/{NameGrIcd}")]
        public async Task<ActionResult<Icd>> Get(string KeyIcd, string NameGrIcd)
        {
            List<Icd> _listdetailing = new List<Icd>();
            if (KeyIcd.Trim().Length == 0 && NameGrIcd.Trim().Length == 0) { return NotFound(); }

            Icd _detailing = new Icd();
            if (KeyIcd != "0")
            {
                _listdetailing = await db.Icds.Where(x => x.KeyIcd.Contains(KeyIcd) == true).ToListAsync();
            }
            if (NameGrIcd != "0")
            {
                 _listdetailing = await db.Icds.Where(x => x.Name.Contains(NameGrIcd) == true ).ToListAsync();
            }
            return Ok(_listdetailing);


        }

        // POST api/<IcdController>
        [HttpPost]
        public async Task<ActionResult<Icd>> Post(Icd _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Icds.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Icds.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<IcdController>/5
        [HttpPut]
        public async Task<ActionResult<Icd>> Put(Icd _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Icds.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Icds.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<IcdController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Icd>> Delete(string id)
        {
            Icd _detailing = new Icd();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Icds.ToListAsync();
                foreach (Icd str in _compl)
                {
                    _detailing = await db.Icds.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.Icds.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                _detailing = await db.Icds.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.Icds.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Icds.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }           
            }

            return Ok(_detailing);
        }
    }
}
