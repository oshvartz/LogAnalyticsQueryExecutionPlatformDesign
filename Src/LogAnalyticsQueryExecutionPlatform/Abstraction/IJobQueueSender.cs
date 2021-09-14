using LogAnalyticsQueryExecutionPlatform.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction
{
    public interface IJobQueueSender
    {
        Task EnqueueJob(JobDescription jobDescription, CancellationToken cancellationToken);
    }
}
