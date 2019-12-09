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
            //Console.WriteLine("Press [Enter] when you're ready to begin...");
            //Console.ReadLine();

            var trygramService = RestService.For<ITrygramParserApi>(parserApiUrl);

            for(var num = startingNum; num < startingNum+batchSize; num++)
            {
                (var title, var text) = await downloadText(num);
                if(title == null || text == null)
                {
                    Console.WriteLine($"#{num} didn't return any title/text.  Skipping...");
                    continue;
                }

                Console.WriteLine($"Submitting {title} (#{num} of {startingNum + batchSize})");

                await addTrygrams(title, text, trygramService);
            }

            Console.WriteLine("All done!");
        }

        private static async Task<(string title, string text)> downloadText(int num)
        {
            var url = $"http://www.gutenberg.org/cache/epub/{num}/pg{num}.txt";
            using (var client = new AutoDeflateWebClient())
            {
                try
                {
                    var text = await client.DownloadStringTaskAsync(url);                    
                    text = text.Substring(0, 10_000);
                    var title = text.Substring(0, text.IndexOf('\n'));
                    return (title, text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!! Unable to download #{num}: {ex.Message}");
                    return (null, null);
                }
            }
        }

        private static async Task addTrygrams(string title, string text, ITrygramParserApi trygramService)
        {
            try
            {
                await trygramService.CreateTrygramsAsync(new CreateTrygramRequest
                {
                    Title = title,
                    Text = text
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"!! Unable to add trygrams for {title}: {ex.Message}");
            }
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

    public class AutoDeflateWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }
}
