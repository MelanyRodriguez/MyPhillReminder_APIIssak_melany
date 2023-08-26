using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPhillReminder_APIIssak_melany.Attributes;
using MyPhillReminder_APIIssak_melany.Models;

namespace MyPhillReminder_APIIssak_melany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]

    public class PillRemindersController : ControllerBase
    {
        private readonly MyPhillReminderBDContext _context;

        public PillRemindersController(MyPhillReminderBDContext context)
        {
            _context = context;
        }

        // GET: api/PillReminders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PillReminder>>> GetPillReminders()
        {
          if (_context.PillReminders == null)
          {
              return NotFound();
          }
            return await _context.PillReminders.ToListAsync();
        }

        // GET: api/PillReminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PillReminder>> GetPillReminder(int id)
        {
          if (_context.PillReminders == null)
          {
              return NotFound();
          }
            var pillReminder = await _context.PillReminders.FindAsync(id);

            if (pillReminder == null)
            {
                return NotFound();
            }

            return pillReminder;
        }
        [HttpGet("GetpillreminderListByUser")]
        public async Task<ActionResult<IEnumerable<PillReminder>>> GetpillreminderListByUser(int id)
        {
            if (_context.PillReminders == null)
            {
                return NotFound();
            }
            var PillReminderList = await _context.PillReminders.Where(p => p.Equals(id)).ToListAsync();



            if (PillReminderList == null)
            {
                return NotFound();
            }



            return PillReminderList;
        }

    

                // PUT: api/PillReminders/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
        public async Task<IActionResult> PutPillReminder(int id, PillReminder pillReminder)
        {
            if (id != pillReminder.ReminderId)
            {
                return BadRequest();
            }

            _context.Entry(pillReminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PillReminderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PillReminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PillReminder>> PostPillReminder(PillReminder pillReminder)
        {
          if (_context.PillReminders == null)
          {
              return Problem("Entity set 'MyPhillReminderBDContext.PillReminders'  is null.");
          }
            _context.PillReminders.Add(pillReminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPillReminder", new { id = pillReminder.ReminderId }, pillReminder);
        }

        // DELETE: api/PillReminders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePillReminder(int id)
        {
            if (_context.PillReminders == null)
            {
                return NotFound();
            }
            var pillReminder = await _context.PillReminders.FindAsync(id);
            if (pillReminder == null)
            {
                return NotFound();
            }

            _context.PillReminders.Remove(pillReminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PillReminderExists(int id)
        {
            return (_context.PillReminders?.Any(e => e.ReminderId == id)).GetValueOrDefault();
        }
    }
}
