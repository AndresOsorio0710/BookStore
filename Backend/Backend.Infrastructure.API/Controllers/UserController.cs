using Backend.Aplications.Services.Users;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Logic;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private SesionLogic sesionLogic = new SesionLogic();

        private UserService CreateUserService()
        {
            BookAppContext db = new BookAppContext();
            UserRepository userRepository = new UserRepository(db);
            UserService userService = new UserService(userRepository);
            return userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get([FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                    {
                        var service = this.CreateUserService();
                        return Ok(service.GetAll());
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpGet("{id}")]
        public ActionResult<Object> Get(Guid id, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN") || this.sesionLogic.ValidateOwner(sesionId, id))
                    {
                        var service = this.CreateUserService();
                        User user = service.Get(id);
                        var result = new
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Role = user.Role,
                            CreatedAt = user.CreatedAt,
                            UpdatedAt = user.UpdatedAt,
                            DeletedAt = user.DeletedAt,
                            PeopleId = user.PeopleId
                        };
                        return Ok(result);
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }
    }
}
