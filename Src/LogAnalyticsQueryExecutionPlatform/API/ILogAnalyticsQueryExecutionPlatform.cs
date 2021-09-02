using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.API
{
    public interface ILogAnalyticsQueryExecutionPlatform
    {
        /// <summary>
        /// Create or update QueryExecution Job
        /// </summary>
        /// <param name="jobDefinition">the job definition</param>
        /// <param name="jobScheduling">scheduling or null if should run immediately</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task UpsertJob(JobDescription jobDescription, CancellationToken cancellationToken);

        
        Task<JobDescription> GetJob(JobDefinitionIdentity jobDefinitionIdentity);


        Task<bool> DeleteJob(JobDefinitionIdentity jobDefinitionIdentity);
    }
}
