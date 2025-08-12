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
    [Route("api/ApiControllerDoctor")]
    [ApiController]
    public class ApiControllerDoctor : ControllerBase
    {
        DbContextSeam db;

        public ApiControllerDoctor(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
  
        }
        // GET: api/<ApiControllerDoctor>
        [HttpGet]
        public async Task<ActionResult<Doctor>> Get()
        {
            List<Doctor> _doctor = await db.Doctors.OrderBy(x => x.Edrpou ).OrderBy(x => x.KodDoctor).ToListAsync();
            return Ok(_doctor);
        }

        // GET api/<ApiControllerDoctor>/5
        [HttpGet("{KodDoctor}/{Edrpou}/{PoiskDoctor}")]
        public async Task<ActionResult<Doctor>> Get(string KodDoctor, string Edrpou, string PoiskDoctor)
        {
            if (KodDoctor == "0" && Edrpou == "0" && PoiskDoctor.Trim() == "0") return NotFound(); 
            if (PoiskDoctor.Trim() != "0")
            {
                List<Doctor> _listDoctor = await db.Doctors.Where(x => x.Surname.Contains(PoiskDoctor) == true).ToListAsync();
                return Ok(_listDoctor);
            }
            if (Edrpou != "0") 
            {
                List<Doctor> _listdoctor = await db.Doctors.Where(x => x.Edrpou == Edrpou).OrderBy(x => x.KodDoctor).ToListAsync();
                return Ok(_listdoctor);
            }
            Doctor _doctor = new Doctor();
            if (KodDoctor != "0")
            {
                _doctor = await db.Doctors.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctor);
            }
            return Ok(_doctor);           

        }

        // POST api/<ApiControllerDoctor>
        [HttpPost]
        public async Task<ActionResult<Doctor>> Post(Doctor _doctor)
        {
            if (_doctor == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Doctors.Add(_doctor);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Doctors.Any(x => x.Id != _doctor.Id)) { return NotFound(); }
            }
            return Ok(_doctor);
        }

        // PUT api/<ApiControllerDoctor>/5
        [HttpPut]
        public async Task<ActionResult<Doctor>> Put(Doctor _doctor)
        {

            if (_doctor == null) { return BadRequest(); }
            try
            {
                db.Doctors.Update(_doctor);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Doctors.Any(x => x.Id != _doctor.Id)) { return NotFound(); }
            }
            return Ok(_doctor);

        }

        // DELETE api/<ApiControllerDoctor>/5
        [HttpDelete("{id}/{KodDoctora}")]
        public async Task<ActionResult<Doctor>> Delete(string id, string KodDoctora)
        {
            Doctor _doctor = new Doctor();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Doctors.ToListAsync();
                foreach (Doctor str in _compl)
                {
                    _doctor = await db.Doctors.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_doctor != null)
                    { 
                         db.Doctors.Remove(_doctor);
                        await db.SaveChangesAsync();                  
                    }
 
                }
                _doctor.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                { 
                    _doctor = await db.Doctors.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_doctor == null) { return NotFound(); }
                    try
                    {
                        db.Doctors.Remove(_doctor);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.Doctors.Any(x => x.Id == _doctor.Id)) { return BadRequest(); }

                    }                          
                }
                if (KodDoctora.Trim() != "0")
                {
                    _doctor = await db.Doctors.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                    if (_doctor != null) db.Doctors.Remove(_doctor);
                    await db.SaveChangesAsync();
                }

            }
            return Ok(_doctor);
        }
    }
}
