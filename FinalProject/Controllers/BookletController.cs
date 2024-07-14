using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookletController : ControllerBase
    {
        private readonly IBooklet _bookletStore;

        public BookletController(IBooklet bookletStore)
        {
            _bookletStore = bookletStore;
        }

        [HttpPost]
        public ActionResult Post([FromBody] BookletDto newBooklet)
        {
            var res = _bookletStore.CreateBooklet(newBooklet);
            if (res!=null)
                return Ok();
            return BadRequest("Failed to create booklet");
        }

        [HttpGet]
        public async Task<List<BookletDto>> Get()
        {
            List<BookletDto> result =await _bookletStore.GetAllBooklet();
            return result;
        }

        [HttpGet("{name}")]
        public async Task<BookletDto> Get([FromBody] string name)
        {
            BookletDto result = await _bookletStore.GetBookletByName(name);
            return result;         
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] double price)
        {
            _bookletStore.UpdatePrice(price, id);
            return Ok();

        }

        [HttpDelete("{id}")]
        public void Delete([FromBody] int id)
        {
            _bookletStore.DeleteBooklet(id);
        }

    }
}
