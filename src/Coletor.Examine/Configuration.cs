using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor.Examine
{
    public class Configuration
    {
        public string IndexerName { get; set; }
        public string SpacedPathFieldName { get; set; }
        public string PathFieldName { get; set; }
        public List<string> SortableFields { get; set; }
    }

    
}
