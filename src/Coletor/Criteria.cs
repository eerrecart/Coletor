using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Coletor
{
    public class Criteria
    {
        public List<int> ParentNodeIds;
        public List<String> NodeTypeAlias;

        public Criteria()
        {
            ParentNodeIds = new List<int>();
            NodeTypeAlias = new List<String>();
        }
    }
}
