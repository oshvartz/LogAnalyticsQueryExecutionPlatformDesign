using LogAnalyticsQueryExecutionPlatform.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction.Impl
{
    public class InMemJobQueueSender : IJobQueueSender
    {
        public Task EnqueueJob(JobDescription jobDescription, CancellationToken cancellationToken)
        {
            InMemQueueRepository.Instance.Enqueue(jobDescription.JobDefinition.JobType, JsonSerializer.Serialize<JobDescription>(jobDescription));
            return Task.CompletedTask;
        }
    }
}
