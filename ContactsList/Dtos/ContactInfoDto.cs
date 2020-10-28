using System.ComponentModel.DataAnnotations;
using ContactsList.Models;

namespace ContactsList.Dtos
{
    public class ContactInfoDto
    {
        [Required]
        [MaxLength(40)]
        public string Value { get; set; }
        [Required]
        public ContactInfoType Type { get; set; }
    }
}