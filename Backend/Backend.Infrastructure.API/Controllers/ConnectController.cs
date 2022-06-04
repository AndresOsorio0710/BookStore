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
        private static Connectlogic connectlogic = new Connectlogic();

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            return Ok(connectlogic.Get(id));
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Sesion> LogIn([FromBody] Connect connect)
        {
            var sesion = connectlogic.LogIn(connect);
            if (sesion == null)
                return Ok("Invalid credentials.");
            return sesion;
        }

        [HttpDelete("logout/{id}")]
        public ActionResult LogOut(Guid id)
        {
            return Ok(connectlogic.LogOut(id));
        }

    }
}
