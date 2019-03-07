using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using MonsterJobs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonsterJobs.Admin
{
    public partial class JobsAdmin : Form
    {
        private string TopicPath = "postedjobs";
        private NamespaceManager NamespaceMgr;
        private MessagingFactory Factory;
        private TopicClient JobTopicClient;

        public JobsAdmin()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CreateManagerAndFactory();
            CreateTopicsAndSubscriptions();
        }

        private void CreateManagerAndFactory()
        {
            // Retrieve the connection string.
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            // Create the NamespaceManager
            NamespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);

            // Create the MessagingFactory
            Factory = MessagingFactory.CreateFromConnectionString(connectionString);
        }

        private void CreateTopicsAndSubscriptions()
        {
            // If the topic exists, delete it.
            if (NamespaceMgr.TopicExists(TopicPath))
            {
                NamespaceMgr.DeleteTopic(TopicPath);
            }

            // Create the topic.
            NamespaceMgr.CreateTopic(TopicPath);

            // Subscription for all jobs
            NamespaceMgr.CreateSubscription
                (TopicPath, Models.JobType.FullStack.ToString());

            // Subscriptions for Specific jobs
            NamespaceMgr.CreateSubscription(TopicPath, Models.JobType.WebDeveloper.ToString(),
                new SqlFilter($"JobType = '{Models.JobType.WebDeveloper.ToString()}'"));

            NamespaceMgr.CreateSubscription(TopicPath, Models.JobType.DBA.ToString(),
                new SqlFilter($"JobType = '{Models.JobType.DBA.ToString()}'"));

            NamespaceMgr.CreateSubscription(TopicPath, Models.JobType.MiddleTierDeveloper.ToString(),
                new SqlFilter($"JobType = '{Models.JobType.MiddleTierDeveloper.ToString()}'"));

            // Correlation subscription for Tester
            NamespaceMgr.CreateSubscription(TopicPath, Models.JobType.Testing.ToString(),
                new CorrelationFilter($"{Models.JobType.Testing.ToString()}"));

        }

        private void PostJob_Click(object sender, EventArgs e)
        {
            JobType jobType;

            if (Enum.TryParse<JobType>(JobType.SelectedItem.ToString(), out jobType))
            {
                var job = new Job()
                {
                    JobId = Guid.NewGuid().ToString(),
                    PostedDate = DateTime.Now,
                    JobType = jobType,
                    JobDescription = JobDescription.Text
                };

                SubmitJob(job);
            }
        }

        private void SubmitJob(Job job)
        {
            // Create a TopicClient for Job Topic.
            JobTopicClient = Factory.CreateTopicClient(TopicPath);

            var message = new BrokeredMessage(job);

            // Promote properties.
            message.Properties.Add("JobType", job.JobType.ToString());

            // Set the CorrelationId to the region.
            message.CorrelationId = job.JobType.ToString();

            // Send the message.
            JobTopicClient.Send(message);
        }
    }
}
