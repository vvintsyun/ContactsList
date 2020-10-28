using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactsList.Dtos;
using ContactsList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactsList.Services
{
    public class ContactsService : IContactsService
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IContactsService> _logger;

        public ContactsService(Context dbContext, IMapper mapper, ILogger<IContactsService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        
        public List<Contact> GetContacts(string filterSearch)
        {
            List<Contact> contacts;
            try
            {
                if (string.IsNullOrEmpty(filterSearch))
                {
                    contacts = _dbContext.Contacts
                        .ToList();
                }
                else
                {
                    contacts = _dbContext.Contacts
                        .Where(x =>
                            x.FirstName.Contains(filterSearch) ||
                            x.LastName.Contains(filterSearch) ||
                            x.MiddleName.Contains(filterSearch) ||
                            x.OrganizationName.Contains(filterSearch) ||
                            x.OrganizationPost.Contains(filterSearch) ||
                            x.BirthDate.ToString("dd MMM yyyy", new CultureInfo("en-US")).Contains(filterSearch) ||
                            x.ContactInfos.Any(xx => xx.Value.Contains(filterSearch)))
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }

            return contacts;
        }

        public Contact GetContact(long id)
        {
            Contact contact;
            try
            {
                contact = _dbContext.Contacts
                    .Include(x => x.ContactInfos)
                    .FirstOrDefault(x => x.Id == id);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }

            return contact;
        }

        public void AddContact(AddContactDto addContactDto)
        {
            var newContact = _mapper.Map<Contact>(addContactDto);

            try
            {
                _dbContext.Add(newContact);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
        
        public void UpdateContact(Contact contact, UpdateContactDto updateContactDto)
        {
            _mapper.Map(updateContactDto, contact);
            
            try
            {
                _dbContext.Update(contact);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
        
        public void DeleteContact(Contact contact)
        {
            try
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
    }
}