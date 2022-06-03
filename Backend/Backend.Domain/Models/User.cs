using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public Guid PeopleId { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid();
            this.Name = "NOT SUPPLIED";
            this.Email = "NOT SUPPLIED";
            this.Password = "a7fe4f3e1584626f36aaaf4fb5752b3d";
            this.Role = "STANDARD";
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = this.CreatedAt;
            this.DeletedAt = this.CreatedAt;
        }
    }
}
