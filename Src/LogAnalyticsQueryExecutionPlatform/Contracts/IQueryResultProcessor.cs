using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Contracts
{
    public interface IQueryResultProcessor
    {
        Task<ProcessingResult> ProcessQueryResultsAsync(QueryResults queryResults);
    }
}
