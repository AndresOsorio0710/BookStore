using Backend.Domain.Models;
using Backend.Infrastructure.API.Logic;
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
        private static BookLogic bookLogic = new BookLogic();

        [HttpGet]
        public ActionResult<List<Book>> Get([FromHeader] Guid sesionId)
        {
            var books = bookLogic.Get(sesionId);
            if (books == null)
                return Ok("Access error.");
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id, [FromHeader] Guid sesionId)
        {
            var book = bookLogic.Get(sesionId, id);
            if (book == null)
                return Ok("Access error.");
            return Ok(book);
        }

        [HttpGet("paginator/{page}/{rows}")]
        public async Task<IActionResult> Get(int page, int rows, [FromHeader] Guid sesionId)
        {
            var book = await bookLogic.Get(sesionId, page, rows);
            if (book == null)
                return Ok("Access error.");
            return Ok(book);
        }

        [HttpGet("paginator/{search}")]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int rows, string search, [FromHeader] Guid sesionId)
        {
            var book = await bookLogic.Get(sesionId, page, rows, search);
            if (book == null)
                return Ok("Access error.");
            return Ok(book);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book, [FromHeader] Guid sesionId)
        {
            return Ok(bookLogic.Add(sesionId, book));
        }

        [HttpPatch("{id?}")]
        public ActionResult Patch([FromBody] Book book, Guid? id, [FromHeader] Guid sesionId)
        {
            return Ok(bookLogic.Edit(sesionId, book, id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id, [FromHeader] Guid sesionId)
        {
            return Ok(bookLogic.Delete(sesionId, id));
        }
    }
}
