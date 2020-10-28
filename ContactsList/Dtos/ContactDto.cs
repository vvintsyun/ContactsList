using System;

namespace ContactsList.Dtos
{
    public class ContactDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string BirthDate { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationPost { get; set; }
    }
}