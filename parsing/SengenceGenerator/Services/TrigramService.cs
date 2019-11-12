using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SentenceGenerator.Services
{
    public class TrigramService
    {
        public Dictionary<string, List<string>> getTrigramsDictionary(string sentence)
        {
            sentence = removeExtraWhiteSpace(sentence);
            string[] words = splitSentenceByWords(sentence);
            var TrigramsDictionary = getTrigrams(words);
            return TrigramsDictionary;
        }

        private string removePunctuation(string sentence)
        {
            return Regex.Replace(sentence, @"[-:();_]", " ");
        }

        private string removeExtraWhiteSpace(string sentence)
        {
            return Regex.Replace(sentence, @"\s+", " ");
        }

        private string[] splitSentenceByWords(string sentence)
        {
            string[] words = Regex.Split(sentence, @"[ ]+");
            return words;
        }

        private Dictionary<string, List<string>> getTrigrams(string[] words)
        {
            var TrigramsDictionary = addWordsToDictionary(words);
            return TrigramsDictionary;
        }

        private Dictionary<string, List<string>> addWordsToDictionary(string[] words)
        {
            var TrigramsDictionary = new Dictionary<string, List<string>>();
            for (int i = 0; i < words.Length - 2; i++)
            {
                string firstTwoWords = words[i] + " " + words[i + 1];
                string thirdWord = words[i + 2];

                if (TrigramsDictionary.ContainsKey(firstTwoWords))
                {
                    TrigramsDictionary[firstTwoWords].Add(thirdWord);
                }
                else
                {
                    TrigramsDictionary.Add(firstTwoWords, new List<string> { thirdWord });
                }
            }

            return TrigramsDictionary;
        }

        public string CreateSentenceFromTrigram(Dictionary<string, List<string>> dictionary, string startingWord)
        {
            var key = getKeyFromDictionaryThatContainsWord(dictionary, startingWord);
            var story = new List<string>();
            //Add Key (split by spaces)
            var twoWords = splitSentenceByWords(key);
            story.Add(twoWords[0]);
            story.Add(twoWords[1]);

            //Get Rand Index
            string thirdWord = getRandomWord(dictionary[key]);

            //Add Third Word
            story.Add(thirdWord);

            story = addWordsToStory(dictionary, story, 10000);

            return String.Join(" ", story);

        }

        private List<string> addWordsToStory(Dictionary<string, List<string>> dictionary, List<string> story, int maxLength)
        {
            bool done = false;
            while (!done)
            {
                var twoLastWords = story[story.Count - 2] + " " + story[story.Count - 1];
                var key = twoLastWords;

                if (dictionary.ContainsKey(key) == false || story.Count > maxLength)
                {
                    done = true;
                }

                if (dictionary.ContainsKey(key))
                {
                    string thirdWord = getRandomWord(dictionary[key]);
                    story.Add(thirdWord);
                }
            }

            return story;
        }

        private string getRandomWord(List<string> list)
        {
            Random rand = new Random();
            string randWord = list[rand.Next(0, list.Count)];
            return randWord;
        }

        private string getKeyFromDictionaryThatContainsWord(Dictionary<string, List<string>> dictionary, string word)
        {
            List<string> keyList = new List<string>(dictionary.Keys);

            string key = keyList.Where(k => k.Contains(word)).FirstOrDefault();
            return key;
        }
    }
}
