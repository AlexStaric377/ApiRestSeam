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
    [Route("api/ColectionInterviewController")]
    [ApiController]
    public class ColectionInterviewController : ControllerBase
    {
        DbContextSeam db;
        public ColectionInterviewController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }

        // GET: api/<ColectionInterviewController>
        [HttpGet]
        public async Task<ActionResult<ColectionInterview>> Get()
        {
            List<ColectionInterview> _detailing = await db.ColectionInterviews.OrderBy(x => x.KodComplInterv).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<ColectionInterviewController>/5
        [HttpGet("{KodProtokola}/{KodDoctora}/{KodPacienta}")]
        public async Task<ActionResult<ColectionInterview>> Get(string KodProtokola, string KodDoctora,string KodPacienta)
        {
            int minusday = 1;
            bool Truewhile = true;
            DateTime thisDay = DateTime.Now;
            List<ColectionInterview> _detailing = new List<ColectionInterview>();
            List<ColectionInterview> _AllColection = new List<ColectionInterview>();
            if (KodProtokola.Trim() == "0" && KodPacienta.Trim() == "0" && KodDoctora.Trim() == "0") return NotFound();
            if (KodProtokola.Trim() == "0")
            {
                if (KodPacienta.Trim() != "0") 
                {
                    ColectionInterview KodPacientanull = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta );
                    if (KodPacientanull == null) return Ok(_detailing);
                }
                if (KodDoctora.Trim() != "0")
                {
                    _detailing = await db.ColectionInterviews.Where(x => x.KodDoctor == KodDoctora).ToListAsync();
                    if (_detailing.Count == 0) return Ok(_detailing);
                }
                while (Truewhile == true)
                { 
                    var DateToday = thisDay.ToString(); //"гггг-ММ-дд ЧЧ:мм:сс,ффф"
                    DateToday = DateToday.Substring(0, DateToday.IndexOf(" "));
                    if (KodPacienta.Trim() != "0")
                    { 
                        _detailing = await db.ColectionInterviews.Where(x => x.KodPacient == KodPacienta && x.DateInterview.Contains(DateToday)).ToListAsync();                            
                    }
 
                    else
                    {
                        _detailing = await db.ColectionInterviews.Where(x => x.KodDoctor == KodDoctora && x.DateInterview.Contains(DateToday)).ToListAsync();
                    }
                    if (_detailing.Count > 0)
                    {
                        while (Truewhile == true)
                        {
                            thisDay = thisDay.AddDays(-1);
                            if (_detailing.Count == 0 && _AllColection.Count > 15) break;
                            if (_detailing.Count > 0) _AllColection.AddRange(_detailing);
                            if (_AllColection.Count > 15) break;
                            DateToday = thisDay.ToString();
                            DateToday = DateToday.Substring(0, DateToday.IndexOf(" "));
                            if (KodPacienta.Trim() != "0")
                            {
                                _detailing = await db.ColectionInterviews.Where(x => x.KodPacient == KodPacienta && x.DateInterview.Contains(DateToday)).ToListAsync();
                            }
                            else
                            {
                                _detailing = await db.ColectionInterviews.Where(x => x.KodDoctor == KodDoctora && x.DateInterview.Contains(DateToday)).ToListAsync();
                            }
                            if (_detailing.Count == 0) Truewhile = false;
                        }
                        break;
                    }
                    else
                    { 
                        thisDay = thisDay.AddDays(-1);
                        minusday++;
                        if(minusday>=30) Truewhile = false;
                    } 
                }
                if (_AllColection.Count == 0)
                {
                    if (KodPacienta.Trim() != "0")
                    {
                        _AllColection = await db.ColectionInterviews.Where(x => x.KodPacient == KodPacienta).ToListAsync();
                    }
                    if (KodDoctora.Trim() != "0")
                    {
                        _AllColection = await db.ColectionInterviews.Where(x => x.KodDoctor == KodDoctora).ToListAsync();
                        
                    }

                }
            }
            else
            {
                _AllColection = await db.ColectionInterviews.Where(x => x.KodProtokola == KodProtokola).ToListAsync(); ;
            }

            return Ok(_AllColection);

        }

        // POST api/<ColectionInterviewController>
        [HttpPost]
        public async Task<ActionResult<ColectionInterview>> Post(ColectionInterview _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.ColectionInterviews.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ColectionInterviews.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);
        }

        // PUT api/<ColectionInterviewController>/5
        [HttpPut]
        public async Task<ActionResult<ColectionInterview>> Put(ColectionInterview _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.ColectionInterviews.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.ColectionInterviews.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<ColectionInterviewController>/5
        [HttpDelete("{id}/{KodPacienta}/{KodDoctora}")]
        public async Task<ActionResult<ColectionInterview>> Delete(string id, string KodPacienta, string KodDoctora)
        {
            ColectionInterview _detailing = new ColectionInterview();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.ColectionInterviews.ToListAsync();
                foreach (ColectionInterview str in _compl)
                {
                    _detailing = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.ColectionInterviews.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                if (KodPacienta.Trim() == "0" && id == "0" && KodDoctora.Trim() == "0") { return NotFound(); }
                try
                {
                    if (KodPacienta.Trim() != "0")
                    {
                        _detailing = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                        if (_detailing != null)
                        {
                            db.ColectionInterviews.RemoveRange(db.ColectionInterviews.Where(x => x.KodPacient == KodPacienta));
                        }
                    }
                    if (id != "0")
                    {
                        _detailing = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        db.ColectionInterviews.Remove(_detailing);
                    }
                    if (KodDoctora.Trim() != "0")
                    {
                        _detailing = await db.ColectionInterviews.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                        if (_detailing != null) db.ColectionInterviews.RemoveRange(db.ColectionInterviews.Where(x => x.KodDoctor == KodDoctora));
                    }
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Convert.ToInt32(id) != 0)
                    {
                        if (db.ColectionInterviews.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                    }
                    if (KodPacienta.Trim() != "0")
                    {
                        if (db.ColectionInterviews.Any(x => x.KodPacient == _detailing.KodPacient)) { return BadRequest(); }
                    }
                    if (KodDoctora.Trim() != "0")
                    {
                        if (db.ColectionInterviews.Any(x => x.KodDoctor == _detailing.KodDoctor)) { return BadRequest(); }
                    }
                }           
            }
            return Ok(_detailing);
        }
    }
}
