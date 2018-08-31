using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : Controller
    {
        private readonly DataContext _context;
        public ValuesController(DataContext contest)
        {
            this._context  = contest;
        }
        // GET api/values
        [HttpGet]
        public async Task<List<Value>> GetValues()
        {
            var values =  await _context.Values.ToListAsync();
            return(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value =  await _context.Values.FirstOrDefaultAsync(x => x.Id == id );
            return Ok(value);
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
