using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBooksAsync()
        {
            var book = await _context.GetBooksAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooksAsync(int id)
        {
            var book = await _context.GetBooksAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
}
