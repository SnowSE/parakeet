using System.Collections.Generic;
using System.Threading.Tasks;
using TrygramParserAPI.Models;

namespace TrygramParserAPI.Services
{
    public interface ITrygramService
    {
        Task<bool> ParseString(string input);

        Task<List<Trygram>> GetTrygrams();
    }
}