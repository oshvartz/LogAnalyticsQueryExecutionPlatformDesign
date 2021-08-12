using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Examples
{
    public class ScheduleConditionCheckerQueryResultProcessor : IQueryResultProcessor<AlertGenerationDataModel>
    {
        //private readonly
        public async Task ProcessQueryResultsAsync(QueryResults queryResults, JobExecutionContext<AlertGenerationDataModel> jobExecutionContext, CancellationToken cancellationToken)
        {
            //check condition
            //if condition met:ILogAnalyticsQueryExecutionPlatform.UpsertJob
        }

        
    }
}
