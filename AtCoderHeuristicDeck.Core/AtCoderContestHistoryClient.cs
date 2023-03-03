using System.Collections.Immutable;
using System.Text.Json;

namespace AtCoderHeuristicDeck.Core;

public class AtCoderContestHistoryClient : IContestHistoryClient
{
    private readonly HttpClient _httpClient;

    public AtCoderContestHistoryClient(string userName)
    {
        _httpClient = new HttpClient();
    }

    public async Task<ContestHistory> GetContestHistoryAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException($"{userName}がnullです。", nameof(userName));
        }

        try
        {
            var url = $"https://atcoder.jp/users/{userName}/history/json?contestType=heuristic";
            var json = await _httpClient.GetStringAsync(url);
            var results = JsonSerializer.Deserialize<ContestResult[]>(json) ?? Array.Empty<ContestResult>();

            var ratedResults = results.Where(result => result.IsRated).ToArray();

            if (ratedResults.Length == 0)
            {
                throw new LoadingStatisticsFailureException($"ユーザー {userName} のRatedコンテスト参加履歴が見つかりませんでした。");
            }

            var rating = new Rating(ratedResults.Last().NewRating);
            var performances = ratedResults.Select(result => new Performance(result.Performance, result.ContestName)).ToImmutableArray();

            return new ContestHistory(userName, rating, performances);
        }
        catch (HttpRequestException ex)
        {
            throw new LoadingStatisticsFailureException($"ユーザー {userName} が見つかりませんでした。", ex);
        }
    }
}