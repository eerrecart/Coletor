using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Coletor
{
    public interface ISearcher
    {
        IEnumerable<IPublishedContent> Search(Criteria criteria, Options options);
    }
}