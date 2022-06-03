using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Contexts;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories.People
{
    public class PersonRepository : IRepositoryBase<Person, Guid>
    {
        private BookAppContext db;

        public PersonRepository(BookAppContext db)
        {
            this.db = db;
        }

        private Person Get(Person person)
        {
            var result = this.db.People.Where(p => (p.Email == person.Email || p.PhoneNumber == person.PhoneNumber)).FirstOrDefault();
            return result;
        }

        private Person GetById(Guid id)
        {
            var result = this.db.People.Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public Person Add(Person entity)
        {
            if (this.Get(entity) == null)
            {
                Person person = new Person();
                person.Id = Guid.NewGuid();
                person.FirstName = entity.FirstName.Trim().ToUpper();
                person.LastName = entity.LastName.Trim().ToUpper();
                person.Email = entity.Email.Trim();
                person.PhoneNumber = entity.PhoneNumber.Trim();
                person.Genre = entity.Genre.Trim().ToUpper();
                this.db.Add(person);
                return person;
            }
            return null;
        }

        public void Delete(Guid entityID)
        {
            Person person = this.GetById(entityID);
            if (person != null)
            {
                person.DeletedAt = DateTime.Now;
                this.db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public void Edit(Person entity)
        {
            Person person = this.GetById(entity.Id);
            if (person != null)
            {
                person.FirstName = entity.FirstName.Trim().ToUpper();
                person.LastName = entity.LastName.Trim().ToUpper();
                person.Genre = entity.Genre.Trim().ToUpper();
                person.UpdatedAt = DateTime.Now;
                this.db.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public Person Get(Guid entityID)
        {
            return this.db.People.Where(p => p.Id == entityID && p.CreatedAt == p.DeletedAt).FirstOrDefault();
        }

        public async Task<object> Get(int page, int rows)
        {
            int totalRows = await this.db.People.Where(p => p.CreatedAt == p.DeletedAt).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var people = await this.db.People.Where(p => p.CreatedAt == p.DeletedAt).OrderBy(p => p.LastName).ThenBy(p => p.LastName).Skip((page -1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, people = people };
            return result;
        }

        public async Task<object> Get(int page, int rows, string search)
        {
            int totalRows = await this.db.People.Where(p => p.CreatedAt == p.DeletedAt && (p.FirstName.Contains(search) || p.LastName.Contains(search) || p.Email.Contains(search) || p.PhoneNumber.Contains(search) || p.Address.Contains(search) || p.Genre.Contains(search))).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var people = await this.db.People.Where(p => p.CreatedAt == p.DeletedAt && (p.FirstName.Contains(search) || p.LastName.Contains(search) || p.Email.Contains(search) || p.PhoneNumber.Contains(search) || p.Address.Contains(search) || p.Genre.Contains(search))).OrderBy(p => p.LastName).ThenBy(p => p.LastName).Skip((page -1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, people = people };
            return result;
        }

        public List<Person> GetAll()
        {
            return this.db.People.Where(p => p.CreatedAt == p.DeletedAt).ToList();
        }

        public void SaveAllChanges()
        {
            this.db.SaveChanges();
        }
    }
}
