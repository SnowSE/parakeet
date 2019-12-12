using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Data;
using Shared.Models;

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
            var b = _context.Trygrams.Any(t => t.Key == trygram.Key);
            _context.Database.CloseConnection();
            return b;
        }

        public async Task AddTrygramAsync(Trygram trygram)
        {
            await _context.Trygrams.AddAsync(trygram);
            await _context.SaveChangesAsync();
            await _context.Database.CloseConnectionAsync();
        }

        public async Task UpdateTrygramAsync(Trygram trygram)
        {
            _context.Trygrams.Update(trygram);
            await _context.SaveChangesAsync();
            await _context.Database.CloseConnectionAsync();
        }

        public Trygram GetTrygramByKey(string key)
        {
            var t = _context.Trygrams.Where(t => t.Key == key).FirstOrDefault();
            _context.Database.CloseConnection();
            return t;
        }

        public async Task<List<Trygram>> GetAllTrygramsAsync()
        {
            var items = await _context.Trygrams.ToListAsync();
            await _context.Database.CloseConnectionAsync();
            return items;
        }
    }
}
