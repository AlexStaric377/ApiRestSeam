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
    [Route("api/ControllerPayment")]
    [ApiController]
    public class ControllerPayment : ControllerBase
    {
        
        DbContextSeam db;

        public ControllerPayment(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }
        // GET: api/<ControllerPayment>
        [HttpGet]
        public async Task<ActionResult<Payment>> Get()
        {
            List<Payment> _payment = await db.Payments.OrderBy(x => x.KeyClient).OrderBy(x => x.KeyPrice).ToListAsync();
            return Ok(_payment);
        }

        // GET api/<ControllerPayment>/5
        [HttpGet("{KeyClient}/{KeyPrice}/{Telefon}")]
        public async Task<ActionResult<Payment>> Get(string KeyClient, string KeyPrice, string Telefon)
        {
            List<Payment> _listPayment = new List<Payment>();
            if (KeyClient == "0" && KeyPrice == "0" && Telefon.Trim() == "0") return NotFound();
            if (Telefon.Trim() != "0")
            {
                _listPayment = await db.Payments.Where(x => x.Telefon == Telefon).ToListAsync();
            }
            if (KeyPrice != "0")
            {
                _listPayment = await db.Payments.Where(x => x.KeyPrice == KeyPrice).OrderBy(x => x.KeyClient).ToListAsync();
            }

            if (KeyClient != "0")
            {
                _listPayment = await db.Payments.Where(x => x.KeyClient == KeyClient).ToListAsync(); ;
            }
            return Ok(_listPayment);

        }

        // POST api/<ControllerPayment>
        [HttpPost]
        public async Task<ActionResult<Payment>> Post(Payment _payment)
        {
            if (_payment == null) { return BadRequest(); }
            // Создание новой записи об оплате
            try
            {
                db.Payments.Add(_payment);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Payments.Any(x => x.Id != _payment.Id)) { return NotFound(); }
            }
            return Ok(_payment);
        }

        // PUT api/<ControllerPayment>/5
        [HttpPut]
        public async Task<ActionResult<Payment>> Put(Payment _payment)
        {
            if (_payment == null) { return BadRequest(); }
            try
            {
                db.Payments.Update(_payment);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Payments.Any(x => x.Id != _payment.Id)) { return NotFound(); }
            }
            return Ok(_payment);

        }

        // DELETE api/<ControllerPayment>/5
        [HttpDelete("{id}/{KeyClient}")]
        public async Task<ActionResult<Payment>> Delete(string id, string KeyClient)
        {
            Payment _payment = new Payment();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Payments.ToListAsync();
                foreach (Payment str in _compl)
                {
                    _payment = await db.Payments.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_payment != null)
                    {
                        db.Payments.Remove(_payment);
                        await db.SaveChangesAsync();
                    }

                }
                _payment.Id = 0;
            }
            else
            {
                if (Convert.ToInt32(id) > 0)
                {
                    _payment = await db.Payments.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                    if (_payment == null) { return NotFound(); }
                    try
                    {
                        db.Payments.Remove(_payment);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.Payments.Any(x => x.Id == _payment.Id)) { return BadRequest(); }
                    }
                }
                if (KeyClient.Trim() != "0")
                {
                    var _compl = await db.Payments.Where(x => x.KeyClient == KeyClient).ToListAsync();
                    foreach (Payment str in _compl)
                    {
                        _payment = await db.Payments.FirstOrDefaultAsync(x => x.Id == str.Id);
                        if (_payment != null) db.Payments.Remove(_payment);
                        await db.SaveChangesAsync();
                    }
                    _payment.Id = 0;
                }

            }
            return Ok(_payment);
        }
    }
}
