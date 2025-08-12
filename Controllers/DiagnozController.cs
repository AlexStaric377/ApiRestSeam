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
    [Route("api/DiagnozController")]
    [ApiController]
    public class DiagnozController : ControllerBase
    {
        DbContextSeam db;
        public DiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<DiagnozController>
        [HttpGet]
        public async Task<ActionResult<Diagnoz>> Get()
        {
            List<Diagnoz> _diagnoz = await db.Diagnozs.OrderBy(x => x.IcdGrDiagnoz).ToListAsync();
            //var _diagnoz = new JsonResult(await db.Diagnozs.ToListAsync());
            return Ok(_diagnoz);
        }

        // GET api/<DiagnozController>/5
        [HttpGet("{KodDiagnoza}/{IcdGrDiagnoz}/{PoiskDiagnoz}")]
        public async Task<ActionResult<Diagnoz>> Get(string KodDiagnoza, string IcdGrDiagnoz ,string PoiskDiagnoz)
        {
            List<Diagnoz> _listdiagnoz = new  List<Diagnoz>();
            if (KodDiagnoza.Trim() == "0" && IcdGrDiagnoz.Trim() == "0" && PoiskDiagnoz.Trim() == "0") { return NotFound(); }
            if (PoiskDiagnoz.Trim() != "0")
            {
                _listdiagnoz = await db.Diagnozs.Where(x => x.NameDiagnoza.Contains(PoiskDiagnoz)).ToListAsync();
            }
            if (KodDiagnoza.Trim() != "0")
            {
                Diagnoz _diagnoz = await db.Diagnozs.FirstOrDefaultAsync(x => x.KodDiagnoza == KodDiagnoza);
                return Ok(_diagnoz);
            }
            if (IcdGrDiagnoz.Trim() != "0")
            {
                _listdiagnoz = await db.Diagnozs.Where(x => x.KeyIcd.Contains(IcdGrDiagnoz)).OrderBy(x => x.KodDiagnoza).ToListAsync();
            }
            return Ok(_listdiagnoz);
        }

        // POST api/<DiagnozController>
        [HttpPost]
        public async Task<ActionResult<Diagnoz>> Post(Diagnoz _diagnoz)
        {
            if (_diagnoz == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Diagnozs.Add(_diagnoz);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Diagnozs.Any(x => x.Id != _diagnoz.Id)) { return NotFound(); }
            }

            return Ok(_diagnoz);
        }

        // PUT api/<DiagnozController>/5
        [HttpPut]
        public async Task<ActionResult<Diagnoz>> Put( Diagnoz _diagnoz)
        {
            if (_diagnoz == null) { return BadRequest(); }
            
            try
            {
                db.Diagnozs.Update(_diagnoz);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Diagnozs.Any(x => x.Id != _diagnoz.Id)) { return NotFound(); }
            }
            return Ok(_diagnoz);

        }

        // DELETE api/<DiagnozController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Diagnoz>> Delete(string id)
        {
            Diagnoz _diagnoz = new Diagnoz();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Diagnozs.ToListAsync();
                foreach (Diagnoz str in _compl)
                {
                    _diagnoz = await db.Diagnozs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_diagnoz != null)
                    {
                        db.Diagnozs.Remove(_diagnoz);
                        await db.SaveChangesAsync();
                    }
                }
                _diagnoz.Id = 0;
            }
            else
            { 
                _diagnoz = await db.Diagnozs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_diagnoz == null) { return NotFound(); }
                try
                {
                    db.Diagnozs.Remove(_diagnoz);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Diagnozs.Any(x => x.Id == _diagnoz.Id))  { return BadRequest(); }

                }            
            }

            return Ok(_diagnoz);
        }
    }
}
