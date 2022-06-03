using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Models;
using Backend.Infrastructure.Data.Configs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Contexts
{
    public class BookAppContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Sesion> Sesions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=DESKTOP-8VSJU2D;Initial Catalog=db_book_app;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BookConfig());
            builder.ApplyConfiguration(new PersonConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new SesionConfig());
        }


    }
}
