using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Worker.Examples
{
    class AlertGenerationQueryDefinitionBuilder : IQueryDefinitionBuilder<AlertGenerationDataModel>
    {
        private static readonly TimeSpan LogAnalyticsLatency = TimeSpan.FromMinutes(5);

        public Task<QueryDefinition> BuildAsync(JobExecutionContext<AlertGenerationDataModel> jobExecutionContext, CancellationToken cancellationToken)
        {
            var queryDefinition = new QueryDefinition
            {
                Identity = jobExecutionContext.JobDefinition.JobData.Identity,
                Query = BuildEnrichmentQuery(jobExecutionContext.JobDefinition.JobData.Query),
                QueryStartTimeUtc = jobExecutionContext.JobDefinition.JobData.QueryStartTimeUtc,
                QueryEndTimeUtc = jobExecutionContext.JobDefinition.JobData.QueryEndTimeUtc,
            };

            return Task.FromResult<QueryDefinition>(queryDefinition);
        }

        private string BuildEnrichmentQuery(string query)
        {
            throw new NotImplementedException();
        }
    }
}
