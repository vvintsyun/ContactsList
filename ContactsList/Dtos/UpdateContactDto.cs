using System.ComponentModel.DataAnnotations;

namespace ContactsList.Dtos
{
    public class UpdateContactDto : AddContactDto
    {
        [Required]
        public long Id { get; set; }
    }
}