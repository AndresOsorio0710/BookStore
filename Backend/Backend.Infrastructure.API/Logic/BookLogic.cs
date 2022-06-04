using Backend.Aplications.Services.Books;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Logic;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.Books;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.API.Logic
{
    public class BookLogic
    {
        private static SesionLogic sesionLogic = new SesionLogic();

        BookService CreateBookService()
        {
            BookAppContext db = new BookAppContext();
            BookRepository bookRepository = new BookRepository(db);
            BookService bookService = new BookService(bookRepository);
            return bookService;
        }

        public List<Book> Get(Guid sesionId)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN") || sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                {
                    var service = this.CreateBookService();
                    return service.GetAll();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Book Get(Guid sesionId, Guid bookId)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN") || sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                {
                    var service = this.CreateBookService();
                    return service.Get(bookId);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Object> Get(Guid sesionId, int page, int rows)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN") || sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                {
                    var service = this.CreateBookService();
                    return await service.Get(page, rows);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Object> Get(Guid sesionId, int page, int rows, string search)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN") || sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                {
                    var service = this.CreateBookService();
                    return await service.Get(page, rows, search);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String Add(Guid sesionId, Book book)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                {
                    var service = this.CreateBookService();
                    if (service.Add(book)==null)
                    {
                        return "The record already exists.";
                    }
                    return "Successful registrations.";
                }
                return "Adceso denied.";
            }
            catch (Exception ex)
            {
                return "Access error.";
            }
        }

        public String Edit(Guid sesionId, Book book, Guid? id)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                {
                    if (id!=null)
                    {
                        if (book.Id == id)
                        {
                            var service = this.CreateBookService();
                            service.Edit(book);
                            return "Successful registrations.";
                        }
                        return "Registry error, key does not match.";
                    }
                    return "Book Id is reuired.";
                }
                return "Adceso denied.";
            }
            catch (Exception ex)
            {
                return "Access error.";
            }
        }

        public String Delete(Guid sesionId, Guid id)
        {
            try
            {
                if (sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                {
                    var service = this.CreateBookService();
                    service.Delete(id);
                    return "Successful registrations.";
                }
                return "Adceso denied.";
            }
            catch (Exception ex)
            {
                return "Access error.";
            }
        }
    }
}
