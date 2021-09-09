using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SandBoxRunner
{
    public class ScheduledAlertRuleConditionCheckerQueryDefinitonBuilder : IQueryDefinitionBuilder<ScheduledAlertRuleConditionCheckActorModel>
    {
        private static readonly TimeSpan LogAnalyticsLatency = TimeSpan.FromMinutes(5);

        public Task<QueryDefinition> BuildAsync(JobExecutionContext<ScheduledAlertRuleConditionCheckActorModel> jobExecutionContext, CancellationToken cancellationToken)
        {
            var queryEndTimeUtc = jobExecutionContext.FireLogicTimeUtc - LogAnalyticsLatency;

            var queryDefinition = new QueryDefinition
            {
                Identity = new Identity(),// jobExecutionContext.JobDefinition.JobData.Permissions,
                Query = BuildCount(jobExecutionContext.JobDefinition.JobData.Query),
                QueryStartTimeUtc = jobExecutionContext.JobDefinition.JobData.QueryStartTimeUtc - LogAnalyticsLatency,
                QueryEndTimeUtc = jobExecutionContext.JobDefinition.JobData.QueryEndTimeUtc - LogAnalyticsLatency,
            };

            return Task.FromResult<QueryDefinition>(queryDefinition);
        }

        private string BuildCount(string query)
        {
            return query + "|count";
        }
    }
}
