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
    [ApiKey]
    public class ReminderStepsController : ControllerBase
    {
        private readonly MyPhillReminderBDContext _context;

        public ReminderStepsController(MyPhillReminderBDContext context)
        {
            _context = context;
        }

        // GET: api/ReminderSteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderStep>>> GetReminderSteps()
        {
          if (_context.ReminderSteps == null)
          {
              return NotFound();
          }
            return await _context.ReminderSteps.ToListAsync();
        }

        // GET: api/ReminderSteps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderStep>> GetReminderStep(int id)
        {
          if (_context.ReminderSteps == null)
          {
              return NotFound();
          }
            var reminderStep = await _context.ReminderSteps.FindAsync(id);

            if (reminderStep == null)
            {
                return NotFound();
            }

            return reminderStep;
        }

        // PUT: api/ReminderSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminderStep(int id, ReminderStep reminderStep)
        {
            if (id != reminderStep.ReminderStepId)
            {
                return BadRequest();
            }

            _context.Entry(reminderStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderStepExists(id))
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

        // POST: api/ReminderSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReminderStep>> PostReminderStep(ReminderStep reminderStep)
        {
          if (_context.ReminderSteps == null)
          {
              return Problem("Entity set 'MyPhillReminderBDContext.ReminderSteps'  is null.");
          }
            _context.ReminderSteps.Add(reminderStep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminderStep", new { id = reminderStep.ReminderStepId }, reminderStep);
        }

        // DELETE: api/ReminderSteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminderStep(int id)
        {
            if (_context.ReminderSteps == null)
            {
                return NotFound();
            }
            var reminderStep = await _context.ReminderSteps.FindAsync(id);
            if (reminderStep == null)
            {
                return NotFound();
            }

            _context.ReminderSteps.Remove(reminderStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderStepExists(int id)
        {
            return (_context.ReminderSteps?.Any(e => e.ReminderStepId == id)).GetValueOrDefault();
        }
    }
}
