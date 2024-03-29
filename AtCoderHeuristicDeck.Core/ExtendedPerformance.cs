﻿namespace AtCoderHeuristicDeck.Core;

internal readonly struct ExtendedPerformance : IComparable<ExtendedPerformance>
{
    public double Value { get; }

    internal ExtendedPerformance(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new ArgumentException($"{nameof(value)}の値が不正です。");
        }

        Value = value;
    }

    public static IEnumerable<ExtendedPerformance> FromPerformance(InnerPerformance performance)
    {
        const double s = 724.4744301;
        const int extensionLength = 100;

        for (int i = 1; i <= extensionLength; i++)
        {
            yield return new ExtendedPerformance(performance.Value - s * Math.Log(i));
        }
    }

    public int CompareTo(ExtendedPerformance other) => Value.CompareTo(other.Value);

    public override string ToString() => Value.ToString();
}