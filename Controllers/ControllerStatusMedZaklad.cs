using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/ControllerStatusMedZaklad")]
    [ApiController]
    public class ControllerStatusMedZaklad : ControllerBase
    {
        DbContextSeam db;

        public ControllerStatusMedZaklad(DbContextSeam context)
        {
            db = context;
            if (!db.StatusMedZaklads.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }

        // GET: api/<ControllerStatusMedZaklad>
        [HttpGet]
        public async Task<ActionResult<StatusMedZaklad>> Get()
        {
            List<StatusMedZaklad> _price = await db.StatusMedZaklads.OrderBy(x => x.IdStatus).ToListAsync();
            return Ok(_price);
        }

        // GET api/<ControllerStatusMedZaklad>/5
        [HttpGet("{IdStatus}")]

        public async Task<ActionResult<StatusMedZaklad>> Get(string IdStatus = "")
        {
            StatusMedZaklad _idstatus = new StatusMedZaklad();
            if (IdStatus == "0" || IdStatus == "") return NotFound();

            if (IdStatus != "0")_idstatus = await db.StatusMedZaklads.FirstOrDefaultAsync(x => x.IdStatus == IdStatus);
            return Ok(_idstatus);

        }

        // POST api/<ControllerStatusMedZaklad>
        [HttpPost]
        public async Task<ActionResult<StatusMedZaklad>> Post(StatusMedZaklad _idstatus)
        {
            if (_idstatus == null) { return BadRequest(); }
            // Создание новой записи об оплате
            try
            {
                db.StatusMedZaklads.Add(_idstatus);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.StatusMedZaklads.Any(x => x.Id != _idstatus.Id)) { return NotFound(); }
            }
            return Ok(_idstatus);
        }

        // PUT api/<ControllerStatusMedZaklad>/5
        [HttpPut]
        public async Task<ActionResult<StatusMedZaklad>> Put(StatusMedZaklad _idstatus)
        {
            if (_idstatus == null) { return BadRequest(); }
            try
            {
                db.StatusMedZaklads.Update(_idstatus);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.StatusMedZaklads.Any(x => x.Id != _idstatus.Id)) { return NotFound(); }
            }
            return Ok(_idstatus);

        }

        // DELETE api/<ControllerStatusMedZaklad>/5
        [HttpDelete("{id}/{IdStatus}")]
        public async Task<ActionResult<StatusMedZaklad>> Delete(string id, string IdStatus)
        {
            StatusMedZaklad _idstatus = new StatusMedZaklad();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.StatusMedZaklads.ToListAsync();
                foreach (StatusMedZaklad str in _compl)
                {
                    _idstatus = await db.StatusMedZaklads.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_idstatus != null)
                    {
                        db.StatusMedZaklads.Remove(_idstatus);
                        await db.SaveChangesAsync();
                    }

                }
                _idstatus.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                {
                    _idstatus = await db.StatusMedZaklads.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_idstatus == null) { return NotFound(); }
                    try
                    {
                        db.StatusMedZaklads.Remove(_idstatus);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.StatusMedZaklads.Any(x => x.Id == _idstatus.Id)) { return BadRequest(); }
                    }
                }
                if (IdStatus.Trim() != "0")
                {
                    var _compl = await db.StatusMedZaklads.Where(x => x.IdStatus == IdStatus).ToListAsync();
                    foreach (StatusMedZaklad str in _compl)
                    {
                        _idstatus = await db.StatusMedZaklads.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_idstatus != null) db.StatusMedZaklads.Remove(_idstatus);
                        await db.SaveChangesAsync();
                    }
                    _idstatus.Id = 0;
                }

            }
            return Ok(_idstatus);
        }
    }
}
