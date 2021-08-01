using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    public abstract class LogAnalyticsQueryExecutionWorker<T>
    {
        protected abstract IQueryResultProcessor<T> QueryResultProcessor { get; }
        protected abstract IQueryDefinitionBuilder<T> QueryDefinitionBuilder { get; }

        private async Task ProcessMessage(object message, CancellationToken cancellationToken)
        {
            IJobExecutionContext<T> jobExecutionContext = ReadContext(message);
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

        private IJobExecutionContext<T> ReadContext(object message)
        {
            throw new NotImplementedException();
        }
    }
}
