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
    [Route("api/LikarGrupDiagnozController")]
    [ApiController]
    public class LikarGrupDiagnozController : ControllerBase
    {

        DbContextSeam db;
        public LikarGrupDiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<LikarGrupDiagnozController>
        [HttpGet]
        public async Task<ActionResult<DoctorGrDiagnoz>> Get()
        {
            List<DoctorGrDiagnoz> _detailing = await db.DoctorGrDiagnozs.OrderBy(x => x.KodDoctor).OrderBy(x => x.IcdGrDiagnoz).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<LikarGrupDiagnozController>/5
        [HttpGet("{KodDoctor}/{IcdGrDiagnoz}")]
        public async Task<ActionResult<DoctorGrDiagnoz>> Get(string KodDoctor, string IcdGrDiagnoz)
        {

            if (KodDoctor.Trim() == "0" && IcdGrDiagnoz.Trim() == "0") { return NotFound(); }

            List<DoctorGrDiagnoz> _listdiagnoz = new List<DoctorGrDiagnoz>();
            if (KodDoctor.Trim() != "0" && IcdGrDiagnoz.Trim() == "0")
            { 
                _listdiagnoz = await db.DoctorGrDiagnozs.Where(x => x.KodDoctor == KodDoctor).OrderBy(x => x.IcdGrDiagnoz).ToListAsync();            
            }
            if (IcdGrDiagnoz.Trim() != "0" && KodDoctor.Trim() == "0")
            {
                _listdiagnoz = await db.DoctorGrDiagnozs.Where(x => x.IcdGrDiagnoz == IcdGrDiagnoz).OrderBy(x => x.KodDoctor).ToListAsync();
            }

            if (KodDoctor.Trim() != "0" && IcdGrDiagnoz.Trim() != "0")
            {
                _listdiagnoz = await db.DoctorGrDiagnozs.Where(x => x.IcdGrDiagnoz == IcdGrDiagnoz && x.KodDoctor == KodDoctor).OrderBy(x => x.KodDoctor).ToListAsync();

            }

            return Ok(_listdiagnoz);

        }
        // POST api/<LikarGrupDiagnozController>
        [HttpPost]
        public async Task<ActionResult<DoctorGrDiagnoz>> Post(DoctorGrDiagnoz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.DoctorGrDiagnozs.Add(_detailing);
                await db.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.DoctorGrDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);
        }

        // PUT api/<LikarGrupDiagnozController>/5
        [HttpPut]
        public async Task<ActionResult<DoctorGrDiagnoz>> Put(DoctorGrDiagnoz _detailing)
        {

            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.DoctorGrDiagnozs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.DoctorGrDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }
            return Ok(_detailing);

        }

        // DELETE api/<LikarGrupDiagnozController>/5
        [HttpDelete("{id}/{KodDoctora}")]
        public async Task<ActionResult<DoctorGrDiagnoz>> Delete(string id, string KodDoctora)
        {
            DoctorGrDiagnoz _detailing = new DoctorGrDiagnoz();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.DoctorGrDiagnozs.ToListAsync();
                foreach (DoctorGrDiagnoz str in _compl)
                {
                    _detailing = await db.DoctorGrDiagnozs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.DoctorGrDiagnozs.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            {
                if (KodDoctora.Trim() != "0")
                {
                    _detailing = await db.DoctorGrDiagnozs.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                    if (_detailing != null) db.DoctorGrDiagnozs.RemoveRange(db.DoctorGrDiagnozs.Where(x => x.KodDoctor == KodDoctora));
                }
				if (Convert.ToInt32(id) >0)
				{
					_detailing = await db.DoctorGrDiagnozs.FirstOrDefaultAsync(x =>x.Id == Convert.ToInt32(id));
					if (_detailing == null) { return NotFound(); }
					try
					{
						db.DoctorGrDiagnozs.Remove(_detailing);
						

					}
					catch (DbUpdateConcurrencyException)
					{
						if (db.DoctorGrDiagnozs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

					}
				}
                await db.SaveChangesAsync();
            }

            return Ok(_detailing);
        }
    }
}
