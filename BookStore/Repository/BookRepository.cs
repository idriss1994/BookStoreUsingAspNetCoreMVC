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
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<int> AddAsync(BookModel bookModel)
        {
            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
                Author = bookModel.Author,
                CreateOn = DateTime.UtcNow,
                TotalPages = bookModel.TotalPages,
                UpdateOn = DateTime.UtcNow,
                LanguageId = bookModel.LanguageId
            };
            await _context.Books.AddAsync(book);
            await SaveAsync();

            return book.Id;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
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
                        LanguageId = book.LanguageId,
                        Category = book.Category
                    });
                }
            }
            return allBooksModel;
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
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
                    LanguageId = book.LanguageId
                };
            }
            return null;
        }
    }
}
