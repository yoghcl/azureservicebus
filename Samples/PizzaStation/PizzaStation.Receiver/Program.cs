using Microsoft.ServiceBus.Messaging;
using PizzaStation.Models;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaStation.Receiver
{
    class Program
    {
        private static QueueClient m_QueueClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Receiver Console - Hit enter");
            Console.ReadLine();

            //SimplePizzaReceiveLoop();
            ReceiveAndProcessPizzaOrdesUsingOnMessage(1);

            Console.WriteLine("Receiving, hit enter to exit");
            Console.ReadLine();
        }

        private static void SimplePizzaReceiveLoop()
        {
            // Create a queue client
            QueueClient client = QueueClient.CreateFromConnectionString
                (PizzaStationSettings.ConnectionString, PizzaStationSettings.QueueName);

            while (true)
            {
                Console.WriteLine("Receiving...");

                // Receive a message
                BrokeredMessage message = client.Receive(TimeSpan.FromSeconds(5));

                if (message != null)
                {
                    try
                    {
                        // Process the message
                        var order = message.GetBody<PizzaOrder>();

                        // Process the message
                        CookPizza(order);

                        // Mark the message as complete
                        message.Complete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);

                        // Abandon the message
                        message.Abandon();

                        // Deadletter the message
                        //message.DeadLetter();

                        // Or do nothing
                    }
                }
                else
                {
                    Console.WriteLine("No message present on queue.");
                }
            }
        }

        static void ReceiveAndProcessPizzaOrdesUsingOnMessage(int threads)
        {
            // Create a new client
            m_QueueClient = QueueClient.CreateFromConnectionString
                (PizzaStationSettings.ConnectionString, PizzaStationSettings.QueueName);

            // Set the options for using OnMessage
            var options = new OnMessageOptions()
            {
                AutoComplete = false,
                MaxConcurrentCalls = threads,
                AutoRenewTimeout = TimeSpan.FromSeconds(30)
            };

            // Create a message pump using OnMessage
            m_QueueClient.OnMessage(message =>
            {
                // Deserializse the message body
                var order = message.GetBody<PizzaOrder>();

                // Process the message
                CookPizza(order);

                // Complete the message
                message.Complete();

            }, options);


            Console.WriteLine("Receiving, hit enter to exit");
            Console.ReadLine();
            StopReceiving();
        }

        private static void CookPizza(PizzaOrder order)
        {
            Console.WriteLine("Cooking {0} for {1}.", order.Type, order.CustomerName);
            Thread.Sleep(5000);
            Console.WriteLine("    {0} pizza for {1}.", order.Type, order.CustomerName);
        }

        static void StopReceiving()
        {
            // Close the client, which will stop the message pump.
            m_QueueClient.Close();
        }
    }
}
