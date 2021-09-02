using LogAnalyticsQueryExecutionPlatform.API;
using LogAnalyticsQueryExecutionPlatform.API.Imp;
using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.Impl;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SandBoxRunner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ILogAnalyticsQueryExecutionPlatform logAnalyticsQueryExecutionPlatform = new LogAnalyticsQueryExecutionPlatformImpl();
            await logAnalyticsQueryExecutionPlatform.UpsertJob(new JobDescription
            {
                JobDefinition =
                new JobDefinition
                {
                    JobId = "15",
                    JobType = "ScheduledAlertRuleConditionCheck",
                    JobData = JsonDocument.Parse(JsonSerializer.Serialize<ScheduledAlertRuleConditionCheckActorModel>(new ScheduledAlertRuleConditionCheckActorModel
                    {
                        Permissions = "per",
                        Query = "query",
                        DisplayName = "DisplayName"
                    }))
                }
            }, CancellationToken.None);

            var scheduledAlertRuleConditionCheckWorker = new ScheduledAlertRuleConditionCheckWorker(new ScheduledAlertRuleConditionCheckerQueryResultsProcessor(),
                                                        new ScheduledAlertRuleConditionCheckerQueryDefinitonBuilder());

            var logAnalyticsQueryExecutionWorkerHost = new LogAnalyticsQueryExecutionWorkerHost(new ILogAnalyticsQueryExecutionWorker[]
            {
                scheduledAlertRuleConditionCheckWorker
            });

            await logAnalyticsQueryExecutionWorkerHost.StartAsync();

            await Task.Delay(-1);

        }
    }
}
