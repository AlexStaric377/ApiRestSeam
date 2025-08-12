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
    [Route("api/FeatureController")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        DbContextSeam db;
        public FeatureController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<FeatureController>
        [HttpGet]
        public async Task<ActionResult<Feature>>  Get()
        {
            List<Feature> _feature = await db.Features.OrderBy(x => x.KeyComplaint).OrderBy(x => x.KeyFeature).ToListAsync();
            return Ok(_feature);
        }

        // GET api/<FeatureController>/5 поиск карточки описания характера,особености симптома жалобы 
        [HttpGet("{KeyFeature}/{KeyComplaint}/{PoiskNameFeature}")]
        public async Task<ActionResult<Feature>> Get(string KeyFeature, string KeyComplaint, string PoiskNameFeature)
        {
            if (KeyComplaint != "0")
            {
                List<Feature> _listfeature = await db.Features.Where(x => x.KeyComplaint == KeyComplaint).OrderBy(x => x.KeyFeature).ToListAsync();
                return Ok(_listfeature);
            }
            if (KeyFeature != "0")
            { 
               Feature _featurek = await db.Features.FirstOrDefaultAsync(x => x.KeyFeature == KeyFeature);
               return Ok(_featurek); 
            }
            if (PoiskNameFeature != "0")
            {
                List<Feature> _listfeature = await db.Features.Where(x => x.Name.Contains(PoiskNameFeature)).ToListAsync();
                return Ok(_listfeature);
            }

            var _feature = new JsonResult(await db.Features.OrderBy(x => x.KeyComplaint).OrderBy(x => x.KeyFeature).ToListAsync());
            return Ok(_feature);

        }

        // POST api/<FeatureController> создание новых  строк описания характера,особености жалобы 
        [HttpPost]
        public async Task<ActionResult<Feature>> Post(Feature _feature)
        {
            if (_feature == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Features.Add(_feature);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Features.Any(x => x.Id != _feature.Id)) { return NotFound(); }
            }
            return Ok(_feature);
        }

        // PUT api/<FeatureController>/5 модификация описания характера,особености жалобы 
        [HttpPut]
        public async Task<ActionResult<Feature>> Put( Feature _feature)
        {
            
            if (_feature == null) { return BadRequest(); }
            try
            {
                db.Features.Update(_feature);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (db.Features.Any(x => x.Id != _feature.Id)) { return NotFound(); }
            }
            return Ok(_feature);

        }

        // DELETE api/<FeatureController>/5 удаление описания характера,особености жалобы 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feature>> Delete(string id)
        {
            Feature _feature = new  Feature();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Features.ToListAsync();
                foreach (Feature str in _compl)
                {
                    _feature = await db.Features.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_feature != null)
                    {
                        db.Features.Remove(_feature);
                        await db.SaveChangesAsync();
                    }
                }
                _feature.Id = 0;
            }
            else
            {
                _feature = await db.Features.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_feature == null) { return NotFound(); }
                try
                {
                    db.Features.Remove(_feature);
                    await db.SaveChangesAsync();                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Features.Any(x => x.Id == _feature.Id)) { return BadRequest(); }
                }
            }
 
            return Ok(_feature);
        }
    }
}
