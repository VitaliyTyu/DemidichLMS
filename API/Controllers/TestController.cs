using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            return await _context.Tests.Include(t => t.Questions).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(Guid id)
        {
            var test = await _context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.Id == id);
            if (test == null) return NotFound();
            return test;
        }

        [HttpPost]
        public async Task<ActionResult<Test>> CreateTest(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(Guid id, Test test)
        {
            if (id != test.Id) return BadRequest();
            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null) return NotFound();
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}