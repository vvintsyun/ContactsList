using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetContacts([FromQuery] string filterString)
        {
            var contacts = _contactsService.GetContacts(filterString);

            if (contacts == null)
            {
                return NotFound();
            }
            
            return Ok(contacts);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetContact([FromRoute] long id)
        {
            var contact = _contactsService.GetContactFullInfo(id);

            if (contact == null)
            {
                return NotFound();
            }
            
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] AddContactDto addContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _contactsService.AddContact(addContactDto);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateContact([FromRoute] long id, [FromBody] UpdateContactDto updateContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateContactDto.Id)
            {
                return BadRequest();
            }

            _contactsService.UpdateContact(updateContactDto);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contactsService.DeleteContact(id);
            return Ok();
        }
    }
}