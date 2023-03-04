using System.Text.Json.Serialization;

namespace AtCoderHeuristicDeck.Core;

public readonly struct Rating
{
    public int Value { get; }

    [JsonConstructor]
    public Rating(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = value;
    }

    internal static Rating FromPerformanceList(ExtendedPerformanceList performances)
    {
        const double ratingThreshold = 400;
        var numerator = 0.0;
        var denomnator = 0.0;

        foreach (var (perf, weight) in performances.EnumeratePerformanceAndWeights())
        {
            numerator += perf.Value * weight;
            denomnator += weight;
        }

        var innerRating = numerator / denomnator;

        int rating;

        if (innerRating >= 400)
        {
            rating = (int)Math.Round(innerRating);
        }
        else
        {
            rating = (int)Math.Round(ratingThreshold / Math.Exp((ratingThreshold - innerRating) / ratingThreshold));
        }

        return new Rating(rating);
    }

    public override string ToString() => Value.ToString();
}