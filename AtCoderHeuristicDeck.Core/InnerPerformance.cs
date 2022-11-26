namespace AtCoderHeuristicDeck.Core;

internal readonly struct InnerPerformance
{
    internal double Value { get; }

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
    }

    public override string ToString() => Value.ToString();
}