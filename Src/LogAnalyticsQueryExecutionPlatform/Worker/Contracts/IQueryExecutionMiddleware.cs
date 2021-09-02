using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Contracts
{
    public interface IQueryExecutionMiddleware<T>
    {
        Task ExecuteQueryAsync(QueryResults queryResults, QueryDefinition queryDefinition, JobExecutionContext<T> jobExecutionContext, CancellationToken cancellationToken);

        IQueryExecutionMiddleware<T> SetNext(IQueryExecutionMiddleware<T> next);
    }
}
