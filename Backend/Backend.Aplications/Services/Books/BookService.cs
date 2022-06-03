using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Aplications.Interfaces;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;

namespace Backend.Aplications.Services.Books
{
    public class BookService : IServiceBase<Book, Guid>
    {
        private readonly IRepositoryBase<Book, Guid> repositoryBase;

        public BookService(IRepositoryBase<Book, Guid> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public Book Add(Book entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("The Book is required.");
            }
            var book = this.repositoryBase.Add(entity);
            if (book != null)
            {
                this.repositoryBase.SaveAllChanges();
            }
            return book;
        }

        public void Delete(Guid entityID)
        {
            this.repositoryBase.Delete(entityID);
            this.repositoryBase.SaveAllChanges();
        }

        public void Edit(Book entity)
        {
            if (entity == null)
            {
                throw new AccessViolationException("The Book is required.");
            }
            this.repositoryBase.Edit(entity);
            this.repositoryBase.SaveAllChanges();
        }

        public Book Get(Guid entityID)
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

        public List<Book> GetAll()
        {
            return this.repositoryBase.GetAll();
        }
    }
}
