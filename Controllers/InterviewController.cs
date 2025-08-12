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
    [Route("api/InterviewController")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        DbContextSeam db;
        public InterviewController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<InterviewController>
        [HttpGet]
        public async Task<ActionResult<Interview>> GetInterview()
        {
            List<Interview> _interview = await db.Interviews.OrderBy(x => x.KodProtokola).ToListAsync();
            return Ok(_interview);
        }

        // GET api/<InterviewController>/5
        [HttpGet("{KodProtokola}/{DetailsInterview}/{NewProtokol}/{PoiskInterview}/{GrDetail}")]
        public async Task<ActionResult<Interview>> Get(string KodProtokola, string DetailsInterview, string NewProtokol, string PoiskInterview, string GrDetail) //, string NewProtokol
        {
            
            Interview _interview = new Interview();
            if (GrDetail.Trim() != "0")
            {
                List<Interview> _listnterview = await db.Interviews.Where(x => x.GrDetail.Contains(GrDetail)).ToListAsync();
                return Ok(_listnterview);
            }
            if (PoiskInterview.Trim() != "0")
            {
                List<Interview> _listnterview = await db.Interviews.Where(x => x.NametInterview.Contains(PoiskInterview)).ToListAsync();
                return Ok(_listnterview);
            }
            if (KodProtokola.Trim() != "0" ) _interview = await db.Interviews.FirstOrDefaultAsync(x => x.KodProtokola == KodProtokola);
            if (DetailsInterview.Trim() != "0" && NewProtokol == "0" && PoiskInterview == "0")
            {
                _interview = await db.Interviews.FirstOrDefaultAsync(x => x.DetailsInterview ==  DetailsInterview);
 
            }
            if (DetailsInterview.Trim() != "0" && NewProtokol == "-1")
            {
                List<Interview> _listnterview = await db.Interviews.Where(x => x.DetailsInterview.StartsWith(DetailsInterview)).ToListAsync();
                if (_listnterview != null)
                {
                    return Ok(_listnterview);
                }
            }

            if (KodProtokola.Trim() == "0" && DetailsInterview.Trim() == "0" && NewProtokol == "0")
            {
                if (db.Interviews.Count() > 0)
                {
                    string maxkod = await db.Interviews.MaxAsync(p => p.KodProtokola);
                    _interview = await db.Interviews.FirstOrDefaultAsync(x => x.KodProtokola == maxkod);
                }
            }
            //if (KodProtokola.Trim() == "0" && DetailsInterview.Trim() == "0") { return NotFound(); }
            return Ok(_interview);
        }

        // POST api/<InterviewController>
        [HttpPost]
        public async Task<ActionResult<Interview>> Post(Interview _interview)
        {
            if (_interview == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Interviews.Add(_interview);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Interviews.Any(x => x.Id != _interview.Id)) { return NotFound(); }
            }

            return Ok(_interview);
        }

        // PUT api/<InterviewController>/5
        [HttpPut]
        public async Task<ActionResult<Interview>> Put(Interview _interview)
        {
            if (_interview == null) { return BadRequest(); }

            try
            {
                db.Interviews.Update(_interview);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Interviews.Any(x => x.Id != _interview.Id)) { return NotFound(); }
            }
            return Ok(_interview);
        }

        // DELETE api/<InterviewController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Interview>> Delete(string id)
        {
            Interview _interview = new Interview();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Interviews.ToListAsync();
                foreach (Interview str in _compl)
                {
                    _interview = await db.Interviews.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_interview != null)
                    {
                        db.Interviews.Remove(_interview);
                        await db.SaveChangesAsync();
                    }
                }
                _interview.Id = 0;
            }
            else
            { 
                _interview = await db.Interviews.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_interview == null) { return NotFound(); }
                try
                {
                    db.Interviews.Remove(_interview);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Interviews.Any(x => x.Id == _interview.Id)) { return BadRequest(); }
                }           
            }

            return Ok(_interview);
        }
    }
}
