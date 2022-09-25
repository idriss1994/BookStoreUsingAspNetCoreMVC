using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookstoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) :
            base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
