using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Models;
using Backend.Infrastructure.API.Logic;


namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private static RegisterLogic registerLogic = new RegisterLogic();

        [HttpPost]
        public ActionResult Post([FromBody] Register register)
        {
            return Ok(registerLogic.Register(register)); ;
        }
    }
}
