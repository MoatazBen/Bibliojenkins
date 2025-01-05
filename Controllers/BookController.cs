using biblio.Models;
using biblio.Services;
using biblio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace biblio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllBooks() => Ok(_service.GetAllBooks());

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _service.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpGet("title")]
        public IActionResult GetAllByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest(new { Message = "Title query parameter is required." });
            }

            var books = _service.GetAllByTitle(title);
            if (!books.Any())
            {
                return NotFound(new { Message = "No books found with the specified title." });
            }

            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddBook(bookVM);
            int addedBookID = _service.GetAllBooks().Last().Id;
            return CreatedAtAction(nameof(GetBookById), new { id = addedBookID }, bookVM);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookVM bookVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.UpdateBook(id, bookVM);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _service.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found." });
            }

            _service.DeleteBook(id);
            return NoContent();
        }
    }
}
