using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Repository.Model
{
    public class TransitionalOutbox
    {
        public long ID { get; set; }
        public string Tabella { get; set; } = string.Empty;
        public string Messaggio { get; set; } = string.Empty;
    }
}
