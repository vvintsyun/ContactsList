using System.Collections.Generic;
using ContactsList.Dtos;
using ContactsList.Models;

namespace ContactsList.Services
{
    public interface IContactsService
    {
        List<ContactDto> GetContacts(string filterSearch);
        ContactFullDto GetContactFullInfo(long id);
        void AddContact(AddContactDto addContactDto);
        void UpdateContact(UpdateContactDto updateContactDto);
        void DeleteContact(long id);
    }
}