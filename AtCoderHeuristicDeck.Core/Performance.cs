﻿namespace AtCoderHeuristicDeck.Core;

internal readonly struct Performance
{
    public int Value { get; }

    public Performance(int value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();
}