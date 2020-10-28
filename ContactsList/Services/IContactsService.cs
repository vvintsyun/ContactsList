using System.Collections.Generic;
using ContactsList.Dtos;
using ContactsList.Models;

namespace ContactsList.Services
{
    public interface IContactsService
    {
        List<Contact> GetContacts(string filterSearch);
        Contact GetContact(long id);
        void AddContact(AddContactDto addContactDto);
        void UpdateContact(Contact contact, UpdateContactDto updateContactDto);
        void DeleteContact(Contact contact);
    }
}