using Azure.Messaging.ServiceBus;
using LogAnalyticsQueryExecutionPlatform.Abstraction;
using LogAnalyticsQueryExecutionPlatform.API;
using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using LogAnalyticsQueryExecutionPlatform.Worker.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    public abstract class LogAnalyticsQueryExecutionWorker<T> : ILogAnalyticsQueryExecutionWorker
    {
        private readonly IJobQueueListener<T> _jobQueueListener;
        
        protected LogAnalyticsQueryExecutionWorker(IJobQueueListener<T> jobQueueListener)
        {
            _jobQueueListener = jobQueueListener;
        }

        protected virtual IRetryPolicy RetryPolicy { get; } = new DefaultRetryPolicy();
        protected abstract IQueryResultProcessor<T> QueryResultProcessor { get; }
        protected abstract IQueryDefinitionBuilder<T> QueryDefinitionBuilder { get; }
        public abstract string JobType { get; } //must match job type used in the API

        private async Task ProcessMessage(JobExecutionContext<T> jobExecutionContext,CancellationToken cancellationToken)
        {
            var queryDefinition = await QueryDefinitionBuilder.BuildAsync(jobExecutionContext, cancellationToken);

            //build pipeline Should Skip -> auto disable -> LA Execution
            var middleware = new ShouldSkipQueryExecutionMiddleware<T>()
                .SetNext(new AutoDisableQueryExecutionMiddleware<T>()
                .SetNext(new LogAnalyticsQueryExecutionMiddleware<T>()));

            var queryResults = new QueryResults();
            await middleware.ExecuteQueryAsync(queryResults, queryDefinition, jobExecutionContext, cancellationToken);
            await QueryResultProcessor.ProcessQueryResultsAsync(queryResults, jobExecutionContext, cancellationToken);
        }

        public async Task StartAsync()
        {
             await _jobQueueListener.StartProcessingAsync(JobType, ProcessMessage);
        }

        public async Task StopAsync()
        {
            await _jobQueueListener.StopAsync();
        }
    }
}
