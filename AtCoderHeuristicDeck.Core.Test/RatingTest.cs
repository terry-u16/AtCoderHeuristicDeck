namespace AtCoderHeuristicDeck.Core.Test;

public class RatingTest
{
    [Fact]
    public void RatingCalculationTest()
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
        .Select(p => new Performance(p))
        .Select(p => new InnerPerformance(p));

        var performanceList = new ExtendedPerformanceList();

        foreach (var perf in performances)
        {
            performanceList = performanceList.Add(ExtendedPerformance.FromPerformance(perf));
        }

        var rating = Rating.FromPerformanceList(performanceList);

        Assert.Equal(2918.0, rating.Value, 1);
    }

    [Fact]
    public void RatingCalculationUnderThresholdTest()
    {
        var performance = new Performance(120);
        var innerPerformance = new InnerPerformance(performance);
        var performanceList = new ExtendedPerformanceList();

        performanceList = performanceList.Add(ExtendedPerformance.FromPerformance(innerPerformance));

        var rating = Rating.FromPerformanceList(performanceList);

        Assert.Equal(10.0, rating.Value, 1);
    }
}