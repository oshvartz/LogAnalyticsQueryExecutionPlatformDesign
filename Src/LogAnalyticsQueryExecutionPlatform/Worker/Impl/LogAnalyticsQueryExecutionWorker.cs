﻿using Azure.Messaging.ServiceBus;
using LogAnalyticsQueryExecutionPlatform.Contracts;
using LogAnalyticsQueryExecutionPlatform.DataModel;
using LogAnalyticsQueryExecutionPlatform.Worker.Contracts;
using LogAnalyticsQueryExecutionPlatform.Worker.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    public abstract class LogAnalyticsQueryExecutionWorker<T> : ILogAnalyticsQueryExecutionWorker
    {


        // connection string to your Service Bus namespace
        static string connectionString = "Endpoint=sb://ofshvart.servicebus.windows.net/;SharedAccessKeyName=Consumer;SharedAccessKey=AVp1zBBF1mOv7yWztn01o50dTgYKtaWziC0NevxFZj8=";

        
        private readonly ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _serviceBusProcessor;
        private ServiceBusSender _serviceBusSender;


        protected LogAnalyticsQueryExecutionWorker()
        {
            _serviceBusClient = new ServiceBusClient(connectionString);


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

        protected virtual IRetryPolicy RetryPolicy { get; } = new DefaultRetryPolicy();
        protected abstract IQueryResultProcessor<T> QueryResultProcessor { get; }
        protected abstract IQueryDefinitionBuilder<T> QueryDefinitionBuilder { get; }
        public abstract string JobType { get; } //must match job type used in the API

        //will be hooked to SB queue by JobType
        private async Task ProcessMessageInternalAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken)
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

            var queryDefinition = await QueryDefinitionBuilder.BuildAsync(jobExecutionContext, cancellationToken);

            //build pipeline Should Skip -> auto disable -> LA Execution
            var middleware =  new ShouldSkipQueryExecutionMiddleware<T>()
                .SetNext(new AutoDisableQueryExecutionMiddleware<T>()
                .SetNext(new LogAnalyticsQueryExecutionMiddleware<T>()));

            //will handle LA errors
            var queryResults = new QueryResults();
            await middleware.ExecuteQueryAsync(queryResults,queryDefinition,jobExecutionContext,cancellationToken);
            await QueryResultProcessor.ProcessQueryResultsAsync(queryResults, jobExecutionContext, cancellationToken);
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

        public async Task StartAsync()
        {
            // create a processor that we can use to process the messages
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(JobType, new ServiceBusProcessorOptions() { AutoCompleteMessages = false, MaxConcurrentCalls = 1 });
            _serviceBusSender = _serviceBusClient.CreateSender(JobType);
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;
            await _serviceBusProcessor.StartProcessingAsync();
        }

        public async Task StopAsync()
        {
            await _serviceBusProcessor.CloseAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
