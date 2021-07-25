using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Contracts
{
    public interface IQueryResultProcessor<T>
    {
        Task<ProcessingResult> ProcessQueryResultsAsync(QueryResults queryResults, JobInvocation<T> jobInvocation);
    }
}
