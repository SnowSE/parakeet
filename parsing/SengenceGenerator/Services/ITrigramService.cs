using System.Collections.Generic;

namespace SentenceGenerator.Services
{
    public interface ITrigramService
    {
        string CreateSentenceFromTrigram(Dictionary<string, List<string>> dictionary, string startingWord);
        Dictionary<string, List<string>> getTrigramsDictionary(string sentence);
    }
}