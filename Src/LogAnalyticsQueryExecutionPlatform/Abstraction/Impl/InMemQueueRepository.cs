using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction.Impl
{
    public class InMemQueueRepository
    {
        private ConcurrentDictionary<string, BlockingCollection<string>> _queues = new ConcurrentDictionary<string, BlockingCollection<string>>();

        private readonly static InMemQueueRepository _instance = new InMemQueueRepository();

        public void Enqueue(string queueName, string message)
        {
            var queue = _queues.GetOrAdd(queueName, _ => new BlockingCollection<string>());
            queue.Add(message);
        }

        public void StartListening(string queueName,Func<string,Task> onMessageAsync)
        {
            var queue = _queues.GetOrAdd(queueName, _ => new BlockingCollection<string>());
            Task.Run(async () =>
            {
                foreach (string message in queue.GetConsumingEnumerable())
                {
                    await onMessageAsync(message);
                }
            });
            
        }

        public static InMemQueueRepository Instance => _instance;
    }
}
