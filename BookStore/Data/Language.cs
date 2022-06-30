using System.Collections.Generic;

namespace BookStore.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //create relation between Language and Books (One to Many)
        public ICollection<Book> Books { get; set; }
    }
}
