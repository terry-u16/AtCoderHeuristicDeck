namespace AtCoderHeuristicDeck.Core;

[Serializable]
public class LoadingStatisticsFailureException : Exception
{
    public LoadingStatisticsFailureException() { }
    public LoadingStatisticsFailureException(string message) : base(message) { }
    public LoadingStatisticsFailureException(string message, Exception inner) : base(message, inner) { }
    protected LoadingStatisticsFailureException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
