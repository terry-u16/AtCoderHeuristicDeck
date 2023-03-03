namespace AtCoderHeuristicDeck.Core;

public class DeckService
{
    private readonly IContestHistoryClient _client;

    public DeckService(IContestHistoryClient client)
    {
        _client = client;
    }

    public async Task<Statistics> GetExtendedPerformancesAsync(string userName)
    {
        var history = await _client.GetContestHistoryAsync(userName);

        var extendedPerformanceList = new ExtendedPerformanceList();

        foreach (var performance in history.Performances)
        {
            var innerPerformance = new InnerPerformance(performance);
            var extendedPerformance = ExtendedPerformance.FromPerformance(innerPerformance);
            extendedPerformanceList.Add(extendedPerformance);
        }

        return new Statistics(history.UserName, history.Rating, extendedPerformanceList);
    }
}
