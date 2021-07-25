using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.DataModel
{
    public class JobInvocation<T>
    {
        public DateTime TriggerLogicTime { get; set; }
        public DateTime TriggerActualTime { get; set; }

        public T JobDefinition { get; set; }
    }
}
