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
    public class SuggestionRepository:GenericRepository<Suggestion>,ISuggestionRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Suggestion> _dbSet;
        public SuggestionRepository(AppDbContext context):base(context)
        {
            _context = context;
            _dbSet = _context.Set<Suggestion>();
        }
        
        public async Task<List<Suggestion>> GetAllSuggestions(){
            return _dbSet.Include(x=> x.SuggestedLook).ThenInclude(x=> x.Photos).ToList();
        }
        public async Task<Suggestion> GetSuggestionById(Guid id){
            return _dbSet.Include(x => x.SuggestedLook).ThenInclude(x=> x.Photos).FirstOrDefault(x=> x.Id ==id);
        }
        public async Task<Suggestion> CreateSuggestion(User user)
        {
            var suggestion = new Suggestion{
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
