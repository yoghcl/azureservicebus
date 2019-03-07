using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MonsterJobs.Models
{
    public enum JobType
    {
        FullStack,
        WebDeveloper,
        MiddleTierDeveloper,
        DBA,
        BA,
        Testing,
        Architect
    }

    [DataContract]
    public class Job
    {
        [DataMember]
        public string JobId { get; set; }

        [DataMember]
        public string JobDescription { get; set; }

        [DataMember]
        public DateTime PostedDate { get; set; }

        [DataMember]
        public JobType JobType { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine($"Job Id: {JobId}");
            sb.AppendLine($"Job Type: {JobType}");
            sb.AppendLine($"Job Description: {JobDescription}");
            sb.AppendLine($"Posting Date: {PostedDate.ToLongDateString()}");

            return sb.ToString();
        }
    }
}
