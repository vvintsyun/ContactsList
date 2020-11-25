using System.Globalization;
using AutoMapper;
using ContactsList.Dtos;
using ContactsList.Models;

namespace ContactsList.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddContactDto, Contact>();

            CreateMap<Contact, ContactFullDto>()
                .IncludeBase<Contact, ContactDto>();
            CreateMap<Contact, UpdateContactDto>();
            
            CreateMap<ContactInfoDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoDto>();
        }
    }
}