using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Aplications.Interfaces;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;

namespace Backend.Aplications.Services.People
{
    public class PersonService : IServiceBase<Person, Guid>
    {
        private readonly IRepositoryBase<Person, Guid> repositoryBase;

        public PersonService(IRepositoryBase<Person, Guid> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public Person Add(Person entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("The Person is required.");
            }
            Person person = this.repositoryBase.Add(entity);
            if (person != null)
            {
                this.repositoryBase.SaveAllChanges();
            }
            return person;
        }

        public void Delete(Guid entityID)
        {
            this.repositoryBase.Delete(entityID);
            this.repositoryBase.SaveAllChanges();
        }

        public void Edit(Person entity)
        {
            if (entity == null)
            {
                throw new AccessViolationException("The Person is required.");
            }
            this.repositoryBase.Edit(entity);
            this.repositoryBase.SaveAllChanges();
        }

        public Person Get(Guid entityID)
        {
            return this.repositoryBase.Get(entityID);
        }

        public Task<object> Get(int page, int rows)
        {
            return this.repositoryBase.Get(page, rows);
        }

        public Task<object> Get(int page, int rows, string search)
        {
            return this.repositoryBase.Get(page, rows, search);
        }

        public List<Person> GetAll()
        {
            return this.repositoryBase.GetAll();
        }
    }
}
