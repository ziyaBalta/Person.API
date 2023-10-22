
using AutoMapper;
using Person.Business.Dto;
using Person.Data;
using Person.Data.Models;
using Person.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Person.Business.Logic
{
    public class PersonsLogic
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        string _userId = "";

        public PersonsLogic(AppDbContext context, string userId)
        {
            _context = context;
            _userId = userId;

        }

        public int AddPersons(PersonsDto personsDto)
        {
            PersonsRepo personsRepo = new PersonsRepo(_context, _userId);

            var persons = _mapper.Map<Persons>(personsDto);
            var companyResult = personsRepo.Add(persons);
            return companyResult.Id;
        }

        public List<PersonsDto> GetAll()
        {
            PersonsRepo personRepo = new PersonsRepo(_context, _userId);

            var personResult = personRepo.GetAll();
            var person = _mapper.Map<List<PersonsDto>>(personResult);
            return person;
        }

    }
}
