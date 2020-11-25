using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ContactsList.Models;

namespace ContactsList.Dtos
{
    public class AddContactDto
    {
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(50)]
        public string OrganizationName { get; set; }
        [MaxLength(50)]
        public string OrganizationPost { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set; }
    }
}