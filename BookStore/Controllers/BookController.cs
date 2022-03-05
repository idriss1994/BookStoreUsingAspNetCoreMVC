using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController(BookRepository bookRepository)
        {
            this._bookRepository = new BookRepository();
        }
        public IActionResult GetAllBooks()
        {
            var bookList = _bookRepository.GetAllBooks();
            return View(bookList);
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);

            return View(book);
        }

        public IActionResult AddNewBook()
        {
            //Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper
            return View();
        }

        [HttpPost]
        public IActionResult AddNewBook(BookModel model)
        {
            
            return View(model);
        }
    }
}
