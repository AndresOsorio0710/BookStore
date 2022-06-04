using Backend.Domain.Models;
using Backend.Infrastructure.API.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserLogic userLogic = new UserLogic();

        [HttpGet]
        public ActionResult<List<User>> Get([FromHeader] Guid sesionId)
        {
            var books = userLogic.Get(sesionId);
            if (books == null)
                return Ok("Access error.");
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Object> Get(Guid id, [FromHeader] Guid sesionId)
        {
            var books = userLogic.Get(sesionId, id);
            if (books == null)
                return Ok("Access error.");
            return Ok(books);
        }
    }
}
