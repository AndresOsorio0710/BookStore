using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Models;
using Backend.Infrastructure.API.Logic;
using System;

namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        private SesionLogic sesionLogic;

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            if (id == null)
            {
                return Ok("Id required.");
            }
            sesionLogic = new SesionLogic();
            if (sesionLogic.inSession(id))
            {
                return Ok("Ok.");
            }
            return Ok("No connect.");
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Sesion> LogIn([FromBody] Connect connect)
        {
            if (connect == null)
            {
                return Ok("Credentials required.");
            }
            sesionLogic = new SesionLogic();
            var sesion = this.sesionLogic.LogIn(connect.user, connect.password);
            if (sesion == null)
            {
                return Ok("Invalid credentials.");
            }
            return sesion;
        }

        [HttpDelete("logout/{id}")]
        public ActionResult LogOut(Guid id)
        {
            sesionLogic = new SesionLogic();
            sesionLogic.LogOut(id);
            return Ok("Successful transaction.");
        }

    }
}
