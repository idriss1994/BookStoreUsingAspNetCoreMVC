using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookstoreDbContext _context;
        public BookRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<int> Add(BookModel bookModel)
        {
            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
                Author = bookModel.Author,
                CreateOn = DateTime.UtcNow,
                TotalPages = bookModel.TotalPages,
                UpdateOn = DateTime.UtcNow
            };
            await _context.Books.AddAsync(book);
            await Save();

            return book.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            List<Book> books = await _context.Books.ToListAsync();
            var allBooksModel = new List<BookModel>();
            //var value = books?.Any(); // can be true, false or null

            if (books?.Any() == true)
            {
                foreach (var book in books)
                {
                    allBooksModel.Add(new BookModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Description = book.Description,
                        TotalPages = book.TotalPages,
                        Language = book.Language,
                        Category = book.Category
                    });
                }
            }
            return allBooksModel;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                return new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    TotalPages = book.TotalPages,
                    Author = book.Author,
                    Category = book.Category,
                    Language = book.Language
                };
            }
            return null;
        }

        public IList<BookModel> DataSource()
        {
            var _bookList = new List<BookModel>()
            {
                new BookModel
                {
                    Id = 1,
                    Title = "C#",
                    Description = "This description for c# book",
                    Author = "Idriss",
                    Category = "Programing",
                    Language = "English",
                    TotalPages = 345
                },
                new BookModel
                {
                    Id = 2,
                    Title = "Java",
                    Description = "This description for java book",
                    Author = "Khalid",
                    Category = "Programing",
                    Language = "English",
                    TotalPages = 345
                },
                new BookModel
                {
                    Id = 3,
                    Title = "Asp.net core",
                    Description = "This description for Asp.net core book",
                    Author = "Yassine",
                    Category = "Framework",
                    Language = "English",
                    TotalPages = 34
                },
                new BookModel
                {
                    Id = 4,
                    Title = "PHP",
                    Description = "This description for PHP book",
                    Author = "Abdelkarim",
                    Category = "Programing",
                    Language = "English",
                    TotalPages = 145
                },
                new BookModel
                {
                    Id = 5,
                    Title = "JavaScript",
                    Description = "This description for JavaScript book",
                    Author = "Mourad",
                    Category = "Programing",
                    Language = "French",
                    TotalPages = 600
                }
            };

            return _bookList;
        }
    }
}
