using LogAnalyticsQueryExecutionPlatform.API;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction.Impl
{
    public class InMemJobQueueListener<T> : IJobQueueListener<T>
    {
        public Task StartProcessingAsync(string queueName, Func<JobExecutionContext<T>, CancellationToken, Task> processMessageHandler)
        {
            InMemQueueRepository.Instance.StartListening(queueName, message =>
             {
                 var jobDescription =  JsonSerializer.Deserialize<JobDescription>(message);

                 T data = JsonSerializer.Deserialize<T>(ToJsonString(jobDescription.JobDefinition.JobData));

                 var now = DateTime.UtcNow;
                 var jobExecutionContext = new JobExecutionContext<T>
                 {
                     FireActualTimeUtc = now,
                     FireLogicTimeUtc = now,
                     FireInstanceId = Guid.NewGuid().ToString(),
                     RetryCount = 0,
                     JobDefinition = new JobDefinition<T>
                     {
                         JobData = data,
                         JobId = jobDescription.JobDefinition.JobId,
                         JobScheduling = jobDescription.JobScheduling,
                         JobType = jobDescription.JobDefinition.JobType
                     }

                     
                 };
                 processMessageHandler(jobExecutionContext, CancellationToken.None);
             });

            return Task.CompletedTask;
        }

        public static string ToJsonString(JsonDocument jdoc)
        {
            using (var stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                jdoc.WriteTo(writer);
                writer.Flush();
                return System.Text.Encoding.UTF8.GetString( stream.ToArray());
            }
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
