using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.API.Imp
{
    public class LogAnalyticsQueryExecutionPlatformImpl : ILogAnalyticsQueryExecutionPlatform
    {
        // connection string to your Service Bus namespace
        static string connectionString = "Endpoint=sb://ofshvart.servicebus.windows.net/;SharedAccessKeyName=Consumer;SharedAccessKey=AVp1zBBF1mOv7yWztn01o50dTgYKtaWziC0NevxFZj8=";
        private readonly ServiceBusClient _serviceBusClient;


        public LogAnalyticsQueryExecutionPlatformImpl()
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
        }

        public Task<bool> DeleteJob(JobDefinitionIdentity jobDefinitionIdentity)
        {
            throw new NotImplementedException();
        }

        public Task<JobDescription> GetJob(JobDefinitionIdentity jobDefinitionIdentity)
        {
            throw new NotImplementedException();
        }

        public async Task UpsertJob(JobDescription jobDescription, CancellationToken cancellationToken)
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
            //message.PartitionKey 
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
