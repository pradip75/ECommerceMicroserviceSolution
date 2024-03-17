using Confluent.Kafka;
using Order.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Application.EventConsumer 
{
    public class ProductChangeEventConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IConsumer<string, string> _consumer;

        public string? BootstrapServers { get; }

        public ProductChangeEventConsumer(string bootstrapServers)
        {
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "order-microservice-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            _cancellationTokenSource = new CancellationTokenSource();
            _consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
        }

        public async Task StartConsumingAsync()
        {
            _consumer.Subscribe("product-changed-topic");

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(_cancellationTokenSource.Token);
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");

                    // Process the product change event here...
                    ProcessProductChangeEvent(consumeResult.Message.Value);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellationTokenSource.Cancel() is called.
            }
            finally
            {
                _consumer.Close();
            }
        }

        public void StopConsuming()
        {
            _cancellationTokenSource.Cancel();
        }

        private void ProcessProductChangeEvent(string eventPayload)
        {
            try
            {
                // Deserialize the JSON payload into a dynamic object
                EventData eventData = JsonSerializer.Deserialize<EventData>(eventPayload);

                // Extract relevant information from the event data
                string operationType = eventData.OperationType; // Assuming 'operationType' is a property in the event payload

                // Determine the type of operation
                switch (operationType)
                {
                    case "Delete":
                        HandleProductDeleteEvent(eventData.ProductId);
                        break;
                    default:
                        // Ignore unsupported operation types or handle accordingly
                        break;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing event payload: {ex.Message}");
                // Optionally, handle deserialization errors
            }
        }

        private async void HandleProductDeleteEvent(int productId)
        {
            //var order = await _orderService.GetOrderByProductId(productId);
            //if (order != null)
            //{
            //    await _orderService.DeleteOrder(order.Id);
            //}
        }
    }
}
