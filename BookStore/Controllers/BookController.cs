using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> GetAllBooks()
        {
            List<BookModel> bookList = await _bookRepository.GetAllBooksAsync();
            return View(bookList);
        }

        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            BookModel book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(book);
            }
            return View(book);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddNewBook(int id = 0, bool isSuccess = false)
        {
            //var model = new BookModel
            //{
            //    LanguageId = 2 // binding the value attribute of option tag in select tag.
            //};
            ViewBag.BookId = id;
            ViewBag.IsSuccess = isSuccess;

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CoverPhoto != null)
                {
                    //Save the cover book in the server:
                    string folderPath = Path.Combine("images", "book");
                    model.CoverBookUrl = await UploadFile(folderPath, model.CoverPhoto);
                }
                if (model.GalleryFiles != null)
                {
                    //Save the gallery images of the book in the server:
                    string folderPath = Path.Combine("images", "gallery");
                    model.Gallery = new List<GalleryModel>();

                    foreach (IFormFile file in model.GalleryFiles)
                    {
                        var galleryModel = new GalleryModel
                        {
                            Name = file.FileName,
                            URL = await UploadFile(folderPath, file)
                        };
                        model.Gallery.Add(galleryModel);
                    }
                }
                if (model.BookPdf != null)
                {
                    //Save the  book in pdf format in the server:
                    string folderPath = Path.Combine("books", "pdf");
                    model.BookPdfUrl = await UploadFile(folderPath, model.BookPdf);
                }
                int bookId = await _bookRepository.AddAsync(model);
                if (bookId > 0)
                {
                    return RedirectToAction(nameof(AddNewBook),
                        new { IsSuccess = true, Id = bookId });
                }
            }
            return View(model);
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            string fileUrl = Path.Combine(folderPath, uniqueFileName);
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, fileUrl);
            await file.CopyToAsync(new FileStream(fullPath, FileMode.Create));

            return fileUrl;
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
