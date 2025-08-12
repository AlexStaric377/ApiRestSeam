using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Windows;


/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRestSeam.Controllers
{
    [Route("api/UnLoadController")]
    [ApiController]
    public class UnLoadController : ControllerBase
    {

        DbContextSeam db;
        public UnLoadController(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                StartLoadDb LoadDb = new();
                LoadDb.AddDb(db);
            }

        }
        // GET: api/<UnLoadController>
        [HttpGet]
        public JsonResult Get()
        {
            string model = "";
            string OutFile = Program.UnloadString + model + ".json";
            string[] fileLines = System.IO.File.ReadAllLines(OutFile);
            return new JsonResult(fileLines);
        }

        // GET api/<UnLoadController>/5
        [HttpGet("{model}/{stroka}/{id}")]
        public JsonResult Get(string model,string stroka, string id)
        {
            string OutFile = Program.UnloadString + model + ".json";

            if (!(Directory.Exists(Program.UnloadString)))
            {
                Directory.CreateDirectory(Program.UnloadString);
            }
            if (!(System.IO.File.Exists(OutFile)))
            {
                try
                {
                    using (FileStream NewFile = System.IO.File.Create(OutFile))
                    {
                        NewFile.Close();
                    }
                }
                catch (Exception) //error
                {
                    stroka = "";
                    return new JsonResult(stroka);
                }
            }

            if (stroka == "0")
            {
                Encoding code = Encoding.Default;
                string[] fileLines = System.IO.File.ReadAllLines(OutFile, code);
                return new JsonResult(fileLines);
            }
            using (StreamWriter writer = new StreamWriter(OutFile, true))
            {
                
                writer.WriteLineAsync(stroka);
                writer.Close();
                return new JsonResult(stroka);
            }
            
            
           
        }

        // POST api/<UnLoadController>
        [HttpPost ("{model}/{stroka}")]

        public JsonResult Post(string model, string stroka)
        {
            return new JsonResult(stroka);
        }

        // PUT api/<UnLoadController>/5
        [HttpPut("{model}/{stroka}/{id}")]
        public JsonResult Put(string model, string stroka, string id)
        {
            return new JsonResult(stroka);
        }

        // DELETE api/<UnLoadController>/5
        [HttpDelete("{model}/{stroka}/{id}")]
        public JsonResult Delete(string model, string stroka, string id)
        {
            return new JsonResult(stroka);
        }
    }
}
