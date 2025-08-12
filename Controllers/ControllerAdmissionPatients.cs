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
    [Route("api/ControllerAdmissionPatients")]
    [ApiController]
    public class ControllerAdmissionPatients : ControllerBase
    {

            DbContextSeam db;
            public ControllerAdmissionPatients(DbContextSeam context)
            {
                db = context;
                if (!db.Complaints.Any())
                {
                    StartLoadDb LoadDb = new();
                    LoadDb.AddDb(db);
                }
            }

        // GET: api/<ControllerAdmissionPatients>
        [HttpGet]
        public async Task<ActionResult<AdmissionPatients>> Get()
        {
            List<AdmissionPatients> _content = await db.AdmissionPatientss.ToListAsync();
            return Ok(_content);
        }


        // GET api/<ControllerAdmissionPatients>/5
        [HttpGet("{KodPacient}/{KodDoctor}/{KodComplInterv}/{DateVizita}")]
        public async Task<ActionResult<AdmissionPatients>> Get(string KodPacient, string KodDoctor, string KodComplInterv, string DateVizita)
        {
            if (KodPacient.Trim() == "0" && DateVizita.Trim() == "0" && KodDoctor.Trim() == "0" && KodComplInterv.Trim() == "0") { return NotFound(); }
            List<AdmissionPatients> _detailing = new List <AdmissionPatients>();
            if (KodPacient.Trim() == "0")
            {

                if (KodDoctor.Trim() != "0" && KodComplInterv.Trim() == "0")
                {
                    _detailing = await db.AdmissionPatientss.Where(x => x.KodDoctor == KodDoctor).OrderBy(x => x.DateVizita).ToListAsync(); //FirstOrDefaultAsync(x => x.KodDoctor == KodDoctor);
                }
                else 
                { 
                    _detailing = await db.AdmissionPatientss.Where(x => x.DateVizita == DateVizita).ToListAsync();                
                }

            }
            else
            {
                if (KodDoctor.Trim() == "0" && KodComplInterv.Trim() == "0")
                { 
                    _detailing = await db.AdmissionPatientss.Where(x => x.KodPacient == KodPacient ).OrderBy(x => x.DateVizita).ToListAsync();
                }
                if (KodDoctor.Trim() != "0" && KodComplInterv.Trim() == "0")
                {
                    _detailing = await db.AdmissionPatientss.Where(x => x.KodDoctor== KodDoctor && x.KodPacient == KodPacient  ).OrderBy(x => x.KodPacient).ToListAsync();
                }
                if (KodDoctor.Trim() != "0" && KodComplInterv.Trim() != "0")
                {
                    _detailing = await db.AdmissionPatientss.Where(x =>x.KodDoctor == KodDoctor &&  x.KodPacient == KodPacient &&  x.KodComplInterv == KodComplInterv).OrderBy(x => x.DateVizita).ToListAsync();
                }
            }
            return Ok(_detailing);

        }

        // POST api/<ControllerAdmissionPatients>
        [HttpPost]
        public async Task<ActionResult<AdmissionPatients>> Post(AdmissionPatients _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.AdmissionPatientss.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AdmissionPatientss.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
 
            return Ok(_detailing);
        }

        // PUT api/<ControllerAdmissionPatients>/5
        [HttpPut]
        public async Task<ActionResult<AdmissionPatients>> Put(AdmissionPatients _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.AdmissionPatientss.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AdmissionPatientss.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<ControllerAdmissionPatients>/5
        [HttpDelete("{id}/{KodPacienta}/{KodDoctora}")]
        public async Task<ActionResult<AdmissionPatients>> Delete(string id, string KodPacienta, string KodDoctora)
        {
            if ( id == "0" && KodDoctora.Trim() == "0" && KodPacienta.Trim() == "0") { return NotFound(); }
            AdmissionPatients _detailing = new AdmissionPatients();
            try
            {
                if (id != "0")
                {
                    if (Convert.ToInt32(id) == -1)
                    {
                        var _compl = await db.AdmissionPatientss.ToListAsync();
                        foreach (AdmissionPatients str in _compl)
                        {
                            _detailing = await db.AdmissionPatientss.FirstOrDefaultAsync(x => x.Id == str.Id);
                            if (_detailing != null)
                            {
                                db.AdmissionPatientss.Remove(_detailing);
                                await db.SaveChangesAsync();
                            }
                        }
                        _detailing.Id = 0;
                    }
                    else
                    {
                        _detailing = await db.AdmissionPatientss.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        db.AdmissionPatientss.Remove(_detailing);
                    }
                }
                else
                { 
                    if (KodDoctora.Trim() != "0")
                    {
                        _detailing = await db.AdmissionPatientss.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                        if (_detailing != null) db.AdmissionPatientss.RemoveRange(db.AdmissionPatientss.Where(x => x.KodDoctor == KodDoctora));
                    }
                    if (KodPacienta.Trim() != "0")
                    {

                        _detailing = await db.AdmissionPatientss.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                        if (_detailing != null)
                        {
                            db.AdmissionPatientss.RemoveRange(db.AdmissionPatientss.Where(x => x.KodPacient == KodPacienta));
                        }
                    }
                }
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Convert.ToInt32(id) != 0)
                {
                    if (db.AdmissionPatientss.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }
                if (KodDoctora.Trim() != "0")
                {
                    if (db.AdmissionPatientss.Any(x => x.KodDoctor == _detailing.KodDoctor)) { return BadRequest(); }
                }

            }
            return Ok(_detailing);
        }
    }
}
