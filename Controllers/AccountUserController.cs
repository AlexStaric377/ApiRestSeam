using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/AccountUserController")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {
        DbContextSeam db;
        public AccountUserController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }

        // GET: api/<AccountUserController>
        [HttpGet]
        public async Task<ActionResult<AccountUser>> Get()
        {
            List<AccountUser> _detailing = await db.AccountUsers.OrderBy(x => x.IdUser).ToListAsync();
            return Ok(_detailing);

        }

        // GET api/<AccountUserController>/5
        [HttpGet("{IdUser}/{Login}/{Password}/{PoiskUser}")]
        public async Task<ActionResult<AccountUser>> Get(string IdUser, string Login, string Password, string PoiskUser)
        {
            AccountUser _detailing = new AccountUser();
            if (IdUser.Trim() == "0" && Login.Trim() == "0" && Password.Trim() == "0" && PoiskUser.Trim() == "0") { return NotFound(); }

            if (PoiskUser.Trim() != "0")
            { 
                List <AccountUser> _listdetailing = await db.AccountUsers.Where(x => x.Login.Contains(PoiskUser)).ToListAsync();
                return Ok(_listdetailing);
            } 

            if (IdUser.Trim() != "0")
            { 
                _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.IdUser == IdUser);
            }
            
            if (Login.Trim() != "0" && Password.Trim() != "0")
            {
                _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.Login == Login && x.Password == Password);
            }
            if (Login.Trim() != "0" && Password.Trim() == "0")
            {
                _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.Login == Login );
            }
            return Ok(_detailing);
        }

        // POST api/<AccountUserController>
        [HttpPost]
        public async Task<ActionResult<AccountUser>> Post(AccountUser _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            // Создание новой карты жалобы
            try
            {
                db.AccountUsers.Add(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AccountUsers.Any(x => x.Id != _detailing.Id)) { return NotFound(); }

            }

            return Ok(_detailing);
        }


        // PUT api/<AccountUserController>/5
        [HttpPut]
        public async Task<ActionResult<AccountUser>> Put(AccountUser _detailing)
        {
            if (_detailing == null) { return BadRequest(); }
            try
            {
                db.AccountUsers.Update(_detailing);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AccountUsers.Any(x => x.Id != _detailing.Id)) { return NotFound(); }
            }
            return Ok(_detailing);

        }

        // DELETE api/<AccountUserController>/5
        [HttpDelete("{id}/{IdUser}")]
        public async Task<ActionResult<AccountUser>> Delete(string id, string IdUser)
        {
            AccountUser _detailing = new AccountUser();
            if (Convert.ToInt32(id) == -1)
            {
                var _compl = await db.AccountUsers.ToListAsync();
                foreach (AccountUser str in _compl)
                {
                    _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.Id == str.Id);
                    if (_detailing != null)
                    {
                        db.AccountUsers.Remove(_detailing);
                        await db.SaveChangesAsync();
                    }
                }
                _detailing.Id = 0;
            }
            else
            { 
                if (IdUser.Trim() == "0" && id == "0") { return NotFound(); }
                try
                {
                    if (id == "0")
                    {
                        _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.IdUser == IdUser);
                        if (_detailing != null) db.AccountUsers.RemoveRange(db.AccountUsers.Where(x => x.IdUser == IdUser));

                    }
                    else
                    {
                        _detailing = await db.AccountUsers.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(id));
                        if (_detailing != null) db.AccountUsers.Remove(_detailing);
                    }
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Convert.ToInt32(id) != 0)
                    {
                        if (db.AccountUsers.Any(x => x.Id == _detailing.Id)) { return BadRequest(); }
                    }
                    else
                    {
                        if (db.AccountUsers.Any(x => x.IdUser == _detailing.IdUser)) { return BadRequest(); }
                    }

                
                }           
            }

            return Ok(_detailing);
        }
    }
}
