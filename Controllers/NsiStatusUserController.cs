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
    [Route("api/NsiStatusUserController")]
    [ApiController]
    public class NsiStatusUserController : ControllerBase
    {

        DbContextSeam db;
        public NsiStatusUserController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
           
        }

        // GET: api/<NsiStatusUserController>
        [HttpGet]
        public async Task<ActionResult<NsiStatusUser>> Get()
        {
            List<NsiStatusUser> _detailing = await db.NsiStatusUsers.ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<NsiStatusUserController>/5
        [HttpGet("{IdStatus}")]
        public async Task<ActionResult<NsiStatusUser>> Get(string IdStatus)
        {

            if (IdStatus.Trim().Length == 0) { return NotFound(); }
            NsiStatusUser _detailing = await db.NsiStatusUsers.FirstOrDefaultAsync(x => x.IdStatus == IdStatus);
            return Ok(_detailing);

        }

        // POST api/<NsiStatusUserController>
        [HttpPost]
        public async Task<ActionResult<NsiStatusUser>> Post(NsiStatusUser _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.NsiStatusUsers.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.NsiStatusUsers.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<NsiStatusUserController>/5
        [HttpPut]
        public async Task<ActionResult<NsiStatusUser>> Put(NsiStatusUser _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.NsiStatusUsers.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.NsiStatusUsers.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<NsiStatusUserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NsiStatusUser>> Delete(string id)
        {
            NsiStatusUser _detailing = new NsiStatusUser();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.NsiStatusUsers.ToListAsync();
                foreach (NsiStatusUser str in _compl)
                {
                    _detailing = await db.NsiStatusUsers.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.NsiStatusUsers.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                 _detailing = await db.NsiStatusUsers.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.NsiStatusUsers.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.NsiStatusUsers.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }           
            }
 
            return Ok(_detailing);
        }
    }
}
