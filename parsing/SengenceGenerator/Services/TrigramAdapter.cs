using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentenceGenerator.Services
{
    public class TrigramAdapter : ITrigramAdapter
    {
        private readonly TrygramContext _context;

        public TrigramAdapter(TrygramContext context)
        {
            _context = context;
        }

        public Dictionary<string, List<string>> ConvertTrigramObjectToString(string title)
        {
            var unparsedDictionary = new Dictionary<string, List<TrygramValues>>();
            foreach (var keyValuePair in _context.Trygrams.Include(t=>t.Values).Where(t=>t.Title == title).ToList())
            {
                unparsedDictionary[keyValuePair.Key] = keyValuePair.Values;
            }

            var dictionary = new Dictionary<string, List<string>>();
            foreach (var keyPair in unparsedDictionary)
            {
                foreach (var trigramValue in keyPair.Value)
                {
                    if (dictionary.ContainsKey(keyPair.Key) == false)
                    {
                        dictionary[keyPair.Key] = new List<string> { trigramValue.Value };
                    }
                    else
                    {
                        dictionary[keyPair.Key].Add(trigramValue.Value);
                    }
                }
            }

            return dictionary;
        }

        public async Task<IEnumerable<string>> GetTitlesAsync()
        {
            var trygrams = await _context.Trygrams.ToListAsync();
            return trygrams.Select(t => t.Title).Distinct();            
        }
    }
}
