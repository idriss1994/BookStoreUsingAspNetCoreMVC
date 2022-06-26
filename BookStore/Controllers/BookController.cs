using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var model = new BookModel
            {
                Language = "2" // binding the value attribute of option tag in select tag.
            };
            ViewBag.BookId = id;
            ViewBag.IsSuccess = isSuccess;
            ViewBag.Languages = GetLanguagesUsingSelectListItem();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                int bookId = await _bookRepository.Add(model);
                if (bookId > 0)
                {
                    return RedirectToAction(nameof(AddNewBook),
                        new { IsSuccess = true, Id = bookId });
                }
            }
            ViewBag.ModelNotValid = true;
            ModelState.AddModelError("", "This my custome error msg1");
            ModelState.AddModelError("", "This my custome error msg2");
            ViewBag.Languages = GetLanguagesUsingSelectListItem();
            return View();
        }
        private IEnumerable<SelectListItem> GetLanguages()
        {
            var list = new List<LanguageModel>
            {
                new LanguageModel { Id = 1, Text = "Arabic" },
                new LanguageModel { Id = 2, Text = "English" },
                new LanguageModel { Id = 3, Text = "French" }
            };

            return list.Select(l => new SelectListItem
            {
                Text = l.Text,
                Value = l.Id.ToString()
            });
         }
        private List<SelectListItem> GetLanguagesUsingSelectListItem()
        {
            //Create groups for SelectListItem instances:
            var group1 = new SelectListGroup { Name = "Group1" };
            var group2 = new SelectListGroup { Name = "Group2" };
            var group3 = new SelectListGroup { Name = "Group3", Disabled = true };
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Arabic", Value = "1", Group = group1 },
                new SelectListItem { Text = "English", Value = "2", Group = group2 },
                new SelectListItem { Text = "Amazigh", Value = "3", Group = group1 },
                new SelectListItem { Text = "French", Value = "4" , Group = group2},
                new SelectListItem { Text = "Hindi", Value = "5", Group = group3 },
                new SelectListItem { Text = "Dutch", Value = "6", Group = group3 },
            };
        }
    }
}
