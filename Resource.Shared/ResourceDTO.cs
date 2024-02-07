using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Shared
{
    public class ResourceDTO
    {
        public int ID { get; set; } // NOT NULL
        public string Name { get; set; } // NOT NULL
        public int Grade { get; set; } // NOT NULL
        public int Own { get; set; } // NOT NULL
        public int Craftable { get; set; } // NOT NULL
        public int? Material1 { get; set; }
        public int? Material2 { get; set; }
        public int? Material3 { get; set; }
        public int Material1Q { get; set; }
        public int Material2Q { get; set; }
        public int Material3Q { get; set; }
        public int LMD { get; set; }
    }
}
