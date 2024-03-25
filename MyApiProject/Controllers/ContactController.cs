using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/contacts/{num}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //contact.CreationTimestamp = DateTime.Now;
            contact.CreationTimestamp = DateTime.UtcNow;
            contact.LastChangeTimestamp = DateTime.UtcNow;
            
            if (string.IsNullOrWhiteSpace(contact.Displayname))
            {
                contact.Displayname = $"{contact.Salutation} {contact.Firstname} {contact.Lastname}";
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        // PUT: api/contacts/{num}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var existingContact = await _context.Contacts.FindAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(contact.Displayname) &&
                    existingContact.Displayname.Contains(existingContact.Salutation) &&
                    existingContact.Displayname.Contains(existingContact.Firstname) &&
                    existingContact.Displayname.Contains(existingContact.Lastname))
            {
                existingContact.Displayname = $"{contact.Salutation} {contact.Firstname} {contact.Lastname}";
            }
            else if (contact.Displayname != null)
            {
                existingContact.Displayname = contact.Displayname;
            }
            existingContact.Salutation = contact.Salutation;
            existingContact.Firstname = contact.Firstname;
            existingContact.Lastname = contact.Lastname;
            
            existingContact.Birthdate = contact.Birthdate != null ? contact.Birthdate : existingContact.Birthdate;
            existingContact.Email = contact.Email;
            existingContact.Phonenumber = contact.Phonenumber!= null ? contact.Phonenumber : existingContact.Phonenumber;
            
            existingContact.LastChangeTimestamp = DateTime.UtcNow;
            _context.Entry(existingContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // DELETE: api/contacts/{num}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}


