using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            List<BookModel> books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
