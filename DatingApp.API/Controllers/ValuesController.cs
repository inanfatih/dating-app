using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    //Buradaki [controller] bir placeholder. yani localhost:5000/api/values seklinde kullaniliyor
    [Route("api/[controller]")] // Buna attribute based routing deniyormus 
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            // IEnumerable ==> a collection of things
            // IEnumerable<string> ==> a collection of strings

            // public ActionResult<IEnumerable<string>> Get() ===> Bu sadece IEnumarable return edecekti.  IActionResult a donusturduk ki HTTP request ile alakali donus yapsin
            // public IActionResult GetValues() ===> Bu synchronous 
            // public async Task<IActionResult> GetValues() ===> Bu asynchronous

            var values = await _context.Values.ToListAsync();

            return Ok(values);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            //Asagidaki "x=>" js teki arrow function ile ayni
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}