using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private List<BookModel> _bookList = null;
        public BookRepository()
        {
            _bookList = new List<BookModel>()
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
        }
        public IList<BookModel> GetAllBooks()
        {
            return _bookList;
        }

        public BookModel GetBookById(int id)
        {
            return _bookList.FirstOrDefault(b => b.Id == id);
        }
    }
}
