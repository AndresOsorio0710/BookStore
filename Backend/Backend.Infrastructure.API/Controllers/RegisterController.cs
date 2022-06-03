using Microsoft.AspNetCore.Mvc;
using Backend.Aplications.Services.People;
using Backend.Aplications.Services.Users;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Models;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.People;
using Backend.Infrastructure.Data.Repositories.Users;


namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        PersonService CreatePersonService()
        {
            BookAppContext db = new BookAppContext();
            PersonRepository personRepository = new PersonRepository(db);
            PersonService personService = new PersonService(personRepository);
            return personService;
        }

        UserService CreateUserService()
        {
            BookAppContext db = new BookAppContext();
            UserRepository userRepository = new UserRepository(db);
            UserService userService = new UserService(userRepository);
            return userService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Register register)
        {
            if (register.person == null)
            {
                return Ok("The Person is requited.");
            }
            if (register.user == null)
            {
                return Ok("The User is requited.");
            }
            var personService = this.CreatePersonService();
            Person newPerson = personService.Add(register.person);
            if (newPerson == null)
            {
                return Ok("The record already exists.");
            }
            else
            {
                var userService = this.CreateUserService();
                register.user.PeopleId = newPerson.Id;
                User newUser = userService.Add(register.user);
                if (newUser == null)
                {
                    personService.Delete(newPerson.Id);
                    return Ok("The record already exists.");
                }
            }
            return Ok("Successful registrations.");
        }
    }
}
