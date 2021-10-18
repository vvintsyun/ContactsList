using System.ComponentModel.DataAnnotations;
using ContactsList.Dtos;

namespace ContactsList.Models
{
    public class ContactInfo
    {
        public long Id { get; set; }
        [Required]
        public long ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        
        
        [Required]
        [MaxLength(40)]
        public string Value { get; set; }
        [Required]
        public ContactInfoType Type { get; set; }
    }

    public enum ContactInfoType: byte
    {
        Telephone,
        Email,
        Skype,
        Other
    }
}