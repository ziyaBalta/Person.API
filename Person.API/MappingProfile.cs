using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using Person.Data.Models;
using Person.Business.Dto;

namespace Person.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContactInformation, ContactInformationDto>().ReverseMap();
            CreateMap<Persons, PersonsDto>().ReverseMap();
  


        }
    }
}
