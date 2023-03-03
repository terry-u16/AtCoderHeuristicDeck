﻿namespace AtCoderHeuristicDeck.Core;

internal record ContestResult(bool IsRated, int Place, int OldRating, int NewRating, int Performance, 
    int InnerPerformance, string ContestScreenName, string ContestName, string ContestNameEn, DateTime EndTime);