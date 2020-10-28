using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string OrganizationName { get; set; }
        [Required]
        [MaxLength(50)]
        public string OrganizationPost { get; set; }
        
        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}