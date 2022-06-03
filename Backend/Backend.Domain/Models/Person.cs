using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Genre { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Person()
        {
            this.Id = Guid.NewGuid();
            this.FirstName = "NOT SUPPLIED";
            this.LastName = "NOT SUPPLIED";
            this.Email = "NOT SUPPLIED";
            this.PhoneNumber = "NOT SUPPLIED";
            this.Address = "NOT SUPPLIED";
            this.Genre = "NOT SUPPLIED";
            this.BirthDate = DateTime.ParseExact("01/01/1990", "MM/dd/yyyy", null);
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = this.CreatedAt;
            this.DeletedAt = this.CreatedAt;
        }
    }
}
