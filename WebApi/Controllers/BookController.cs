using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;

         public BookController(BookStoreDbContext context)
         {
            _context = context;
         }


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x=>x.Title==newBook.Title);

            if(book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();

            if(book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            
            if(book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();           
        }

    }
}