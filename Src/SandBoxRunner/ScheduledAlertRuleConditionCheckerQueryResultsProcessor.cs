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
    public class ScheduledAlertRuleConditionCheckerQueryResultsProcessor : IQueryResultProcessor<ScheduledAlertRuleConditionCheckActorModel>
    {
        public Task ProcessQueryResultsAsync(QueryResults queryResults,JobExecutionContext<ScheduledAlertRuleConditionCheckActorModel> jobInvocation, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{DateTime.UtcNow} in ScheduledAlertRuleConditionCheckerQueryResultsProcessor.ProcessQueryResultsAsync");
            //check if condition met
            throw new ArgumentException("arg");
            //if yes create alert generation job
            return Task.CompletedTask;
        }
    }
}
