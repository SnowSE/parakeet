using System.Collections.Generic;
using Shared.Models;

namespace SentenceGenerator.Services
{
    public interface ITrigramAdapter
    {
        Dictionary<string, List<string>> ConvertTrigramObjectToString();
        IEnumerable<string> GetTitles();
    }
}