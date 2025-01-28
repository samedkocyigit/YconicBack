using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SuggestionRepositories
{
    public class SuggestionRepository:GenericRepository<Suggestions>,ISuggestionRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Suggestions> _dbSet;
        public SuggestionRepository(AppDbContext context):base(context)
        {
            _context = context;
            _dbSet = _context.Set<Suggestions>();
        }
        public async Task<Suggestions> GetSuggestionById(Guid id){
            return _dbSet.Include(x => x.SuggestedLook).FirstOrDefault(x=> x.Id ==id);
        }
        public async Task<Suggestions> CreateSuggestion(User user)
        {
            var suggestion = new Suggestions{
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Description = "Suggested Look",
            };
            await _context.SaveChangesAsync();


            var clotheCategories = user.UserGarderobe.ClothesCategory;
            foreach (var item in clotheCategories)
            {
                suggestion.SuggestedLook.Add(item.Clothes.FirstOrDefault());
            }
            await _dbSet.AddAsync(suggestion);
            await _context.SaveChangesAsync();
            
            return suggestion;
        }

    }
}
