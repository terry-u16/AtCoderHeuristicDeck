using System.Text.Json.Serialization;

namespace AtCoderHeuristicDeck.Core;

public readonly struct Performance
{
    public int Value { get; }

    public string ContestName { get; }

    [JsonConstructor]
    public Performance(int value, string contestName)
    {
        Value = value;
        ContestName = contestName;
    }

    public override string ToString() => Value.ToString();
}