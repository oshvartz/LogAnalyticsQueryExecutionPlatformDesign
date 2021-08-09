using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Examples
{
    public class AlertGenerationQueryResultProcessor : IQueryResultProcessor<AlertGenerationDataModel>
    {
        public Task ProcessQueryResultsAsync(QueryResults queryResults, JobExecutionContext<AlertGenerationDataModel> jobExecutionContext, CancellationToken cancellationToken)
        {
            var alerts = GenerateAlerts(queryResults, jobExecutionContext);
        }

        private object GenerateAlerts(QueryResults queryResults, JobExecutionContext<AlertGenerationDataModel> jobExecutionContext)
        {
            throw new NotImplementedException();
        }
    }
}
