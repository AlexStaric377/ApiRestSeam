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
    [Route("api/ControllerListGroupDetail")]
    [ApiController]
    public class ControllerListGroupDetail : ControllerBase
    {
        DbContextSeam db;
        public ControllerListGroupDetail(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<ControllerListGroupDetail>
        [HttpGet]
        public async Task<ActionResult<ListGrDetailing>> Get()
        {
            List<ListGrDetailing> _grDetailing = await db.ListGrDetailings.OrderBy(x => x.KeyGrDetailing).ToListAsync();

            //var _grDetailing = new JsonResult(await db.ListGrDetailings.ToListAsync());
            return Ok(_grDetailing);
        }

        // GET api/<ControllerListGroupDetail>/5
        [HttpGet("{KeyGrDetailing}/{PoiskGrDetailing}")]
        public async Task<ActionResult<ListGrDetailing>> Get(string KeyGrDetailing, string PoiskGrDetailing)
        {

            if (KeyGrDetailing.Trim() == "0" && PoiskGrDetailing.Trim() == "0") { return NotFound(); }
            if (PoiskGrDetailing != "0")
            {
                List<ListGrDetailing> _listdetailing = new List<ListGrDetailing>();
                _listdetailing = await db.ListGrDetailings.Where(x => x.NameGrup.Contains(PoiskGrDetailing) == true).ToListAsync();
                return Ok(_listdetailing);
            }
            ListGrDetailing _grDetailing = await db.ListGrDetailings.FirstOrDefaultAsync(x => x.KeyGrDetailing == KeyGrDetailing);
            return Ok(_grDetailing);

        }

        // POST api/<ControllerListGroupDetail>
        [HttpPost]
        public async Task<ActionResult<ListGrDetailing>> Post(ListGrDetailing _grDetailing)
        {
            if (_grDetailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.ListGrDetailings.Add(_grDetailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ListGrDetailings.Any(x => x.Id != _grDetailing.Id)) { return NotFound(); }
            }
            return Ok(_grDetailing);
        }

        // PUT api/<ControllerListGroupDetail>/5
        [HttpPut]
        public async Task<ActionResult<ListGrDetailing>> Put(ListGrDetailing _grDetailing)
        {

            if (_grDetailing == null) { return BadRequest(); }
            try
            {
                db.ListGrDetailings.Update(_grDetailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ListGrDetailings.Any(x => x.Id != _grDetailing.Id)) { return NotFound(); }
            }
            return Ok(_grDetailing);

        }

        // DELETE api/<ControllerListGroupDetail>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ListGrDetailing>> Delete(string id)
        {
            ListGrDetailing _grDetailing = new ListGrDetailing();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.ListGrDetailings.ToListAsync();
                foreach (ListGrDetailing str in _compl)
                {
                    _grDetailing = await db.ListGrDetailings.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_grDetailing != null)
                    {
                        db.ListGrDetailings.Remove(_grDetailing);
                        await db.SaveChangesAsync();
                    }
                }
                _grDetailing.Id = 0;
            }
            else
            { 
                _grDetailing = await db.ListGrDetailings.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_grDetailing == null) { return NotFound(); }
                try
                {
                    db.ListGrDetailings.Remove(_grDetailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.ListGrDetailings.Any(x => x.Id == _grDetailing.Id)) { return BadRequest(); }
                }            
            }

            return Ok(_grDetailing);
        }
    }
}
