using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContactsList.Dtos;
using ContactsList.Models;

namespace ContactsList.Services
{
    public interface IContactsService
    {
        Task<List<ContactDto>> GetContacts(string filterSearch, CancellationToken ct);
        Task<ContactFullDto> GetContactFullInfo(long id, CancellationToken ct);
        Task AddContact(AddContactDto addContactDto, CancellationToken ct);
        Task UpdateContact(UpdateContactDto updateContactDto, CancellationToken ct);
        Task DeleteContact(long id, CancellationToken ct);
    }
}