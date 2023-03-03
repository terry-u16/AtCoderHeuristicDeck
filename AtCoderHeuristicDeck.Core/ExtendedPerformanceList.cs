using System.Collections.Immutable;

namespace AtCoderHeuristicDeck.Core;

public class ExtendedPerformanceList
{
    private const int MaxCount = 100;
    private readonly ImmutableArray<ExtendedPerformance> _performances;
    internal IEnumerable<ExtendedPerformance> Performances => _performances;

    public ExtendedPerformanceList()
    {
        _performances = ImmutableArray<ExtendedPerformance>.Empty;
    }

    private ExtendedPerformanceList(ImmutableArray<ExtendedPerformance>.Builder builder)
    {
        builder.Sort((a, b) => -a.CompareTo(b));

        while (builder.Count > MaxCount)
        {
            builder.RemoveAt(builder.Count - 1);
        }

        _performances = builder.ToImmutable();
    }

    public ExtendedPerformanceList Add(ExtendedPerformance performance)
    {
        var builder = _performances.ToBuilder();
        builder.Add(performance);
        return new ExtendedPerformanceList(builder);
    }


    public ExtendedPerformanceList Add(IEnumerable<ExtendedPerformance> performances)
    {
        var builder = _performances.ToBuilder();
        builder.AddRange(performances);
        return new ExtendedPerformanceList(builder);
    }
}