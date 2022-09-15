using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public LanguageRepository(BookstoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<LanguageModel>> GetLanguagesAsync()
        {
            return await this._dbContext.Languages.Select(language => new LanguageModel
            {
                Id = language.Id,
                Name = language.Name,
                Description = language.Description
            }).ToListAsync();
        }
    }
}
