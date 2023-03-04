using System.Collections.Immutable;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace AtCoderHeuristicDeck.Core;

public class AtCoderContestHistoryClient : IContestHistoryClient
{
    private readonly HttpClient _httpClient;

    public AtCoderContestHistoryClient()
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

            var results = await _httpClient.GetFromJsonAsync<ContestResult[]>(url) ?? Array.Empty<ContestResult>();

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
            Debug.WriteLine(ex.Message);
            throw new LoadingStatisticsFailureException($"ユーザー {userName} が見つかりませんでした。", ex);
        }
    }
}