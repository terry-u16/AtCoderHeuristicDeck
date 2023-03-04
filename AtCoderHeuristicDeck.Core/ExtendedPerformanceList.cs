using System.Collections;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace AtCoderHeuristicDeck.Core;

public class ExtendedPerformanceList
{
    private const int MaxCount = 100;

    public ImmutableArray<ExtendedPerformance> Performances { get; }

    public ExtendedPerformanceList()
    {
        Performances = ImmutableArray<ExtendedPerformance>.Empty;
    }

    [JsonConstructor]
    public ExtendedPerformanceList(ImmutableArray<ExtendedPerformance> performances)
    {
        Performances = performances;
    }

    private ExtendedPerformanceList(ImmutableArray<ExtendedPerformance>.Builder builder)
    {
        builder.Sort((a, b) => -a.CompareTo(b));

        while (builder.Count > MaxCount)
        {
            builder.RemoveAt(builder.Count - 1);
        }

        Performances = builder.ToImmutable();
    }

    public ExtendedPerformanceList Add(ExtendedPerformance performance)
    {
        var builder = Performances.ToBuilder();
        builder.Add(performance);
        return new ExtendedPerformanceList(builder);
    }


    public ExtendedPerformanceList Add(IEnumerable<ExtendedPerformance> performances)
    {
        var builder = Performances.ToBuilder();
        builder.AddRange(performances);
        return new ExtendedPerformanceList(builder);
    }
}