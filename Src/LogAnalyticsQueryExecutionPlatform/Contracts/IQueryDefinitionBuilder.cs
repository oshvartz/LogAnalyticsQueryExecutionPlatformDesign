using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Contracts
{
    public interface IQueryDefinitionBuilder<T>
    {
        Task<QueryDefinition> BuildAsync(IJobExecutionContext<T> jobInvocation,CancellationToken cancellationToken);
    }
}
