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
    [Route("api/DependencyDiagnozController")]
    [ApiController]
    public class DependencyDiagnozController : ControllerBase
    {
         DbContextSeam db;
        public DependencyDiagnozController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<DependencyDiagnozController>
        [HttpGet]
        public async Task<ActionResult<DependencyDiagnoz>> Get()
        {
            List<DependencyDiagnoz> _detailing = await db.DependencyDiagnozs.OrderBy(x => x.KodDiagnoz).ToListAsync();
            //var _detailing = new JsonResult(await db.DependencyDiagnozs.ToListAsync());
            return Ok(_detailing);
        }

        // GET api/<DependencyDiagnozController>/5
        [HttpGet("{KodDiagnoz}/{KodProtokola}/{PoiskDiagnoz}")]
        public async Task<ActionResult<DependencyDiagnoz>> Get(string KodDiagnoz, string KodProtokola, string PoiskDiagnoz)
        {

            if (KodDiagnoz.Trim() == "0" && KodProtokola.Trim() == "0" && PoiskDiagnoz.Trim() == "0") { return NotFound(); }

            if (PoiskDiagnoz.Trim() != "0")
            {
                List<DependencyDiagnoz> listDependency = new List<DependencyDiagnoz>();
                List<Diagnoz> _listDiagnoz = await db.Diagnozs.Where(x => x.NameDiagnoza.Contains(PoiskDiagnoz)).ToListAsync();
                foreach (Diagnoz diagnoz in _listDiagnoz)
                {
                    DependencyDiagnoz _koddiagnoz = await db.DependencyDiagnozs.FirstOrDefaultAsync(x => x.KodDiagnoz == diagnoz.KodDiagnoza);
                    listDependency.Add(_koddiagnoz);
                }
                return Ok(listDependency);
            }
            DependencyDiagnoz _detailing = (KodDiagnoz.Trim() == "0" && PoiskDiagnoz.Trim() == "0") ? await db.DependencyDiagnozs.FirstOrDefaultAsync(x => x.KodProtokola == KodProtokola) :
                await db.DependencyDiagnozs.FirstOrDefaultAsync(x => x.KodDiagnoz == KodDiagnoz);
            return Ok(_detailing);
        }

        // POST api/<DependencyDiagnozController>
        [HttpPost]
        public async Task<ActionResult<DependencyDiagnoz>> Post(DependencyDiagnoz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.DependencyDiagnozs.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.DependencyDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<DependencyDiagnozController>/5
        [HttpPut]
        public async Task<ActionResult<DependencyDiagnoz>> Put(DependencyDiagnoz _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.DependencyDiagnozs.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.DependencyDiagnozs.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<DependencyDiagnozController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DependencyDiagnoz>> Delete(string id)
        {
            DependencyDiagnoz _detailing = new DependencyDiagnoz();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.DependencyDiagnozs.ToListAsync();
                foreach (DependencyDiagnoz str in _compl)
                {
                    _detailing = await db.DependencyDiagnozs.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.DependencyDiagnozs.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                _detailing = await db.DependencyDiagnozs.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.DependencyDiagnozs.Remove(_detailing);
                    await db.SaveChangesAsync();  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.DependencyDiagnozs.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }           
            }

            return Ok(_detailing);
        }
    }
}
