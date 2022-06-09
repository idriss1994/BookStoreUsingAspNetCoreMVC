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
            _bookRepository = bookRepository;
        } 
        public async Task<IActionResult> GetAllBooks()
        {
            List<BookModel> bookList = await _bookRepository.GetAllBooks();
            return View(bookList);
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            BookModel book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound(book);
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult AddNewBook(int id = 0, bool isSuccess = false)
        {
            ViewBag.BookId = id;
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            int bookId = await _bookRepository.Add(model);
            if (bookId > 0)
            {
                return RedirectToAction(nameof(AddNewBook),
                    new { IsSuccess = true, Id = bookId});
            }

            return View();
        }
    }
}
