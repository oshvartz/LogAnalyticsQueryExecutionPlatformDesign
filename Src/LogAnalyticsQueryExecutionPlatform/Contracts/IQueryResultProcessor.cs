using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Contracts
{
    public interface IQueryResultProcessor<T>
    {
        Task ProcessQueryResultsAsync(QueryResults queryResults, JobExecutionContext<T> jobInvocation,CancellationToken cancellationToken);
    }
}
