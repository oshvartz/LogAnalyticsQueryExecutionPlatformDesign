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
    public abstract class QueryExecutionMiddleware<T> : IQueryExecutionMiddleware<T>
    {
        private IQueryExecutionMiddleware<T> _next;

        public async Task ExecuteQueryAsync(QueryResults queryResults, QueryDefinition queryDefinition, JobExecutionContext<T> jobExecutionContext, CancellationToken cancellationToken)
        {
            var shouldInvokeNext = await ExecuteQueryInternalAsync(queryResults, queryDefinition, jobExecutionContext, cancellationToken);
            if (_next != null && shouldInvokeNext)
            {
                await _next.ExecuteQueryAsync(queryResults, queryDefinition, jobExecutionContext, cancellationToken);
            }
        }

        public IQueryExecutionMiddleware<T> SetNext(IQueryExecutionMiddleware<T> next)
        {
            _next = next;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryResults"></param>
        /// <param name="queryDefinition"></param>
        /// <param name="jobExecutionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task<bool> ExecuteQueryInternalAsync(QueryResults queryResults, QueryDefinition queryDefinition, JobExecutionContext<T> jobExecutionContext, CancellationToken cancellationToken);

    }
}
