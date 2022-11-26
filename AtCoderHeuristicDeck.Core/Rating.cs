namespace AtCoderHeuristicDeck.Core;

internal readonly struct Rating
{
    public int Value { get; }

    private Rating(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = value;
    }

    internal static Rating FromPerformanceList(ExtendedPerformanceList performances)
    {
        const double r = 0.8271973364;
        const double ratingThreshold = 400;
        var numerator = 0.0;
        var denomnator = 0.0;
        var powR = 1.0;

        foreach (var perf in performances.Performances)
        {
            powR *= r;
            numerator += perf.Value * powR;
            denomnator += powR;
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