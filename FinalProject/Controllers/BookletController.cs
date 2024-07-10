using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookletController : ControllerBase
    {
        private readonly IBooklet _bookStore;

        public BookletController(IBooklet bookStore)
        {
            _bookStore = bookStore;
        }

        [HttpPost]
        public ActionResult Post([FromBody] BookletDto newBooklet)
        {
            var res = _bookStore.CreateBooklet(newBooklet);
            if (res!=null)
                return Ok();
            return BadRequest("Failed to create booklet");
        }
    }
}
