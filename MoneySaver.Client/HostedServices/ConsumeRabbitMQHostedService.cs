using Common.Methods;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySaver.Client.HostedServices
{
    public class ConsumeRabbitMQHostedService : BackgroundService
    {
        private readonly RabbitModelPooledObjectPolicy rabbitModel;
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;

        public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory, RabbitModelPooledObjectPolicy con)
        {
            this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
            this.rabbitModel = con;
            this._channel = con.Create();
            //this._connection = con.Connection;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            //var factory = new ConnectionFactory { HostName = "localhost" };

            //// create connection  
            //_connection = factory.CreateConnection();

            // create channel  


            _channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("demo.queue.log", false, false, false, null);
            _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  


                // handle the received message  
                HandleMessage(ea.Body.ToArray());
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("demo.queue.log", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(byte[] content)
        {
            // we just print this message  
            var result = System.Text.Encoding.UTF8.GetString(content);

            //T resultObject = JsonConvert.DeserializeObject<T>(result);
            _logger.LogInformation($"consumer received {content}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
