using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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
        
        public async Task<List<ContactDto>> GetContacts(string filterSearch, CancellationToken ct)
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
                    var isDateParsed = DateTime.TryParseExact(filterSearch, "dd MMM yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out var date);
                    
                    contacts = await _dbContext.Contacts
                        .Where(x =>
                            x.FirstName.Contains(filterSearch) ||
                            x.LastName.Contains(filterSearch) ||
                            x.MiddleName.Contains(filterSearch) ||
                            x.OrganizationName.Contains(filterSearch) ||
                            x.OrganizationPost.Contains(filterSearch) ||
                            isDateParsed && x.BirthDate.HasValue && x.BirthDate >= date && x.BirthDate < date.AddDays(1) ||
                            x.ContactInfos.Any(xx => xx.Value.Contains(filterSearch)))
                        .ProjectTo<ContactDto>()
                        .ToListAsync(ct);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }

            return contacts;
        }

        private async Task<Contact> GetContact(long id, CancellationToken ct)
        {
            Contact contact;
            try
            {
                contact = await _dbContext.Contacts
                    .Include(x => x.ContactInfos)
                    .FirstOrDefaultAsync(x => x.Id == id, ct);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }

            return contact;
        }
        
        public async Task<ContactFullDto> GetContactFullInfo(long id, CancellationToken ct)
        {
            ContactFullDto contact;
            try
            {
                contact = await _dbContext.Contacts
                    .Include(x => x.ContactInfos)
                    .ProjectTo<ContactFullDto>()
                    .FirstOrDefaultAsync(x => x.Id == id, ct);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }

            return contact;
        }

        public async Task AddContact(AddContactDto addContactDto, CancellationToken ct)
        {
            var newContact = _mapper.Map<Contact>(addContactDto);

            try
            {
                _dbContext.Add(newContact);
                await _dbContext.SaveChangesAsync(ct);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
        
        public async Task UpdateContact(UpdateContactDto updateContactDto, CancellationToken ct)
        {
            var contact = await GetContact(updateContactDto.Id, ct);
            _mapper.Map(updateContactDto, contact);
            
            try
            {
                _dbContext.Update(contact);
                await _dbContext.SaveChangesAsync(ct);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
        
        public async Task DeleteContact(long id, CancellationToken ct)
        {
            try
            {
                var contact = await GetContact(id, ct);

                if (contact == null)
                {
                    throw new Exception("Contact doesn't exist");
                }
                
                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync(ct);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{DateTime.Now:g}: {ex.Message}");
                throw;
            }
        }
    }
}