using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRestSeam.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/ApiControllerComplaint")]
    [ApiController]
    public class ApiControllerComplaint : ControllerBase
    {

        DbContextSeam db;

        public ApiControllerComplaint(DbContextSeam context)
        {
            db = context;
            if (!db.Pacients.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }
        }


        // GET: api/<ApiControllerComplaint>
        [HttpGet]
        public async Task<ActionResult<Complaint>> Get()
        {
            List<Complaint> _complaint = await db.Complaints.ToListAsync(); //.OrderBy(x => x.KeyComplaint)
            return Ok(_complaint);

        }
 
        // GET api/<ApiControllerComplaint>/5
        [HttpGet("{KeyComplaint}/{PoiskComplaint}")]
        public async Task<ActionResult<Complaint>> Get(string KeyComplaint, string PoiskComplaint)
        {

            if (KeyComplaint == "0" && PoiskComplaint == "0") { return NotFound(); }
            if (KeyComplaint == "0")
            {
                List<Complaint> _listComplaint = await db.Complaints.Where(x => x.Name.Contains(PoiskComplaint)).ToListAsync();
                return Ok(_listComplaint);
            }
            Complaint _complaint = await db.Complaints.FirstOrDefaultAsync(x => x.KeyComplaint == KeyComplaint);
            return Ok(_complaint);

        }
 
        // POST api/<ApiControllerComplaint>
        [HttpPost]
        public async Task<ActionResult<Complaint>> Post(Complaint _complaint)
        {
            if (_complaint == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.Complaints.Add(_complaint);
                await db.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Complaints.Any(x => x.Id != _complaint.Id)) { return NotFound(); }

            }
            return Ok(_complaint);
        }
   
        // PUT api/<ApiControllerComplaint>/5
        [HttpPut]
        public async Task<ActionResult<Complaint>> Put(Complaint _complaint)
        {

            if (_complaint == null) { return BadRequest(); }
            try
            {
                db.Complaints.Update(_complaint);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Complaints.Any(x => x.Id != _complaint.Id)) { return NotFound(); }

            }
            return Ok(_complaint);

        }
 
        // DELETE api/<ApiControllerComplaint>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Complaint>> Delete(string id)
        {
            Complaint _complaint = new Complaint();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.Complaints.ToListAsync();
                foreach (Complaint str in _compl)
                {
                    _complaint = await db.Complaints.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_complaint != null)
                    { 
                         db.Complaints.Remove(_complaint);
                        await db.SaveChangesAsync();                   
                    }

                }
                _complaint.Id = 0;
            }
            else
            {
                _complaint = await db.Complaints.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                if (_complaint == null) { return NotFound(); }
                try
                {
                    db.Complaints.Remove(_complaint);
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Complaints.Any(x => x.Id == _complaint.Id)) { return BadRequest(); }

                }
            }

            return Ok(_complaint);
        }
    
    }
}
