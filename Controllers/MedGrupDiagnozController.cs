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
    [Route("api/MedGrupDiagnozController")]
    [ApiController]
    public class MedGrupDiagnozController : ControllerBase
    {


        DbContextSeam db;
        public MedGrupDiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<MedGrupDiagnozController>
        [HttpGet]
        public async Task<ActionResult<MedicalGrDiagnoz>> Get()
        {
            List<MedicalGrDiagnoz> _detailing = await db.MedicalGrDiagnozs.OrderBy(x => x.Edrpou).OrderBy(x => x.IcdGrDiagnoz).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<MedGrupDiagnozController>/5
        [HttpGet("{Edrpou}/{IcdGrDiagnoz}/{IcdKey}")]
        public async Task<ActionResult<MedicalGrDiagnoz>> Get(string Edrpou, string IcdGrDiagnoz, string IcdKey)
        {
            List<MedicalGrDiagnoz> _listdiagnoz = new List<MedicalGrDiagnoz>();
            if (Edrpou.Trim() == "0" && IcdGrDiagnoz.Trim() == "0" && IcdKey.Trim() == "0") { return NotFound(); }

            if (Edrpou.Trim() != "0")
            { 
                _listdiagnoz = await db.MedicalGrDiagnozs.Where(x => x.Edrpou == Edrpou).OrderBy(x => x.IcdGrDiagnoz).ToListAsync();
            }
            if (IcdGrDiagnoz.Trim() != "0")
            {
                _listdiagnoz = await db.MedicalGrDiagnozs.Where(x => x.IcdGrDiagnoz == IcdGrDiagnoz).OrderBy(x => x.Edrpou).ToListAsync();
            }
            if (IcdKey.Trim() != "0")
            {
                _listdiagnoz = await db.MedicalGrDiagnozs.Where(x => x.IcdKey.Contains(IcdKey) == true).OrderBy(x => x.Edrpou).ToListAsync();
            }
            return Ok(_listdiagnoz);
 
        }

        // POST api/<MedGrupDiagnozController>
        public async Task<ActionResult<MedicalGrDiagnoz>> Post(MedicalGrDiagnoz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.MedicalGrDiagnozs.Add(_detailing);
                await db.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.MedicalGrDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<MedGrupDiagnozController>/5
        [HttpPut]
        public async Task<ActionResult<MedicalGrDiagnoz>> Put(MedicalGrDiagnoz _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.MedicalGrDiagnozs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.MedicalGrDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);

        }

        // DELETE api/<MedGrupDiagnozController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalGrDiagnoz>> Delete(string id)
        {
            MedicalGrDiagnoz _detailing = new MedicalGrDiagnoz();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.MedicalGrDiagnozs.ToListAsync();
                foreach (MedicalGrDiagnoz str in _compl)
                {
                    _detailing = await db.MedicalGrDiagnozs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.MedicalGrDiagnozs.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.MedicalGrDiagnozs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.MedicalGrDiagnozs.Remove(_detailing);
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.MedicalGrDiagnozs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
