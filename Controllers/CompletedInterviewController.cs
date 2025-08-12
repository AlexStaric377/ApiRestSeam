using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/CompletedInterviewController")]
    [ApiController]
    public class CompletedInterviewController : ControllerBase
    {
        DbContextSeam db;
        public CompletedInterviewController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
                var users = db.AccountUsers.ToList();
            }
        }

        public async Task<ActionResult<CompletedInterview>> Get()
        {
            List<CompletedInterview> _content = await db.CompletedInterviews.OrderBy(x => x.KodComplInterv).ToListAsync();
            return Ok(_content);
        }

        // GET api/<ContentInterviewController>/5
        [HttpGet("{KodProtokola}/{id}")]
        public async Task<ActionResult<CompletedInterview>> Get(string KodProtokola,int Id)
        {
            if (KodProtokola.Trim() == "0" && Id ==0) { return NotFound(); }
            if (KodProtokola.Trim() == "0" && Id != 0)
            {
                CompletedInterview _content = new CompletedInterview();

                if (db.CompletedInterviews.Count() > 0)
                {
                    int ComplCount = db.CompletedInterviews.Count();

                    List<CompletedInterview> cmp = await db.CompletedInterviews.OrderBy(x => x.KodComplInterv).ToListAsync();
                    string maxid = cmp[ComplCount-1].KodComplInterv;
                   
                    _content = await db.CompletedInterviews.FirstOrDefaultAsync(x => x.KodComplInterv == maxid);               
                }

                return Ok(_content);
            }
            else
            {
                List<CompletedInterview>  _content = await db.CompletedInterviews.Where(x => x.KodComplInterv == KodProtokola).OrderBy(x => x.Numberstr).ToListAsync(); // .ThenBy(x => x.Numberstr)           
                return Ok(_content);
            }
 


        }

        // POST api/<ContentInterviewController>
        [HttpPost]
        public async Task<ActionResult<CompletedInterview>> Post(CompletedInterview _content)
        {
            if (_content == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.CompletedInterviews.Add(_content);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.CompletedInterviews.Any(x => x.Id != _content.Id)) { return NotFound(); }
            }
            return Ok(_content);
        }

        // PUT api/<ContentInterviewController>/5
        [HttpPut]
        public async Task<ActionResult<CompletedInterview>> Put(CompletedInterview _content)
        {

            if (_content == null) { return BadRequest(); }
            try
            {
                db.CompletedInterviews.Update(_content);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.CompletedInterviews.Any(x => x.Id != _content.Id)) { return NotFound(); }
            }
            return Ok(_content);

        }

        // DELETE api/<ContentInterviewController>/5
        [HttpDelete("{KodProtokola}/{id}")]
        public async Task<ActionResult<CompletedInterview>> Delete(string KodProtokola, string id)
        {
            CompletedInterview _content = new CompletedInterview();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.CompletedInterviews.ToListAsync();
                foreach (CompletedInterview str in _compl)
                {
                    _content = await db.CompletedInterviews.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_content != null)
                    {
                        db.CompletedInterviews.Remove(_content);
                        await db.SaveChangesAsync();
                    }
                }
                 _content.Id = 0;
            }
            else
            { 
                if (KodProtokola.Trim() == "0" && id == "0" ) { return NotFound(); }
                try
                {
                    if (KodProtokola.Trim() != "0")
                    {
                        _content = await db.CompletedInterviews.FirstOrDefaultAsync(x => x.KodComplInterv == KodProtokola);
                        if (_content != null)
                        { 
                            db.CompletedInterviews.RemoveRange(db.CompletedInterviews.Where(x => x.KodComplInterv == KodProtokola));
                        
                        }
                    }

                    if (id != "0")
                    {
                        _content = await db.CompletedInterviews.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        db.CompletedInterviews.Remove(_content);
                    }
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Convert.ToInt32(id) != 0)
                    {
                        if (db.CompletedInterviews.Any(x => x.Id == _content.Id)) { return BadRequest(); }
                    }
                    else
                    {
                        if (db.CompletedInterviews.Any(x => x.KodComplInterv == _content.KodComplInterv )) { return BadRequest(); }
                    }
                }           
            }
            return Ok(_content);
        }
    }
}
