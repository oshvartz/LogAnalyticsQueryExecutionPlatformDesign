using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Examples
{
    public class AlertGenerationDataModel
    {
        public Identity Identity { get; set; }
        public string Query { get; set; }
        public DateTime QueryStartTimeUtc { get;  set; }
        public DateTime QueryEndTimeUtc { get;  set; }
    }
}
