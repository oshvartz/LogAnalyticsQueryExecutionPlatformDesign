using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public interface IJobExecutionContext<T>
    {
        int RetryCount { get; }
        string FireInstanceId { get; }
        DateTime FireLogicTimeUtc { get; }
        DateTime FireActualTimeUtc { get; }
        JobDefinition<T> JobDefinition { get; }
    }
}
