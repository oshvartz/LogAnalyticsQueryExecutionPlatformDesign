using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Impl
{
    public class DefaultRetryPolicy : IRetryPolicy
    {
        public TimeSpan[] SleepDurations => new[] { TimeSpan.FromSeconds(10) };

        public bool IsTransient(Exception ex)
        {
            //if log analytics and transient - return true
            //otherwise return false
            return true;
        }
    }
}
