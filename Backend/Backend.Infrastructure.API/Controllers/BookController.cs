using Backend.Aplications.Services.Books;
using Backend.Domain.Models;
using Backend.Infrastructure.API.Logic;
using Backend.Infrastructure.Data.Contexts;
using Backend.Infrastructure.Data.Repositories.Books;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private SesionLogic sesionLogic = new SesionLogic();

        BookService CreateBookService()
        {
            BookAppContext db = new BookAppContext();
            BookRepository bookRepository = new BookRepository(db);
            BookService bookService = new BookService(bookRepository);
            return bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get([FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN") || this.sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                    {
                        var service = this.CreateBookService();
                        return Ok(service.GetAll());
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN") || this.sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                    {
                        var service = this.CreateBookService();
                        return Ok(service.Get(id));
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpGet("paginator/{page}/{rows}")]
        public async Task<IActionResult> Get(int page, int rows, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN") || this.sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                    {
                        var service = this.CreateBookService();
                        var result = await service.Get(page, rows);
                        return Ok(result);
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpGet("paginator/{search}")]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int rows, [FromQuery] string search, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN") || this.sesionLogic.ValidatePermission(sesionId, "STANDARD"))
                    {
                        var service = this.CreateBookService();
                        var result = await service.Get(page, rows, search);
                        return Ok(result);
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                    {
                        var service = this.CreateBookService();
                        if (service.Add(book) == null)
                        {
                            return Ok("The record already exists.");
                        }
                        return Ok("Successful registrations.");
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpPatch("{id?}")]
        public ActionResult Patch([FromBody] Book book, Guid? id, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                    {
                        if (id!=null)
                        {
                            if (book.Id == id)
                            {
                                var service = this.CreateBookService();
                                service.Edit(book);
                                return Ok("Successful registrations.");
                            }
                            return BadRequest("Registry error, key does not match.");
                        }
                        return BadRequest("Book Id is reuired.");
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id, [FromHeader] Guid sesionId)
        {
            if (sesionId != null)
            {
                try
                {
                    if (this.sesionLogic.ValidatePermission(sesionId, "ADMIN"))
                    {
                        if (id!=null)
                        {
                            var service = this.CreateBookService();
                            service.Delete(id);
                            return Ok("Successful registrations.");
                        }
                        return BadRequest("Book Id is reuired.");
                    }
                    return BadRequest("Adceso denied.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Access error.");
                }
            }
            return BadRequest("Credentials required.");
        }
    }
}
