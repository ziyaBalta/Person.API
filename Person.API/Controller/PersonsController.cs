using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Person.Business.Dto;
using Person.Business.Logic;
using Person.Data;
using Person.Data.Auth;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Person.API.Controller
{
    
    [Route("api/[controller]")]
    [ApiController] 
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        string _userId = "";


        public PersonsController( AppDbContext context, IMapper mapper, string userId)
        {
            _context = context;
            _mapper = mapper;
            _userId = userId;

        }

        //[HttpPost]
        //[Route("add")]
        //[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        //public int Add(PersonsDto person)
        //{
        //    PersonsLogic personsLogic = new PersonsLogic(_context, _userId);
        //    var personsId = personsLogic.AddPersons(person);
        //    return personsId;
        //}


        [HttpGet]
        [Route("getall")]
        public List<PersonsDto> GetAll()
        {
            PersonsLogic personLogic = new PersonsLogic(_context, _userId, _mapper);
            var persons = personLogic.GetAll();
            return persons;
        }

    }
}
