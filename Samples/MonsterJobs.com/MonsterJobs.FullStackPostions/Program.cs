using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using MonsterJobs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterJobs.FullStackPostions
{
    class Program
    {
        private static string TopicPath = "postedjobs";
        private static NamespaceManager NamespaceMgr;
        private static MessagingFactory Factory;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press any key to continue to list FullStack Positions! Wait for admin console to load");
            Console.ReadKey();

            CreateManagerAndFactory();
            // Receive all messages from the ordertopic subscriptions.
            ReceiveFromSubscriptions(TopicPath);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void ReceiveFromSubscriptions(string topicPath)
        {
            // Create a SubscriptionClient
            SubscriptionClient subClient =
                Factory.CreateSubscriptionClient(topicPath, JobType.FullStack.ToString());

            while (true)
            {
                // Recieve any message with a one second timeout.
                BrokeredMessage msg = subClient.Receive(TimeSpan.FromSeconds(1));
                if (msg != null)
                {
                    // Deserialize the message body to an order.
                    var job = msg.GetBody<Job>();
                    Console.WriteLine(job.ToString());

                    // Mark the message as complete.
                    msg.Complete();
                }
                else
                {
                    //Console.WriteLine();
                    //break;
                }
            }

            //subClient.Close();
        }

        static void CreateManagerAndFactory()
        {
            // Retrieve the connection string.
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            // Create the NamespaceManager
            NamespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);

            // Create the MessagingFactory
            Factory = MessagingFactory.CreateFromConnectionString(connectionString);
        }
    }
}
