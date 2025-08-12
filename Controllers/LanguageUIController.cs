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
    [Route("api/LanguageUIController")]
    [ApiController]
    public class LanguageUIController : ControllerBase
    {
        DbContextSeam db;
        public LanguageUIController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<LanguageUIController>
        [HttpGet]
        public async Task<ActionResult<LanguageUI>> Get()
        {
            List<LanguageUI> _detailing = await db.LanguageUIs.OrderBy(x => x.KeyLang).ToListAsync();
            return Ok(_detailing); 
        }

        // GET api/<LanguageUIController>/5
        [HttpGet("{KeyLang}")]
        public async Task<ActionResult<LanguageUI>> Get(string KeyLang)
        {

            if (KeyLang.Trim().Length == 0) { return NotFound(); }
            LanguageUI _detailing = await db.LanguageUIs.FirstOrDefaultAsync(x => x.KeyLang == KeyLang);
            return Ok(_detailing); 

        }

        // POST api/<LanguageUIController>
        [HttpPost]
        public async Task<ActionResult<LanguageUI>> Post(LanguageUI _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.LanguageUIs.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LanguageUIs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<LanguageUIController>/5
        [HttpPut]
        public async Task<ActionResult<LanguageUI>> Put(LanguageUI _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.LanguageUIs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.LanguageUIs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }


        // DELETE api/<LanguageUIController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LanguageUI>> Delete(string id)
        {

            LanguageUI _detailing = new  LanguageUI();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.LanguageUIs.ToListAsync();
                foreach (LanguageUI str in _compl)
                {
                    _detailing = await db.LanguageUIs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.LanguageUIs.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.LanguageUIs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.LanguageUIs.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.LanguageUIs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }

            return Ok(_detailing);
        }
    }
}
