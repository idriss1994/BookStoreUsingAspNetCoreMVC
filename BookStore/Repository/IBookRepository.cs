using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddAsync(BookModel bookModel);
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        Task SaveAsync();
    }
}