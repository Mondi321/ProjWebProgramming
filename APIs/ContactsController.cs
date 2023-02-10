using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjWebProgramming.Data;
using ProjWebProgramming.Models;

namespace ProjWebProgramming.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            return await _context.Contact.ToListAsync();
        }

        // GET: api/Contacts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Contact>> GetContact(Guid id)
        //{
        //    var contact = await _context.Contact.FindAsync(id);

        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return contact;
        //}

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutContact(Guid id, Contact contact)
        //{
        //    if (id != contact.ContactId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(contact).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ContactExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes ="Api")]
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteContact(Guid id)
        //{
        //    var contact = await _context.Contact.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Contact.Remove(contact);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool ContactExists(Guid id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
