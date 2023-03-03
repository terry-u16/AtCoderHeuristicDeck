using System.Collections.Immutable;

namespace AtCoderHeuristicDeck.Core;

public record Statistics(string UserName, int Rating, ImmutableArray<Performance> Performances);