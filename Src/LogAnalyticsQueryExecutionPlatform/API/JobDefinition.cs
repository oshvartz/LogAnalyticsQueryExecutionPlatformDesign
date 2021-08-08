using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobDefinition
    {
        public string JobId { get; set; }
        public string JobType { get; set; }
        public JObject JobData { get; set; }
    }
}
