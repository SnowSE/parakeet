using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SampleProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if(args.Length < 3)
            {
                Console.WriteLine("I need 3 arguments: The url of the trygram parser, the starting # and the batch size");
                return;
            }
            var parserApiUrl = args[0];
            var startingNum = int.Parse(args[1]);
            var batchSize = int.Parse(args[2]);

            Console.WriteLine($"Starting with {startingNum}, doing {batchSize} @ {parserApiUrl}");
            Console.WriteLine("Press [Enter] when you're ready to begin...");
            Console.ReadLine();

            var trygramService = RestService.For<ITrygramParserApi>(parserApiUrl);

            for(var num = startingNum; num < startingNum+batchSize; num++)
            {
                var url = $"http://www.gutenberg.org/cache/epub/{num}/pg{num}.txt";
                using (var client = new WebClient())
                {
                    try
                    {
                        var text = client.DownloadString(url).Substring(0, 10_000);
                        var title = text.Substring(0, text.IndexOf('\n'));

                        Console.WriteLine($"Submitting {title} (#{num} of {startingNum + batchSize})");
                        await trygramService.CreateTrygramsAsync(new CreateTrygramRequest
                        {
                            Title = title,
                            Text = text
                        });
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"!! Unfortunately, #{num} died: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("All done!");
        }
    }

    public interface ITrygramParserApi
    {
        [Post("/api/trygram/createtrygrams")]
        Task CreateTrygramsAsync(CreateTrygramRequest request);
    }

    public class CreateTrygramRequest
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
