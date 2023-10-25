using Person.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Person.Data.Repo
{
    public class PersonsRepo
    {
        private readonly AppDbContext _context;
        private string _userId;
        private readonly IMapper _mapper;


        public PersonsRepo(AppDbContext context, string userId , IMapper mapper)
        {
            _context = context;
            _userId = userId;
            _mapper = mapper;
        }

        public Persons Add(Persons persons)
        {
            persons.SetCreate(_userId);
            _context.Add(persons);
            _context.SaveChanges();
            return persons;
        }

        public List<Persons> GetAll()
        {
            var Persons = _context.Persons.ToList();
            return Persons;
        }


    }
}
