using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxRunner
{
    public class ScheduledAlertRuleConditionCheckWorker : LogAnalyticsQueryExecutionWorker<ScheduledAlertRuleConditionCheckActorModel>
    {
        public override string JobType => "ScheduledAlertRuleConditionCheck";

        protected override IQueryResultProcessor<ScheduledAlertRuleConditionCheckActorModel> QueryResultProcessor => throw new NotImplementedException();

        protected override IQueryDefinitionBuilder<ScheduledAlertRuleConditionCheckActorModel> QueryDefinitionBuilder => throw new NotImplementedException();
    }
}
