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
    [Route("api/PacientController")]
    [ApiController]
    public class PacientController : ControllerBase
    {
        DbContextSeam db;
        public PacientController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }


        }
        // GET: api/<PacientController>
        [HttpGet]
        public async Task<ActionResult<Pacient>> Get()
        {
            List<Pacient> _detailing = await db.Pacients.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);
        }
        // GET api/<PacientController>/5
        [HttpGet("{KodPacient}/{id}/{NamePacient}/{SurnamePacient}/{TelefonPacient}")]
        public async Task<ActionResult<Pacient>> Get(string KodPacient, string Id, string NamePacient, string SurnamePacient, string TelefonPacient)
        {
            
            Pacient _detailing = new Pacient();
            
            if (KodPacient == "0" && Id == "0" && NamePacient == "0" && SurnamePacient == "0" && TelefonPacient == "0")
            {
                if (db.Pacients.Count() > 0)
                {
                    string maxid = await db.Pacients.MaxAsync(p => p.KodPacient);
                    _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.KodPacient == maxid);
                    return Ok(_detailing);
                }
            }

            if (Id != "0")
            {
                _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(Id));
                return Ok(_detailing);
            }
            if (KodPacient.Trim() != "0")
            {
                _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.KodPacient == KodPacient);
             }
            else
            {
                List<Pacient> _listdetailing = new List<Pacient>();
                if (NamePacient != "0" && SurnamePacient == "0" && TelefonPacient == "0")
                {
                    _listdetailing = await db.Pacients.Where(x => x.Name.Contains(NamePacient)).ToListAsync();
                }
                else
                {
                    if (NamePacient == "0" && SurnamePacient != "0" && TelefonPacient == "0")
                    {
                        _listdetailing = await db.Pacients.Where(x => x.Surname.Contains(SurnamePacient)).ToListAsync();
                    }
                    else
                    {
                        if (NamePacient == "0" && SurnamePacient == "0" && TelefonPacient != "0")
                        {
                            _listdetailing = await db.Pacients.Where(x => x.Tel.Contains(TelefonPacient)).ToListAsync();
                        }
                        else
                        {
                            if (NamePacient != "0" && SurnamePacient != "0" && TelefonPacient == "0")
                            {
                                _listdetailing = await db.Pacients.Where(x => x.Name.Contains(NamePacient) && x.Surname.StartsWith(SurnamePacient)).ToListAsync();
                            }
                            else
                            {
                                if (NamePacient != "0" && SurnamePacient == "0" && TelefonPacient != "0")
                                {
                                    _listdetailing = await db.Pacients.Where(x => x.Name.Contains(NamePacient) && x.Tel.StartsWith(TelefonPacient)).ToListAsync();
                                }
                                else
                                {
                                    if (NamePacient == "0" && SurnamePacient != "0" && TelefonPacient != "0")
                                    {
                                        _listdetailing = await db.Pacients.Where(x => x.Surname.Contains(SurnamePacient) && x.Tel.StartsWith(TelefonPacient)).ToListAsync();
                                    }
                                    else
                                    { 
                                        if (NamePacient != "0" && SurnamePacient != "0" && TelefonPacient != "0")
                                        {
                                            _listdetailing = await db.Pacients.Where(x => x.Name.Contains(NamePacient) && x.Surname.StartsWith(SurnamePacient) && x.Tel.StartsWith(TelefonPacient)).ToListAsync();
                                        }                                      
                                    }
                                 
                                }
                                 
                            }
                     
                        }
                        
                    }
                }
                return Ok(_listdetailing);
            }
            if (_detailing == null)return NotFound();
            else return Ok(_detailing);
        }

        // POST api/<PacientController>
        [HttpPost]
        public async Task<ActionResult<Pacient>> Post(Pacient _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Pacients.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Pacients.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<PacientController>/5
        [HttpPut]
        public async Task<ActionResult<Pacient>> Put(Pacient _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Pacients.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Pacients.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<PacientController>/5
        [HttpDelete("{id}/{KodPacienta}")]
        public async Task<ActionResult<Pacient>> Delete(string id, string KodPacienta)
        {
            Pacient _detailing = new Pacient();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Pacients.ToListAsync();
                foreach (Pacient str in _compl)
                {
                    _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.Pacients.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                if (KodPacienta.Trim() != "0")
                {

                    _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.Pacients.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }

                }
                if (Convert.ToInt32(id) > 0)
                {
                    _detailing = await db.Pacients.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_detailing == null) { return NotFound(); }
                    try
                    {
                        db.Pacients.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.Pacients.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                    }
                }
            }
            return Ok(_detailing);
        }
    }
}
