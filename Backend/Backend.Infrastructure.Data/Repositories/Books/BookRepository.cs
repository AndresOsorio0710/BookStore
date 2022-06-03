using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Contexts;
using Backend.Domain.Interfaces.Repository;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories.Books
{
    public class BookRepository : IRepositoryBase<Book, Guid>
    {
        private BookAppContext db;

        public BookRepository(BookAppContext db)
        {
            this.db = db;
        }

        private Book Get(Book book)
        {
            Book result = this.db.Books.Where(b => b.CreatedAt == b.DeletedAt && (b.Title.Equals(book.Title) && b.Author.Equals(book.Author))).FirstOrDefault();
            return result;
        }

        private Book GetById(Guid id)
        {
            return this.db.Books.Where(b => b.CreatedAt == b.DeletedAt && b.Id == id).FirstOrDefault();
        }

        public Book Add(Book entity)
        {
            if (this.Get(entity) == null)
            {
                Book book = new Book();
                book.Title = entity.Title.Trim().ToUpper();
                book.Description = entity.Description.Trim();
                book.Author = entity.Author.Trim().ToUpper();
                book.Publisher = entity.Publisher.Trim().ToUpper();
                book.Genre = entity.Genre.Trim().ToUpper();
                book.Price = entity.Price;
                this.db.Add(book);
                return book;
            }
            return null;
        }

        public void Delete(Guid entityID)
        {
            Book book = this.GetById(entityID);
            if (book != null)
            {
                book.DeletedAt = DateTime.Now;
                this.db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public void Edit(Book entity)
        {
            Book book = this.GetById(entity.Id);
            if (book != null)
            {
                book.Title = entity.Title.Trim().ToUpper();
                book.Description = entity.Description.Trim();
                book.Author = entity.Author.Trim().ToUpper();
                book.Publisher = entity.Publisher.Trim().ToUpper();
                book.Genre = entity.Genre.Trim().ToUpper();
                book.Price = entity.Price;
                book.UpdatedAt = DateTime.Now;
                this.db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public Book Get(Guid entityID)
        {
            Book response = this.db.Books.Where(b => b.CreatedAt == b.DeletedAt && b.Id == entityID).FirstOrDefault();
            return response;
        }

        public async Task<object> Get(int page, int rows)
        {
            int totalRows = await this.db.Books.Where(b => b.CreatedAt == b.DeletedAt).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var books = await this.db.Books.Where(b => b.CreatedAt == b.DeletedAt).OrderBy(b => b.Genre).ThenBy(b => b.Title).Skip((page -1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, books = books };
            return result;
        }

        public async Task<object> Get(int page, int rows, string search)
        {
            int totalRows = await this.db.Books.Where(b => b.CreatedAt == b.DeletedAt && (b.Title.Contains(search) || b.Author.Contains(search) || b.Publisher.Contains(search) || b.Genre.Contains(search) || b.Description.Contains(search))).CountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows)/rows));
            var books = await this.db.Books.Where(b => b.CreatedAt == b.DeletedAt && (b.Title.Contains(search) || b.Author.Contains(search) || b.Publisher.Contains(search) || b.Genre.Contains(search) || b.Description.Contains(search))).OrderBy(b => b.Genre).ThenBy(b => b.Title).Skip((page -1) * rows).Take(rows).ToListAsync();
            var result = new { pages = totalPages, currentPage = page, books = books };
            return result;
        }

        public List<Book> GetAll()
        {
            return this.db.Books.Where(b => b.CreatedAt == b.DeletedAt).OrderBy(b => b.Genre).ThenBy(b => b.Title).ToList();
        }

        public void SaveAllChanges()
        {
            this.db.SaveChanges();
        }
    }
}
