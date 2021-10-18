using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContactsList.Dtos;

namespace ContactsList.Models
{
    public class Contact
    {
        public long Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string MiddleName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [MaxLength(50)]
        public string OrganizationName { get; set; }
        [MaxLength(50)]
        public string OrganizationPost { get; set; }
        
        public ICollection<ContactInfo> ContactInfos { get; set; }

        protected Contact()
        {
        }
        
        public Contact(AddContactDto addDto)
        {
            FirstName = addDto.FirstName;
            LastName = addDto.LastName;
            MiddleName = addDto.MiddleName;
            if (addDto.BirthDate.HasValue)
            {
                var birthDate = addDto.BirthDate.Value;
                BirthDate = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day);
            }
            OrganizationName = addDto.OrganizationName;
            OrganizationPost = addDto.OrganizationPost;
            ContactInfos = addDto.ContactInfos
                .Select(x => new ContactInfo { Value = x.Value, Type = x.Type })
                .ToList();
        }
    }
}