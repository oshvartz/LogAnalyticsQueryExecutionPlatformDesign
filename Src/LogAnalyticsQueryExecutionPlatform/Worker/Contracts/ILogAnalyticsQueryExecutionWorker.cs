using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Contracts
{
    public interface ILogAnalyticsQueryExecutionWorker
    {
        Task StartAsync();
        Task StopAsync();
        public string JobType { get; }
    }
}
