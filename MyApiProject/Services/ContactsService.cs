
using Microsoft.EntityFrameworkCore;
using MyApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiProject.Services
{
    public class ContactsService
    {
        private readonly AppDbContext _context;

        public ContactsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Displayname))
            {
                contact.Displayname = $"{contact.Salutation} {contact.Firstname} {contact.Lastname}";
            }

            contact.CreationTimestamp = DateTime.UtcNow;
            contact.LastChangeTimestamp = DateTime.UtcNow;

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> UpdateContactAsync(int id, Contact contact)
        {
            var existingContact = await _context.Contacts.FindAsync(id);
            if (existingContact == null)
            {
                return null; // Or throw NotFoundException
            }

            // Update contact properties
            existingContact.Salutation = contact.Salutation;
            existingContact.Firstname = contact.Firstname;
            existingContact.Lastname = contact.Lastname;
            existingContact.Birthdate = contact.Birthdate;
            existingContact.Email = contact.Email;
            existingContact.Phonenumber = contact.Phonenumber;

            // Update displayname if necessary
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

            existingContact.LastChangeTimestamp = DateTime.UtcNow;

            _context.Entry(existingContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingContact;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return false;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}