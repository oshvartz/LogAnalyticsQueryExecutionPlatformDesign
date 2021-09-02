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
    public class AutoDisableQueryExecutionMiddleware<T> : IQueryExecutionMiddleware<T>
    {
        private IQueryExecutionMiddleware<T> _next;

        public async Task ExecuteQueryAsync(QueryResults queryResults, QueryDefinition queryDefinition, JobExecutionContext<T> jobExecutionContext, CancellationToken cancellationToken)
        {
            try
            {
                //check if need to disable if yes - finish return;
                //else
                await _next.ExecuteQueryAsync(queryResults, queryDefinition, jobExecutionContext, cancellationToken);
                //store success

            }
            catch (Exception ex)
            {
                //store error
                var error = ex;
                throw;
            }
        }

        public IQueryExecutionMiddleware<T> SetNext(IQueryExecutionMiddleware<T> next)
        {
            _next = next;
            return this;
        }
    }
}
