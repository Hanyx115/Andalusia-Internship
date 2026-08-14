using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Service.Interface;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookservice _bookService;
        public BookController(IBookservice bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public IEnumerable<Books> Get()
        {
            return _bookService.ListAllBooks();
        }
        [HttpGet("{id}")]
        public ActionResult<Books> Get(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Books book)
        {
            _bookService.AddBook(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }








}
