using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace TrygramParserAPI.Services
{
    public interface ITrygramService
    {
        Task ParseAndPersistAsync(string title, string input);
    }
}