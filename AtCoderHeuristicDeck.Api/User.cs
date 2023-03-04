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
        private readonly IContestHistoryClient _contestHistoryClient;

        public User(ILoggerFactory loggerFactory, IContestHistoryClient contestHistoryClient)
        {
            _logger = loggerFactory.CreateLogger<User>();
            _contestHistoryClient = contestHistoryClient;
        }

        [Function("User")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{userId}")] HttpRequestData req, string userId)
        {
            try
            {
                await Console.Out.WriteLineAsync("waiting...");
                _logger.LogInformation("C# HTTP trigger function processed a request.");

                var history = await _contestHistoryClient.GetContestHistoryAsync(userId);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(history);
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
