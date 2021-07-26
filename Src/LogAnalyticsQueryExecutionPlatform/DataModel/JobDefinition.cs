using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobDefinition<T>
    {
        public string JobId { get; set; }
        public string JobType { get; set; }
        public T JobData { get; set; }
    }
}
