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
        private readonly IMapper _mapper;

        public ContactsController(IContactsService contactsService, IMapper mapper)
        {
            _contactsService = contactsService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetContacts([FromQuery] string filterString)
        {
            var contacts = _contactsService.GetContacts(filterString);

            if (contacts == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<ContactDto>>(contacts);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetContact([FromRoute] long id)
        {
            var contact = _contactsService.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ContactFullDto>(contact);
            return Ok(result);
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

            var contact = _contactsService.GetContact(id);
            _contactsService.UpdateContact(contact, updateContactDto);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteContact([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = _contactsService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            _contactsService.DeleteContact(contact);
            return Ok();
        }
    }
}