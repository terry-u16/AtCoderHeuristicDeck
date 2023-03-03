namespace AtCoderHeuristicDeck.Core;

internal readonly struct InnerPerformance
{
    public double Value { get; }
    public string ContestName { get; }

    internal InnerPerformance(Performance performance)
    {
        const double ratingThreshold = 400;

        if (performance.Value >= ratingThreshold)
        {
            Value = performance.Value;
        }
        else
        {
            Value = ratingThreshold * (1 - Math.Log(ratingThreshold / performance.Value));
        }

        ContestName = performance.ContestName;
    }

    public override string ToString() => Value.ToString();
}