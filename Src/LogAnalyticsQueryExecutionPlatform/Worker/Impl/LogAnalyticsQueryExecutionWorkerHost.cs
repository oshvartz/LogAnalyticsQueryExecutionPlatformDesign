using LogAnalyticsQueryExecutionPlatform.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    public class LogAnalyticsQueryExecutionWorkerHost
    {
        private IEnumerable<ILogAnalyticsQueryExecutionWorker> _logAnalyticsQueryExecutionWorkers;

        public LogAnalyticsQueryExecutionWorkerHost(IEnumerable<ILogAnalyticsQueryExecutionWorker> logAnalyticsQueryExecutionWorkers)
        {
            _logAnalyticsQueryExecutionWorkers = logAnalyticsQueryExecutionWorkers;
        }

        public async Task StartAsync()
        {
          _logAnalyticsQueryExecutionWorkers.ToList().ForEach(async worker => await worker.StartAsync());
        }
    }
}
