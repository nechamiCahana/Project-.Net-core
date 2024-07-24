using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        // GET: api/<BooksController>

        [HttpGet]
        public async Task<List<UserDto>> Get()
        {
            // _user.getAllBooks();
            List<UserDto> result =await _user.getAllUsers();
            return result;
        }

        // GET api/<BooksController>/5
        [HttpGet("GetById/{mail}")]
        public IActionResult Get(string mail)
        {
            var (status, user) = _user.getByMail(mail);
            if (user == null)
            {
                return NotFound(status);
            }
            return Ok(new { Status = status, User = user });
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult Post([FromBody] UserDto user)
        {
            var res = _user.createUser(user);
            //if (res)
                return Ok();
            //return BadRequest();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDto value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _user.deleteUser(id);
        }
    }
}
