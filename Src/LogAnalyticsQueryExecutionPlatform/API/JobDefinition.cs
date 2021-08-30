using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobDefinition
    {
        public string JobId { get; set; } //rule id
        public string JobType { get; set; } // ConditionChecker - Scheduled/Nrt/AlertGenertor
        public JsonDocument JobData { get; set; } // the data model
    }
}
