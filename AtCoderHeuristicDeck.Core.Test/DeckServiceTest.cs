using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtCoderHeuristicDeck.Core.Test;

public class DeckServiceTest
{
    [Fact]
    public async void RatingCalculationTest()
    {
        var performances = new[]
        {
            2410,
            1603,
            2521,
            2633,
            1669,
            2126,
            2402,
            2488,
            2484,
            3068,
            3482,
            1763,
            2655,
            2708,
            2475,
            3015,
            1821,
            2898
        }
        .Select(p => new Performance(p, ""));
        const string userName = "terry_u16";
        var history = new ContestHistory(userName, new Rating(2918), performances.ToImmutableArray());
        var moq = new Mock<IContestHistoryClient>();
        moq.Setup(x => x.GetContestHistoryAsync(userName)).Returns(Task.Run(() => history));
        var deck = new DeckService(moq.Object);

        var statistics = await deck.GetExtendedPerformancesAsync(userName);

        Assert.Equal(2918, statistics.Rating.Value);
        Assert.Equal(userName, statistics.UserName);
    }
}
