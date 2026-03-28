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
    [Route("api/ControlerFamilyLikar")]
    [ApiController]
    public class ControlerFamilyLikar : ControllerBase
    {
        DbContextSeam db;
        public ControlerFamilyLikar(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }



        // GET: api/<ControlerFamilyLikar>
        [HttpGet]
        public async Task<ActionResult<FamilyLikar>> Get()
        {
            List<FamilyLikar> _detailing = await db.FamilyLikars.OrderBy(x => x.KodPacient).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<ControlerFamilyLikar>/5
        [HttpGet("{KodPacient}/{KodDoctor}")]
        public async Task<ActionResult<FamilyLikar>> Get(string KodPacient, string KodDoctor)
        {
            List<FamilyLikar> _detailing = new List<FamilyLikar>();
            
            if (KodDoctor.Trim() == "0" && KodPacient.Trim() == "0" ) { return NotFound(); }
            if (KodDoctor.Trim() != "0" && KodPacient.Trim() == "0" )
            {
                _detailing = await db.FamilyLikars.Where(x => x.KodDoctor == KodDoctor).ToListAsync();
            }

            if (KodPacient.Trim() != "0" && KodDoctor.Trim() == "0" )
            {
                _detailing = await db.FamilyLikars.Where(x => x.KodPacient == KodPacient).ToListAsync();
            }

            if (KodPacient.Trim() != "0" && KodDoctor.Trim() != "0" )
            {
                _detailing = await db.FamilyLikars.Where(x => x.KodPacient == KodPacient && x.KodDoctor == KodDoctor ).ToListAsync();
            }
            return Ok(_detailing);
        }

        // POST api/<ControlerFamilyLikar>
        [HttpPost]
        public async Task<ActionResult<FamilyLikar>> Post(FamilyLikar _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты 
            try
            {
                db.FamilyLikars.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.FamilyLikars.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<ControlerFamilyLikar>/5
        [HttpPut]
        public async Task<ActionResult<FamilyLikar>> Put(FamilyLikar _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.FamilyLikars.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.FamilyLikars.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<ControlerFamilyLikar>/5
        [HttpDelete("{id}/{KodPacienta}/{KodDoctora}")]
        public async Task<ActionResult<FamilyLikar>> Delete(string id, string KodPacienta, string KodDoctora)
        {
            if (KodPacienta.Trim() == "0" && id == "0" && KodDoctora.Trim() == "0") { return NotFound(); }
            FamilyLikar _detailing = new FamilyLikar();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.FamilyLikars.ToListAsync();
                foreach (FamilyLikar str in _compl)
                {
                    _detailing = await db.FamilyLikars.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.FamilyLikars.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }

                }

                _detailing.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                {
                    _detailing = await db.FamilyLikars.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_detailing == null) { return NotFound(); }
                    try
                    {
                        db.FamilyLikars.Remove(_detailing);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.FamilyLikars.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                    }
                }
                if (KodPacienta.Trim() != "0" && KodDoctora.Trim() == "0" )
                {

                    _detailing = await db.FamilyLikars.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta);
                    if (_detailing != null)
                    {
                        db.FamilyLikars.RemoveRange(db.FamilyLikars.Where(x => x.KodPacient == KodPacienta));
                        await db.SaveChangesAsync();
                    }
                }
                if (KodDoctora.Trim() != "0" && KodPacienta.Trim() == "0" )
                {
                    _detailing = await db.FamilyLikars.FirstOrDefaultAsync(x => x.KodDoctor == KodDoctora);
                    if (_detailing != null) db.FamilyLikars.RemoveRange(db.FamilyLikars.Where(x => x.KodDoctor == KodDoctora));
                }

                if (KodPacienta.Trim() != "0" && KodDoctora.Trim() != "0" )
                {
                    _detailing = await db.FamilyLikars.FirstOrDefaultAsync(x => x.KodPacient == KodPacienta && x.KodDoctor == KodDoctora );
                    if (_detailing != null) db.FamilyLikars.RemoveRange(db.FamilyLikars.Where(x => x.KodPacient == KodPacienta && x.KodDoctor == KodDoctora ));
                }

                await db.SaveChangesAsync();
            }
            return Ok(_detailing);
        }
    }
}
