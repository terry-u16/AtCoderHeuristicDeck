namespace AtCoderHeuristicDeck.Core;

public interface IContestHistoryClient
{
    Task<ContestHistory> GetContestHistoryAsync(string userName);
}
