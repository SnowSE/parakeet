using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrygramParserAPI.Models;
using TrygramParserAPI.Repositories;

namespace TrygramParserAPI.Services
{
    public class TrygramService : ITrygramService
    {
        private ITrygramRepository _trygramRepository;

        public TrygramService(ITrygramRepository trygramRepository)
        {
            _trygramRepository = trygramRepository;
        }

        public async Task<List<Trygram>> GetTrygrams()
        {
            return await _trygramRepository.GetAllTrygramsAsync();
        }

        public async Task<bool> ParseString(string input)
        {
            var stringList = splitString(input);
            for (int i = 0; i < stringList.Count - 2; i++)
            {
                var trygram = new Trygram { Key = (stringList[i] + " " + stringList[i + 1]), Values = new List<TrygramValues>() };
                var value = new TrygramValues { Value = stringList[i + 2]};
                trygram.Values.Add(value);
                if (_trygramRepository.TrygramExists(trygram))
                {
                    var trygramToUpdate = _trygramRepository.GetTrygramByKey(trygram.Key);
                    if(trygramToUpdate.Values is null)
                    {
                        trygramToUpdate.Values = new List<TrygramValues>();
                    }
                    trygramToUpdate.Values.Add(value);

                    await _trygramRepository.UpdateTrygramAsync(trygramToUpdate);
                }
                else
                {
                    await _trygramRepository.AddTrygramAsync(trygram);
                }
            }


            return true;
        }

        private List<string> splitString(string baseString)
        {
            return baseString.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
