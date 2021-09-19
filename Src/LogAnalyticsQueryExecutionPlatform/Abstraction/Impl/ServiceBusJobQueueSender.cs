using Azure.Messaging.ServiceBus;
using LogAnalyticsQueryExecutionPlatform.API;
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
    public class ServiceBusJobQueueSender : IJobQueueSender
    {
        static string connectionString = "XXXX";
        private readonly ServiceBusClient _serviceBusClient;
        public ServiceBusJobQueueSender()
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
        }
        public async Task EnqueueJob(JobDescription jobDescription, CancellationToken cancellationToken)
        {
            var jobDefinition = jobDescription.JobDefinition;
            var jobScheduling = jobDescription.JobScheduling;
            var serviceBusSender = _serviceBusClient.CreateSender(jobDefinition.JobType);

            ServiceBusMessage message = new ServiceBusMessage(ToJsonBinaryData(jobDefinition.JobData));
            var fireInstanceId = Guid.NewGuid().ToString();
            var fireLogicTimeUtc = DateTime.UtcNow;
            message.ApplicationProperties["FireLogicTimeUtc"] = fireLogicTimeUtc;
            message.ApplicationProperties["FireInstanceId"] = fireInstanceId;
            message.ApplicationProperties["RetryCount"] = 0;
            message.ApplicationProperties["JobId"] = jobDefinition.JobId;
            message.ApplicationProperties["JobScheduling"] = jobScheduling != null ? JsonSerializer.Serialize<JobScheduling>(jobScheduling) : string.Empty;
            message.ContentType = "application/json";
            //we will support on delay send in phase 1
            if (jobScheduling?.SimpeleJobScheduling?.StartTimeUtc != null)
            {
                message.ScheduledEnqueueTime = jobScheduling.SimpeleJobScheduling.StartTimeUtc.Value;
            }

            //used for DDup
            message.MessageId = $"{ jobDefinition.JobType}_{jobDefinition.JobId}_{fireLogicTimeUtc.ToString("O")}";

            await serviceBusSender.SendMessageAsync(message, cancellationToken);

            await serviceBusSender.CloseAsync();
        }

        public static byte[] ToJsonBinaryData(JsonDocument jdoc)
        {
            using (var stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                jdoc.WriteTo(writer);
                writer.Flush();
                return stream.ToArray();
            }
        }
    }
}
