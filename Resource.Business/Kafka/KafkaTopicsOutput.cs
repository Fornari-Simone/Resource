using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Business.Kafka
{
    public class KafkaTopicsOutput : AbstractKafkaTopics
    {
        public string Resource { get; set; } = "Resource";
        public override IEnumerable<string> GetTopics()
        {
            return new List<string>() { Resource };
        }
    }
}
