using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Business.Dto;
using Person.Business.Logic;
using Person.Data;
using System.Net;

namespace Person.API.Controller
{
    
    [Route("api/[controller]")]
    [ApiController] 
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        string _userId = "";

        public PersonController(AppDbContext context, IMapper mapper, string userId)
        {
            _context = context;
            _mapper = mapper;
            _userId = userId;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public int Add(PersonsDto person)
        {
            PersonsLogic personsLogic = new PersonsLogic(_context, _userId);
            var personsId = personsLogic.AddPersons(person);
            return personsId;
        }


        public List<PersonsDto> GetAll()
        {
            PersonsLogic personLogic = new PersonsLogic(_context, _userId);
            var persons = personLogic.GetAll();
            return persons;
        }




    }
}
