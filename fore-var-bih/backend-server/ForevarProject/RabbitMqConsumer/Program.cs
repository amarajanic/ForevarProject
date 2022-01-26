using ForevarLibrary.Entities;
using ForevarLibrary.Models;
using ForevarLibrary.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMqConsumer.RabbitMQ;

namespace RabbitMqConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQClient client = new RabbitMQClient();
           
            while (true)
            {
                try
                {
                    client.CreateConnection();
                    Console.WriteLine("Connection open");
                    client.Close();
                    Console.WriteLine("Connection closed");
                }
                catch(Exception err)
                {
                    Console.WriteLine("Connection not established. Error details: " + err);
                }
                Thread.Sleep(300000);
                //ArduinoSHT21
            }


        }
    }
}
