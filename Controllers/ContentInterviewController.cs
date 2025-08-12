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
    [Route("api/ContentInterviewController")]
    [ApiController]
    public class ContentInterviewController : ControllerBase
    {
        DbContextSeam db;
        public ContentInterviewController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<ContentInterviewController>
        [HttpGet]
        public async Task<ActionResult<ContentInterv>> Get()
        {
            List<ContentInterv> _content = await db.ContentIntervs.OrderBy(x => x.KodProtokola).ToListAsync();
            return Ok(_content);
        }

        // GET api/<ContentInterviewController>/5
        [HttpGet("{KodProtokola}")]
        public async Task<ActionResult<ContentInterv>> Get(string KodProtokola)
        {
            if (KodProtokola.Trim().Length == 0) { return NotFound(); }
            List <ContentInterv>  _content = await db.ContentIntervs.Where(x => x.KodProtokola == KodProtokola).OrderBy(x => x.Numberstr).ToListAsync(); // .ThenBy(x => x.Numberstr)
            return Ok(_content);

        }

        // POST api/<ContentInterviewController>
        [HttpPost]
        public async Task<ActionResult<ContentInterv>> Post(ContentInterv _content)
        {
            if (_content == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.ContentIntervs.Add(_content);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ContentIntervs.Any(x => x.Id != _content.Id)) { return NotFound(); }
            }
            return Ok(_content);
        }

        // PUT api/<ContentInterviewController>/5
        [HttpPut]
        public async Task<ActionResult<ContentInterv>> Put(ContentInterv _content)
        {

            if (_content == null) { return BadRequest(); }
            try
            {
                db.ContentIntervs.Update(_content);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ContentIntervs.Any(x => x.Id != _content.Id)) { return NotFound(); }
            }
            return Ok(_content);

        }

        // DELETE api/<ContentInterviewController>/5
        [HttpDelete("{KodProtokola}/{id}")]
        public async Task<ActionResult<ContentInterv>> Delete(string KodProtokola, string id)
        {
            ContentInterv _content = new ContentInterv();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.ContentIntervs.ToListAsync();
                foreach (ContentInterv str in _compl)
                {
                    _content = await db.ContentIntervs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_content != null)
                    {
                        db.ContentIntervs.Remove(_content);
                        await db.SaveChangesAsync();
                    }
                }
                 _content.Id = 0;
            }
            else
            { 
                 _content = Convert.ToInt32(id) ==0 ? await db.ContentIntervs.FirstOrDefaultAsync(x => x.KodProtokola == KodProtokola):
                await db.ContentIntervs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));

                if (_content == null) { return NotFound(); }
                try
                {
                    if (Convert.ToInt32(id) == 0)
                    {
                        db.ContentIntervs.RemoveRange(db.ContentIntervs.Where(x => x.KodProtokola == KodProtokola));
                    }
                    else db.ContentIntervs.Remove(_content);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Convert.ToInt32(id) == 0)
                    {
                        if (db.ContentIntervs.Any(x => x.Id == _content.Id)) { return BadRequest(); }
                    }
                    else
                    {
                        if (db.ContentIntervs.Any(x => x.KodProtokola == _content.KodProtokola)) { return BadRequest(); }
                    }
                }           
            }
            return Ok(_content);
        }
    }
}
