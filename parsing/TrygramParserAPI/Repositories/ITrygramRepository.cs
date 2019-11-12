using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace TrygramParserAPI.Repositories
{
    public interface ITrygramRepository
    {
        Task AddTrygramAsync(Trygram trygram);
        bool TrygramExists(Trygram trygram);
        Task UpdateTrygramAsync(Trygram trygram);
        Trygram GetTrygramByKey(string key);
        Task<List<Trygram>> GetAllTrygramsAsync();
    }
}