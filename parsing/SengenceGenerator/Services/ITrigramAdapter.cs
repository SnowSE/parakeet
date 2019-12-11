using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace SentenceGenerator.Services
{
    public interface ITrigramAdapter
    {
        Dictionary<string, List<string>> ConvertTrigramObjectToString(string title);
        Task<IEnumerable<string>> GetTitlesAsync();
    }
}