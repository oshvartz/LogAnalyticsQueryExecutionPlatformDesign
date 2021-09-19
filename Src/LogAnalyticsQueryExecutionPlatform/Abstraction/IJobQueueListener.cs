using LogAnalyticsQueryExecutionPlatform.DataModel;
using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction
{
    public interface IJobQueueListener<T>
    {
        Task StartProcessingAsync(string queueName, IRetryPolicy retryPolicy,  Func<JobExecutionContext<T>, CancellationToken, Task> processMessageHandler);

        Task StopAsync();

    }
}
