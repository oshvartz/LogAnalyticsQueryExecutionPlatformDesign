using System;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobScheduling
    {
        public string CronExpression { get; set; }

        public TimeSpan? Interval { get; set; }
    }
}