using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManager _manager;
        public ManagerController(IManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public List<ManagerDto> Get()
        {

            List<ManagerDto> result = _manager.getAllManager();
            return result;
        }




        // POST api/<BooksController>
        [HttpPost]
        public ActionResult Post([FromBody] ManagerDto manager)
        {
            var res = _manager.addManager(manager);
            //if (res)
                return Ok();
            //return BadRequest();
        }
    }
}
