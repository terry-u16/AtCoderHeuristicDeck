namespace AtCoderHeuristicDeck.Core;

public readonly struct Performance
{
    public int Value { get; }

    public string ContestName { get; }

    public Performance(int value, string contestName)
    {
        Value = value;
        ContestName = contestName;
    }

    public override string ToString() => Value.ToString();
}