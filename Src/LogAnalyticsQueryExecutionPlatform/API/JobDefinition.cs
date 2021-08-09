using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobDefinition
    {
        public string JobId { get; set; } //rule id
        public string JobType { get; set; } // Scheduled/Nrt/AlertGenertor/AlertPublish
        public JObject JobData { get; set; } // the data model
    }
}
