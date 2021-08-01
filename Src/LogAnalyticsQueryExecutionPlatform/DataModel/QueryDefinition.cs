using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class QueryDefinition
    {
        public string Query { get; set; }
        public TimeSpan? QueryTimeout { get; set; }
        public Identity Identity { get; set; }
    }
}
