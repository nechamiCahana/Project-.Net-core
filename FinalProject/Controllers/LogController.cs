
using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MODELS.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
public struct Login
{
    public string mail { get; set; }
    public string password { get; set; }

    public Login(string Mail, string Password)
    {
        mail = Mail;
        password = Password;
    }
}
public struct Register
{
    public string mail { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    public string address { get; set; }

    public Register(string Mail, string Password, string Name, string Address)
    {
        mail = Mail;
        password = Password;
        name = Name;
        address = Address;
    }
}
namespace FinalProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {

        private IConfiguration _config;
        private readonly IUser _user;
        private readonly IManager _manager;


        public LogController(IConfiguration config, IUser user, IManager manager)
        {
            _config = config;
            _user = user;
            _manager = manager;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Login loginRequest)
        {
            var userFind = _user.getByMail(loginRequest.mail);


            if (userFind.afterMapper == null)
            {
                return BadRequest("User not found");
            }
            var ManagerFind = _manager.getAllManager().FirstOrDefault(x => x.userId == userFind.afterMapper.Id);

            if (userFind.afterMapper.password == loginRequest.password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                string role = "User";
                if (ManagerFind != null)
                    role = "Admin";

                var claims = new[]
                {
                   new Claim(JwtRegisteredClaimNames.Sub, loginRequest.mail),
                   new Claim("role", role)
                 };
                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid password");
            }
        }
        [HttpPost("add new manager for first visit")]
        public IActionResult Register([FromBody] Register registerRequest)
        {
            if (_manager.getAllManager().Count() > 0)
            {
                return BadRequest("Admin already exists");
            }

            UserDto newUser = new UserDto
            {
                mail = registerRequest.mail,
                password = registerRequest.password,
                address = registerRequest.address,
                Name = registerRequest.name


            };

            _user.createUser(newUser);

            var newManager = new ManagerDto
            {
                userId = newUser.Id,

            };

            _manager.addManager(newManager);

            return Ok("Admin registered successfully");
        }

    }
}
