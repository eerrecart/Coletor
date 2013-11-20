using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Coletor
{
    public class Options
    {
        public int Limit;
        public List<String> OrderBy;

        public Options()
        {
            Limit = 0;
            OrderBy = new List<String>();
        }
    }
}
