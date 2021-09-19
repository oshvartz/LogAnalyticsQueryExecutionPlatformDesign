using Azure.Messaging.ServiceBus;
using LogAnalyticsQueryExecutionPlatform.API;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using LogAnalyticsQueryExecutionPlatform.Impl;
using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Abstraction.Impl
{
    public class ServiceBusJobQueueListener<T> : IJobQueueListener<T>
    {
        // connection string to your Service Bus namespace
        static string connectionString = "XXXXX";

        private readonly ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _serviceBusProcessor;
        private ServiceBusSender _serviceBusSender;
        private Func<JobExecutionContext<T>, CancellationToken, Task> _processMessageHandler;

        public ServiceBusJobQueueListener()
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
        }

        public async Task StartProcessingAsync(string queueName, IRetryPolicy retryPolicy, Func<JobExecutionContext<T>, CancellationToken, Task> processMessageHandler)
        {
            _processMessageHandler = processMessageHandler;
            // create a processor that we can use to process the messages
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions() { AutoCompleteMessages = false, MaxConcurrentCalls = 1 });
            _serviceBusSender = _serviceBusClient.CreateSender(queueName);
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;
            await _serviceBusProcessor.StartProcessingAsync();
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception);
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            try
            {
                await ProcessMessageInternalAsync(arg.Message, arg.CancellationToken);
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (CorruptedMessageException)
            {
                await arg.DeadLetterMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {
                var retryCount = (int)arg.Message.ApplicationProperties["RetryCount"];
                if (retryCount > 5) // and is transient
                {
                    Console.WriteLine("DEAD-LETTERING");
                    await arg.DeadLetterMessageAsync(arg.Message, new Dictionary<string, object> { { "RetryCount", retryCount + 1 } });
                }
                else
                {
                    // Send a clone with a deferred wait of 5 seconds
                    ServiceBusMessage clone = new ServiceBusMessage(arg.Message);

                    clone.ScheduledEnqueueTime = DateTime.UtcNow.AddMinutes(1); //By retry policy
                    retryCount = retryCount + 1;
                    clone.ApplicationProperties["RetryCount"] = retryCount;
                    clone.MessageId = $"{clone.MessageId}_{retryCount}";
                    await _serviceBusSender.SendMessageAsync(clone);

                    // Remove message from queue.
                    await arg.CompleteMessageAsync(arg.Message);
                }
            }
        }

        private  async Task ProcessMessageInternalAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken)
        {
            JobExecutionContext<T> jobExecutionContext = null;
            try
            {
                jobExecutionContext = ReadContext(message);
            }
            catch (Exception ex)
            {
                throw new CorruptedMessageException(ex);
            }

            await _processMessageHandler(jobExecutionContext, cancellationToken);
        }


        private JobExecutionContext<T> ReadContext(ServiceBusReceivedMessage message)
        {
            var jobSchedulingStr = (string)message.ApplicationProperties["JobScheduling"];
            return new JobExecutionContext<T>
            {
                FireActualTimeUtc = DateTime.UtcNow,
                FireLogicTimeUtc = (DateTime)message.ApplicationProperties["FireLogicTimeUtc"],
                FireInstanceId = (string)message.ApplicationProperties["FireInstanceId"],
                RetryCount = (int)message.ApplicationProperties["RetryCount"],
                JobDefinition = new JobDefinition<T>
                {
                    JobId = (string)message.ApplicationProperties["JobId"],
                    JobData = message.Body.ToObjectFromJson<T>(),
                    JobScheduling = string.IsNullOrEmpty(jobSchedulingStr) ? null : JsonSerializer.Deserialize<JobScheduling>(jobSchedulingStr)
                }
            };
        }

        public  async Task StopAsync()
        {
            await _serviceBusProcessor.CloseAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
