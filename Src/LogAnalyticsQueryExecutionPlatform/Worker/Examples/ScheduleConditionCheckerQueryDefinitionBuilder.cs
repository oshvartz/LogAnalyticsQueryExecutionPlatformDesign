using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Examples
{
    class ScheduleConditionCheckerQueryDefinitionBuilder : IQueryDefinitionBuilder<AlertGenerationDataModel>
    {
        private static readonly TimeSpan LogAnalyticsLatency = TimeSpan.FromMinutes(5);

        public Task<QueryDefinition> BuildAsync(JobExecutionContext<AlertGenerationDataModel> jobExecutionContext, CancellationToken cancellationToken)
        {
            var queryDefinition = new QueryDefinition
            {
                Identity = jobExecutionContext.JobDefinition.JobData.Identity,
                Query = BuildCount(jobExecutionContext.JobDefinition.JobData.Query),
                QueryStartTimeUtc = jobExecutionContext.FireLogicTimeUtc - LogAnalyticsLatency - jobExecutionContext.JobDefinition.JobData.Windows
                QueryEndTimeUtc = jobExecutionContext.FireLogicTimeUtc - LogAnalyticsLatency
            };

            return private string BuildCount(string query)
        {
            throw new NotImplementedException();
        }

        Task.FromResult<QueryDefinition>(queryDefinition);
        }

        private string BuildCount(string query)
        {
            throw new NotImplementedException();
        }
    }
}
