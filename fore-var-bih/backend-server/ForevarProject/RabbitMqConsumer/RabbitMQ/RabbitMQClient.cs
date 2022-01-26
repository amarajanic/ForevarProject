using ForevarLibrary.Entities;
using ForevarLibrary.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.RabbitMQ
{
    public class RabbitMQClient
    {
        private IConnection _connection;
        private IModel _channel;
        private string _replyQueueName;
        private EventingBasicConsumer _consumer;

        private string hostName = Environment.GetEnvironmentVariable("RABBIT_HOST");
        private string userName = Environment.GetEnvironmentVariable("RABBIT_USER");
        private string passWord = Environment.GetEnvironmentVariable("RABBIT_PASS");
        private int port = int.Parse(Environment.GetEnvironmentVariable("RABBIT_PORT"));

        
        public void CreateConnection()
        {
            var factory = new ConnectionFactory() { HostName = hostName, UserName = userName, Password = passWord, Port = port };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _replyQueueName = _channel.QueueDeclare(queue: "arduino.payloads",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            MessageToModel(_consumer);

            _channel.BasicConsume(_replyQueueName, true, _consumer);

        }

        public void Close()
        {
            _connection.Close();
        }

        public void MessageToModel(EventingBasicConsumer consumer)
        {
            consumer.Received += (model, ea) =>
            {
                var id = string.Empty;
     
                try
                {
                    if (ea.BasicProperties.Headers != null)
                    {

                        var head = (byte[])ea.BasicProperties.Headers["deviceId"];

                        id = Encoding.UTF8.GetString(head);

                    }
                    else
                    {
                        id = "<NOT_SET>";
                    }


                   var body = ea.Body.ToArray();

                   var message = Encoding.UTF8.GetString(body);

                
                   PayloadEntity payload = JsonConvert.DeserializeObject<PayloadEntity>(message);
                   payload.DeviceId = id;
                   PayloadRepository payloadRepository = new PayloadRepository();

                   payloadRepository.Create(payload);

                   payloadRepository.UpdateDevice(payload);

                   Console.WriteLine(" [x] Received {0}", message + " "+id);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            };
        }
    }
}
