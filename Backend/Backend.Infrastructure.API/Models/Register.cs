using Backend.Domain.Models;

namespace Backend.Infrastructure.API.Models
{
    public class Register
    {
        public Person person { get; set; }
        public User user { get; set; }
    }
}
