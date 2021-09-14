using LogAnalyticsQueryExecutionPlatform.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobDefinition<T>
    {
        public string JobId { get; init; }
        public string JobType { get; init; }
        public T JobData { get; init; }
        public JobScheduling JobScheduling { get; init; } //could be null for ad-hoc
    }
}
