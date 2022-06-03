using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Models
{
    public class Sesion
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }

        public Sesion()
        {
            this.Id = Guid.NewGuid();
            this.Role = "STANDARD";
        }
    }
}
