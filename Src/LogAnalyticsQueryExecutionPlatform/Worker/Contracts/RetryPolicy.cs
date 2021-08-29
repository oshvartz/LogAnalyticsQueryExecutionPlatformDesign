using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Contracts
{
    public interface IRetryPolicy
    {
        TimeSpan[] SleepDurations { get;}

        bool IsTransient(Exception ex);

    }
}
