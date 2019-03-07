using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using PizzaStation.Models;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStation.Sender
{
    class Program
    {
        /// <summary>
        /// Warming up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            InitializePizzaStation();

            Console.WriteLine("Pizza Station Console - Complete");
            Console.ReadLine();
        }

        /// <summary>
        /// Intializes the Pizza station
        /// </summary>
        static void InitializePizzaStation()
        {
            CreateQueue();
            TakeSingleOrder();
            TakeBulkOrders();
        }

        /// <summary>
        /// Create the queue
        /// </summary>
        static void CreateQueue()
        {
            var manager = NamespaceManager.CreateFromConnectionString(PizzaStationSettings.ConnectionString);
            if (!manager.QueueExists(PizzaStationSettings.QueueName))
            {
                Console.WriteLine("Creating queue: " + PizzaStationSettings.QueueName + "...");
                manager.CreateQueue(PizzaStationSettings.QueueName);
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("queue for pizza already exists");
            }
        }

        /// <summary>
        /// taking pizza order
        /// </summary>
        private static void TakeSingleOrder()
        {
            var order = new PizzaOrder()
            {
                CustomerName = "Alan Smith",
                Type = "Hawaiian",
                Size = "Large"
            };

            // Create a BrokeredMessage
            var message = new BrokeredMessage(order)
            {
                Label = "PizzaOrder"
            };

            // Send the message...
            var client = QueueClient.CreateFromConnectionString
                (PizzaStationSettings.ConnectionString, PizzaStationSettings.QueueName);

            Console.WriteLine("Sending order...");

            client.Send(message);

            Console.WriteLine("Done!");

            client.Close();
        }

        static void TakeBulkOrders()
        {
            // Create some data
            string[] names = { "Alan", "Jennifer", "James" };
            string[] pizzas = { "Hawaiian", "Vegitarian", "Capricciosa", "Napolitana" };

            // Create a queue client
            var client = QueueClient.CreateFromConnectionString
                (PizzaStationSettings.ConnectionString, PizzaStationSettings.QueueName);

            // Send a batch of pizza orders
            var taskList = new List<Task>();
            for (int pizza = 0; pizza < pizzas.Length; pizza++)
            {
                for (int name = 0; name < names.Length; name++)
                {

                    PizzaOrder order = new PizzaOrder()
                    {
                        CustomerName = names[name],
                        Type = pizzas[pizza],
                        Size = "Large"
                    };
                    var message = new BrokeredMessage(order);

                    taskList.Add(client.SendAsync(message));
                }
            }

            Console.WriteLine("Sending batch...");
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine("Sent!");
        }
    }
}
