using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public interface ILogAnalyticsQueryExecutionPlatform
    {
        Task UpsertLogAnalyticsQueryExecutionJobScheduling(JobDefinition jobDefinition, JobScheduling jobScheduling);
    }
}
