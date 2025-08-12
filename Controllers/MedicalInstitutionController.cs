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
    [Route("api/MedicalInstitutionController")]
    [ApiController]
    public class MedicalInstitutionController : ControllerBase
    {
        DbContextSeam db;
        public MedicalInstitutionController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }
        // GET: api/<MedicalInstitutionController>
        [HttpGet]
        public async Task<ActionResult<MedicalInstitution>> Get()
        {
            List<MedicalInstitution> _detailing = await db.MedicalInstitutions.OrderBy(x => x.Edrpou).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<MedicalInstitutionController>/5
        [HttpGet("{Edrpou}/{PostIndex}/{PoiskNameMedZakl}/{Idstatus}")]
        public async Task<ActionResult<MedicalInstitution>> Get(string Edrpou, string PostIndex, string PoiskNameMedZakl, string Idstatus)
        {

            if (Edrpou.Trim() == "0" && PostIndex.Trim() == "0" && PoiskNameMedZakl.Trim() == "0" && Idstatus == "0") { return NotFound(); }

            MedicalInstitution _detailing = new MedicalInstitution();
 
            if (PoiskNameMedZakl.Trim() != "0")
            {
                List<MedicalInstitution> _listMedicalInstitution = await db.MedicalInstitutions.Where(x => x.Name.Contains(PoiskNameMedZakl) == true).ToListAsync();
                return Ok(_listMedicalInstitution);
            }
            if (Edrpou.Trim() != "0")
            {
                _detailing = await db.MedicalInstitutions.FirstOrDefaultAsync(x => x.Edrpou == Edrpou);
            }
            if (PostIndex.Trim() != "0")
            {
                _detailing = await db.MedicalInstitutions.FirstOrDefaultAsync(x => x.PostIndex == PostIndex);
                if (_detailing == null)
                {
                    List<MedicalInstitution> _detailinglist = await db.MedicalInstitutions.Where(x => x.PostIndex.Substring(0,2) == PostIndex.Substring(0, 2)).OrderBy(x => x.Edrpou).ToListAsync();
                    return Ok(_detailinglist);
                }
            }
            if (Idstatus != "0")
            {
                List<MedicalInstitution> _listMedicalInstitution = await db.MedicalInstitutions.Where(x => x.IdStatus == Idstatus).ToListAsync();
                return Ok(_listMedicalInstitution);
            }
            return Ok(_detailing);
        }

        // POST api/<MedicalInstitutionController>
        [HttpPost]
        public async Task<ActionResult<MedicalInstitution>> Post(MedicalInstitution _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.MedicalInstitutions.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.MedicalInstitutions.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }
        // PUT api/<MedicalInstitutionController>/5
        [HttpPut]
        public async Task<ActionResult<MedicalInstitution>> Put(MedicalInstitution _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.MedicalInstitutions.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.MedicalInstitutions.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<MedicalInstitutionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalInstitution>> Delete(string id)
        {

            MedicalInstitution _detailing = new MedicalInstitution();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.MedicalInstitutions.ToListAsync();
                foreach (MedicalInstitution str in _compl)
                {
                    _detailing = await db.MedicalInstitutions.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.MedicalInstitutions.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                _detailing = await db.MedicalInstitutions.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.MedicalInstitutions.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.MedicalInstitutions.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                }           
            }

            return Ok(_detailing);
        }
    }
}
