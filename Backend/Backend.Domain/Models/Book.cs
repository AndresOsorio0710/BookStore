using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Book()
        {
            this.Id = Guid.NewGuid();
            this.Title = "NOT SUPPLIED";
            this.Description = "NOT SUPPLIED";
            this.Author = "NOT SUPPLIED";
            this.Publisher = "NOT SUPPLIED";
            this.Genre = "NOT SUPPLIED";
            this.Price = 0;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = this.CreatedAt;
            this.DeletedAt = this.CreatedAt;
        }
    }
}
