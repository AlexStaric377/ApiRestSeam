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
    [Route("api/RecommendationController")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        DbContextSeam db;
        public RecommendationController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }
        // GET: api/<RecommendationController>
        [HttpGet]
        public async Task<ActionResult<Recommendation>> Get()
        {
            List<Recommendation> _detailing = await db.Recommendations.OrderBy(x => x.KodRecommendation).ToListAsync();
            return Ok(_detailing);
        }

        // GET api/<RecommendationController>/5
        [HttpGet("{KodRecommendation}/{PoiskRecommendation}")]
        public async Task<ActionResult<Recommendation>> Get(string KodRecommendation, string PoiskRecommendation)
        {
 
            if (KodRecommendation.Trim() == "0" && PoiskRecommendation.Trim() == "0") { return NotFound(); }
            if (KodRecommendation.Trim() == "0")
            {
                List<Recommendation> _listRecommendation = await db.Recommendations.Where(x => x.ContentRecommendation.Contains(PoiskRecommendation)).ToListAsync();
                return Ok(_listRecommendation);
            }
            Recommendation _detailing = await db.Recommendations.FirstOrDefaultAsync(x => x.KodRecommendation == KodRecommendation);
            return Ok(_detailing);

        }


        // POST api/<RecommendationController>
        [HttpPost]
        public async Task<ActionResult<Recommendation>> Post(Recommendation _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Recommendations.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Recommendations.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }

        // PUT api/<RecommendationController>/5
        [HttpPut]
        public async Task<ActionResult<Recommendation>> Put(Recommendation _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.Recommendations.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Recommendations.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<RecommendationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recommendation>> Delete(string id)
        {
            Recommendation _detailing = new Recommendation();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Recommendations.ToListAsync();
                foreach (Recommendation str in _compl)
                {
                    _detailing = await db.Recommendations.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.Recommendations.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                
                _detailing.Id = 0;
            }
            else
            {
                _detailing = await db.Recommendations.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_detailing == null) { return NotFound(); }
                try
                {
                    db.Recommendations.Remove(_detailing);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Recommendations.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }

                }
            }
            return Ok(_detailing);
        }
    }
}
