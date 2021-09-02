namespace LogAnalyticsQueryExecutionPlatform.API
{
    public class JobDefinitionIdentity
    {
        public string JobId { get; set; } //rule id
        public string JobType { get; set; } // ConditionChecker - Scheduled/Nrt/AlertGenertor
    }
}