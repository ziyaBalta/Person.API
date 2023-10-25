using Microsoft.AspNetCore.Identity;       
using Microsoft.AspNetCore.Mvc;           
using Person.Data;                        
using Microsoft.EntityFrameworkCore;      
using Person.Business.Dto;                
using Person.Business.Logic;              
using AutoMapper;                         
using Person.Data.Auth;                   
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace Person.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly PrjSettings _prjSettings;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        string _userId = "";


        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _prjSettings = configuration.GetSection(nameof(PrjSettings)).Get<PrjSettings>();
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public List<PersonsDto> GetAll()
        {
            PersonsLogic personLogic = new PersonsLogic(_context, _userId , _mapper);
            var persons = personLogic.GetAll();
            return persons; 
        }

        [HttpPost]
        [Route("add")]
        public int Add(PersonsDto person)
        {
            PersonsLogic personsLogic = new PersonsLogic(_context, _userId , _mapper);
            var personsId = personsLogic.AddPersons(person);
            return personsId;
        }




        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
 {


            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,

                });
            }
            return Unauthorized();

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {



            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                throw new Exception("Kullanıcı daha önceden tanımlanmış");

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Username,
                Name = model.Name,
                Surname = model.SurName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Kullanıcı oluşturulamadı! Bilgileri kontrol edip tekrar deneyiniz..");

            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return Ok("Kullanıcı Oluşturuldu");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_prjSettings.JwtSecret));

            var token = new JwtSecurityToken(
                issuer: _prjSettings.ValidIssuer,
                audience: _prjSettings.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
