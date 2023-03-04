using System.Net;
using AtCoderHeuristicDeck.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AtCoderHeuristicDeck.Api
{
    public class User
    {
        private readonly ILogger _logger;
        private readonly DeckService _deckService;

        public User(ILoggerFactory loggerFactory, DeckService deckService)
        {
            _logger = loggerFactory.CreateLogger<User>();
            _deckService = deckService;
        }

        [Function("User")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{userId}")] HttpRequestData req, string userId)
        {
            try
            {
                await Console.Out.WriteLineAsync("waiting...");
                _logger.LogInformation("C# HTTP trigger function processed a request.");

                var deck = await _deckService.GetExtendedPerformancesAsync(userId);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(deck);
                return response;
            }
            catch (LoadingStatisticsFailureException)
            {
                var response = req.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
