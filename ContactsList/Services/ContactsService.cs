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
        
        public List<ContactDto> GetContacts(string filterSearch)
        {
            List<ContactDto> contacts;
            try
            {
                if (string.IsNullOrEmpty(filterSearch))
                {
                    contacts = _dbContext
                        .Contacts
                        .ProjectTo<ContactDto>()
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
                            x.BirthDate.HasValue && x.BirthDate.Value.ToString("dd MMM yyyy", new CultureInfo("en-US")).Contains(filterSearch) ||
                            x.ContactInfos.Any(xx => xx.Value.Contains(filterSearch)))
                        .ProjectTo<ContactDto>()
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

        private Contact GetContact(long id)
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
        
        public ContactFullDto GetContactFullInfo(long id)
        {
            ContactFullDto contact;
            try
            {
                contact = _dbContext.Contacts
                    .Include(x => x.ContactInfos)
                    .ProjectTo<ContactFullDto>()
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
        
        public void UpdateContact(UpdateContactDto updateContactDto)
        {
            var contact = GetContact(updateContactDto.Id);
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
        
        public void DeleteContact(long id)
        {
            try
            {
                var contact = GetContact(id);

                if (contact == null)
                {
                    throw new Exception("Contact doesn't exist");
                }
                
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