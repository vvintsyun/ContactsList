using System.Collections.Generic;
using ContactsList.Models;

namespace ContactsList.Dtos
{
    public class ContactFullDto : ContactDto
    {
        public List<ContactInfoDto> ContactInfos { get; set; }
    }
}