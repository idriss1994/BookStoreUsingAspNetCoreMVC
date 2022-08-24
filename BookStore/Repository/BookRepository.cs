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
                LanguageId = bookModel.LanguageId,
                CoverBookUrl = bookModel.CoverBookUrl,
                BookPdfUrl = bookModel.BookPdfUrl
            };

            book.BookGallery = new List<BookGallery>();
            foreach (var file in bookModel.Gallery)
            {
                book.BookGallery.Add(new BookGallery
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }

            await _context.Books.AddAsync(book);
            await SaveAsync();

            return book.Id;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            DbSet<Book> books =  _context.Books;
            var allBooksModel = new List<BookModel>();
            //var value = books?.Any(); // can be true, false or null

            if (books?.Any() == true)
            {

                allBooksModel = await books.Select(book => new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    TotalPages = book.TotalPages,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Category = book.Category,
                    CoverBookUrl = book.CoverBookUrl
                }).ToListAsync();
            }
            return allBooksModel;
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            DbSet<Book> books = _context.Books;
            var topBooksModel = new List<BookModel>();
            //var value = books?.Any(); // can be true, false or null

            if (books?.Any() == true)
            {

                topBooksModel = await books.Select(book => new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    TotalPages = book.TotalPages,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Category = book.Category,
                    CoverBookUrl = book.CoverBookUrl
                }).Take(count).ToListAsync();
            }
            return topBooksModel;
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => b.Id == id)
                .Select(book => new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Description = book.Description,
                    Category = book.Category,
                    TotalPages = book.TotalPages,
                    CoverBookUrl = book.CoverBookUrl,
                    Gallery = book.BookGallery.Select(gallery => new GalleryModel
                    {
                        Id = gallery.Id,
                        Name = gallery.Name,
                        URL = gallery.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
        }
    }
}
