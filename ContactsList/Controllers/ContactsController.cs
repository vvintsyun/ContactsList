using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactsList.Dtos;
using ContactsList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery] string filterString, CancellationToken ct)
        {
            var contacts = await _contactsService.GetContacts(filterString, ct);

            if (contacts == null)
            {
                return NotFound();
            }
            
            return Ok(contacts);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] long id, CancellationToken ct)
        {
            var contact = await _contactsService.GetContactFullInfo(id, ct);

            if (contact == null)
            {
                return NotFound();
            }
            
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] AddContactDto addContactDto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _contactsService.AddContact(addContactDto, ct);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] long id, [FromBody] UpdateContactDto updateContactDto,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateContactDto.Id)
            {
                return BadRequest();
            }

            await _contactsService.UpdateContact(updateContactDto, ct);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _contactsService.DeleteContact(id, ct);
            return Ok();
        }
    }
}