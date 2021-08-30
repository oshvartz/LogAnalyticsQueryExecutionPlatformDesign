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
            var tasks = _logAnalyticsQueryExecutionWorkers.ToList().Select(async worker => await worker.StartAsync());
            await Task.WhenAll(tasks);
        }
    }
}
