using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    public abstract class LogAnalyticsQueryExecutionWorker<T> : ILogAnalyticsQueryExecutionWorker
    {
        protected abstract IQueryResultProcessor<T> QueryResultProcessor { get; }
        protected abstract IQueryDefinitionBuilder<T> QueryDefinitionBuilder { get; }
        public string JobType => typeof(T).FullName;

        private async Task ProcessMessage(object message, CancellationToken cancellationToken)
        {
            JobExecutionContext<T> jobExecutionContext = ReadContext(message);
            var queryDefinition =  await QueryDefinitionBuilder.BuildAsync(jobExecutionContext, cancellationToken);
            try
            {
                var queryResults = await ExecuteQueryAsync(queryDefinition);
                await QueryResultProcessor.ProcessQueryResultsAsync(queryResults, jobExecutionContext, cancellationToken);

            }
            catch(Exception ex)
            {
                await HandleRetryAsync(ex);
            }

        }

        private Task HandleRetryAsync(Exception ex)
        {
            throw new NotImplementedException();
        }

        private Task<QueryResults> ExecuteQueryAsync(QueryDefinition queryDefinition)
        {
            throw new NotImplementedException();
        }

        private JobExecutionContext<T> ReadContext(object message)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync()
        {
            //start listening to queue by job type
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            //stop listening to queue by job type
            return Task.CompletedTask;
        }
    }
}
