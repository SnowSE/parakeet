using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrygramParserAPI.Data;
using TrygramParserAPI.Models;

namespace TrygramParserAPI.Repositories
{
    public class TrygramRepository : ITrygramRepository
    {
        private readonly TrygramContext _context;

        public TrygramRepository(TrygramContext context)
        {
            _context = context;
        }
        public bool TrygramExists(Trygram trygram)
        {
            return _context.Trygrams.Any(t => t.Key == trygram.Key);
        }

        public async Task AddTrygramAsync(Trygram trygram)
        {
            await _context.Trygrams.AddAsync(trygram);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrygramAsync(Trygram trygram)
        {
            _context.Trygrams.Update(trygram);
            await _context.SaveChangesAsync();
        }

        public Trygram GetTrygramByKey(string key)
        {
            return _context.Trygrams.Where(t => t.Key == key).FirstOrDefault();
        }

        public async Task<List<Trygram>> GetAllTrygramsAsync()
        {
            return await _context.Trygrams.ToListAsync();
        }
    }
}
