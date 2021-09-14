using LogAnalyticsQueryExecutionPlatform.DataModel;
using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Impl
{
    public class ShouldSkipQueryExecutionMiddleware<T> : QueryExecutionMiddleware<T>
    {
        protected override Task<bool> ExecuteQueryInternalAsync(QueryResults queryResults, QueryDefinition queryDefinition, JobExecutionContext<T> jobExecutionContext, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{DateTime.UtcNow} in ShouldSkipQueryExecutionMiddleware.ExecuteQueryInternalAsync");
            //if should skip return false
            //otherwise
            return Task.FromResult(true);
        }
    }
}
