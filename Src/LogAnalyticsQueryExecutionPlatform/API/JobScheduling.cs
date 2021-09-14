using System;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobScheduling
    {
        public string CronExpression { get; set; }
        public SimpeleJobScheduling SimpeleJobScheduling { get; set; }
    }

    public class SimpeleJobScheduling
    {
        public TimeSpan? Interval { get; set; }

        public DateTime? StartTimeUtc { get; set; }
    }
}