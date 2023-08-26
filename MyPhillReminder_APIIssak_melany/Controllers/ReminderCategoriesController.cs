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
    public class ReminderCategoriesController : ControllerBase
    {
        private readonly MyPhillReminderBDContext _context;

        public ReminderCategoriesController(MyPhillReminderBDContext context)
        {
            _context = context;
        }

        // GET: api/ReminderCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderCategory>>> GetReminderCategories()
        {
          if (_context.ReminderCategories == null)
          {
              return NotFound();
          }
            return await _context.ReminderCategories.ToListAsync();
        }

        // GET: api/ReminderCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderCategory>> GetReminderCategory(int id)
        {
          if (_context.ReminderCategories == null)
          {
              return NotFound();
          }
            var reminderCategory = await _context.ReminderCategories.FindAsync(id);

            if (reminderCategory == null)
            {
                return NotFound();
            }

            return reminderCategory;
        }

        // PUT: api/ReminderCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminderCategory(int id, ReminderCategory reminderCategory)
        {
            if (id != reminderCategory.ReminderCategory1)
            {
                return BadRequest();
            }

            _context.Entry(reminderCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderCategoryExists(id))
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

        // POST: api/ReminderCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReminderCategory>> PostReminderCategory(ReminderCategory reminderCategory)
        {
          if (_context.ReminderCategories == null)
          {
              return Problem("Entity set 'MyPhillReminderBDContext.ReminderCategories'  is null.");
          }
            _context.ReminderCategories.Add(reminderCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminderCategory", new { id = reminderCategory.ReminderCategory1 }, reminderCategory);
        }

        // DELETE: api/ReminderCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminderCategory(int id)
        {
            if (_context.ReminderCategories == null)
            {
                return NotFound();
            }
            var reminderCategory = await _context.ReminderCategories.FindAsync(id);
            if (reminderCategory == null)
            {
                return NotFound();
            }

            _context.ReminderCategories.Remove(reminderCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderCategoryExists(int id)
        {
            return (_context.ReminderCategories?.Any(e => e.ReminderCategory1 == id)).GetValueOrDefault();
        }
    }
}
