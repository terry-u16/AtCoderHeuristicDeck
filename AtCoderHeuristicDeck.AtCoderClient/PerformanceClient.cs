using AtCoderHeuristicDeck.Core;
using System.Collections.Immutable;
using System.Text.Json;

namespace AtCoderHeuristicDeck.AtCoderClient;


public class PerformanceClient : IStatisticsClient
{
    private readonly string _userName;
    private readonly HttpClient _httpClient;

    public PerformanceClient(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException($"{userName}がnullです。", nameof(userName));
        }

        _userName = userName;
        _httpClient = new HttpClient();
    }

    public async Task<Statistics> GetStatisticsAsync()
    {
        var url = $"https://atcoder.jp/users/{_userName}/history/json?contestType=heuristic";
        var json = await _httpClient.GetStringAsync(url);
        var results = JsonSerializer.Deserialize<ContestResult[]>(json) ?? Array.Empty<ContestResult>();

        var ratedResults = results.Where(result => result.IsRated).ToArray();

        if (ratedResults.Length == 0)
        {
            return new Statistics(_userName, 0, ImmutableArray<Performance>.Empty);
        }

        var rating = ratedResults.Last().NewRating;
        var performances = ratedResults.Select(result => new Performance(result.Performance)).ToImmutableArray();

        return new Statistics(_userName, rating, performances);
    }
}