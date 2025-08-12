using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/ControllerPrice")]
    [ApiController]
    public class ControllerPrice : ControllerBase
    {
        DbContextSeam db;

        public ControllerPrice(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }

        // GET: api/<ControllerPrice>
        [HttpGet]
        public async Task<ActionResult<Price>> Get()
        {
            List<Price> _price = await db.Prices.OrderBy(x => x.KeyPrice).ToListAsync();
            return Ok(_price);
        }


        // GET api/<ControllerPrice>/5
        [HttpGet("{KeyPrice}")]
        public async Task<ActionResult<Price>> Get( string KeyPrice)
        {
            List<Price> _price = new List<Price>();
            if ( KeyPrice == "0" ) return NotFound();

            if (KeyPrice != "0")
            {
                _price = await db.Prices.Where(x => x.KeyPrice == KeyPrice).ToListAsync();
            }
            return Ok(_price);

        }

        // POST api/<ControllerPrice>
        [HttpPost]
        public async Task<ActionResult<Price>> Post(Price _price)
        {
            if (_price == null) { return BadRequest(); }
            // Создание новой записи об оплате
            try
            {
                db.Prices.Add(_price);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Prices.Any(x => x.Id != _price.Id)) { return NotFound(); }
            }
            return Ok(_price);
        }

        // PUT api/<ControllerPrice>/5
        [HttpPut]
        public async Task<ActionResult<Price>> Put(Price _price)
        {
            if (_price == null) { return BadRequest(); }
            try
            {
                db.Prices.Update(_price);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Prices.Any(x => x.Id != _price.Id)) { return NotFound(); }
            }
            return Ok(_price);

        }

        // DELETE api/<ControllerPrice>/5
        [HttpDelete("{id}/{KeyPrice}")]
        public async Task<ActionResult<Price>> Delete(string id,  string KeyPrice)
        {
            Price _price = new Price();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Prices.ToListAsync();
                foreach (Price str in _compl)
                {
                    _price = await db.Prices.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_price != null)
                    {
                        db.Prices.Remove(_price);
                        await db.SaveChangesAsync();
                    }

                }
                _price.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                {
                    _price = await db.Prices.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_price == null) { return NotFound(); }
                    try
                    {
                        db.Prices.Remove(_price);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.Prices.Any(x => x.Id == _price.Id)) { return BadRequest(); }
                    }
                }
                if (KeyPrice.Trim() != "0")
                {
                    var _compl = await db.Prices.Where(x => x.KeyPrice == KeyPrice).ToListAsync();
                    foreach (Price str in _compl)
                    {
                        _price = await db.Prices.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_price != null) db.Prices.Remove(_price);
                        await db.SaveChangesAsync();
                    }
                    _price.Id = 0;
                }

            }
            return Ok(_price);
        }
    }
}
