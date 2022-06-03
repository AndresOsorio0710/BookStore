using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Aplications.Interfaces;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;

namespace Backend.Aplications.Services.Users
{
    public class UserService : IServiceBase<User, Guid>
    {
        private readonly IRepositoryUser<User, Guid> repositoryUser;

        public UserService(IRepositoryUser<User, Guid> repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }

        public User Add(User entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException("The User is required.");
            }
            var user = this.repositoryUser.Add(entity);
            if (user!=null)
            {
                this.repositoryUser.SaveAllChanges();
            }
            return user;
        }

        public void Delete(Guid entityID)
        {
            this.repositoryUser.Delete(entityID);
            this.repositoryUser.SaveAllChanges();
        }

        public void Edit(User entity)
        {
            if (entity == null)
            {
                throw new AccessViolationException("The User is required.");
            }
            this.repositoryUser.Edit(entity);
            this.repositoryUser.SaveAllChanges();
        }

        public User Get(Guid entityID)
        {
            return this.repositoryUser.Get(entityID);
        }

        public Task<object> Get(int page, int rows)
        {
            return this.repositoryUser.Get(page, rows);
        }

        public Task<object> Get(int page, int rows, string search)
        {
            return this.repositoryUser.Get(page, rows, search);
        }

        public User Get(string user, string password)
        {
            return this.repositoryUser.Get(user, password);
        }

        public List<User> GetAll()
        {
            return this.repositoryUser.GetAll();
        }
    }
}
