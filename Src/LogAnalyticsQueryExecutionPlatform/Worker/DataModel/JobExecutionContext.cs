using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobExecutionContext<T>
    {   
        public int RetryCount { get; private set; }
        public string FireInstanceId { get; private set; } //unique identifier of the run
        public DateTime FireLogicTimeUtc { get; private set; }  //will be datetime.UtcNow for ad-hoc trigger
        public DateTime FireActualTimeUtc { get; private set; }
        public JobDefinition<T> JobDefinition { get; private set; }
    }
}
