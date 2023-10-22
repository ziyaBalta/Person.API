using Person.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Person.Data.Repo
{
    public class PersonsRepo
    {
        private readonly AppDbContext _context;
        private string _userId;


        public PersonsRepo(AppDbContext context, string userId)
        {
            _context = context;
            _userId = userId;
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
