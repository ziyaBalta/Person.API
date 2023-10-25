using AutoMapper;
using System.Drawing;
using Person.Data.Models;
using Person.Business.Dto;
using Person.Data.Auth;

namespace Person.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContactInformation, ContactInformationDto>().ReverseMap();
            CreateMap<Persons, PersonsDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();



        }
    }
}
