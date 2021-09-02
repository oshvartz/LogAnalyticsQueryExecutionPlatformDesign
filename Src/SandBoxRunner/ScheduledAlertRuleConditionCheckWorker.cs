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
        private readonly IQueryResultProcessor<ScheduledAlertRuleConditionCheckActorModel> _queryResultProcessor;
        private readonly IQueryDefinitionBuilder<ScheduledAlertRuleConditionCheckActorModel> _queryDefinitionBuilder;

        public ScheduledAlertRuleConditionCheckWorker(IQueryResultProcessor<ScheduledAlertRuleConditionCheckActorModel> queryResultProcessor,
            IQueryDefinitionBuilder<ScheduledAlertRuleConditionCheckActorModel> queryDefinitionBuilder)
        {
            _queryResultProcessor = queryResultProcessor;
            _queryDefinitionBuilder = queryDefinitionBuilder;
        }

        public override string JobType => "ScheduledAlertRuleConditionCheck";

        protected override IQueryResultProcessor<ScheduledAlertRuleConditionCheckActorModel> QueryResultProcessor => _queryResultProcessor;

        protected override IQueryDefinitionBuilder<ScheduledAlertRuleConditionCheckActorModel> QueryDefinitionBuilder => _queryDefinitionBuilder;
    }
}
