using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobExecutionContext<T>
    {
        public JobExecutionContext()
        {

        }
        public int RetryCount { get; init; }
        public string FireInstanceId { get; init; } //unique identifier of the run
        public DateTime FireLogicTimeUtc { get; init; }  //will be datetime.UtcNow for ad-hoc trigger
        public DateTime FireActualTimeUtc { get; init; }
        public JobDefinition<T> JobDefinition { get; init; }
    }
}
