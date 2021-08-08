using System;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobScheduling
    {
        public string CronExpression { get; set; }

        public TimeSpan? Interval { get; set; }
    }
}