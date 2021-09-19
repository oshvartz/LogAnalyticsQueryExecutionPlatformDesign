using Azure.Messaging.ServiceBus;
using LogAnalyticsQueryExecutionPlatform.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.API.Imp
{
    public class LogAnalyticsQueryExecutionPlatformImpl : ILogAnalyticsQueryExecutionPlatform
    {
        private readonly IJobQueueSender _jobQueueSender;

        public LogAnalyticsQueryExecutionPlatformImpl(IJobQueueSender jobQueueSender)
        {
            
            _jobQueueSender = jobQueueSender;
        }

        public Task<bool> DeleteJob(JobDefinitionIdentity jobDefinitionIdentity)
        {
            throw new NotImplementedException();
        }

        public Task<JobDescription> GetJob(JobDefinitionIdentity jobDefinitionIdentity)
        {
            throw new NotImplementedException();
        }

        public async Task UpsertJob(JobDescription jobDescription, CancellationToken cancellationToken)
        {
            await _jobQueueSender.EnqueueJob(jobDescription, cancellationToken);
        }

    }
}
