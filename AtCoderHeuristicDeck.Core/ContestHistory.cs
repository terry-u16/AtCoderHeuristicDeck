using System.Collections.Immutable;

namespace AtCoderHeuristicDeck.Core;


public record ContestHistory(string UserName, Rating Rating, ImmutableArray<Performance> Performances);