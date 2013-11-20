using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examine;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Coletor.Examine
{
    static public class Mapper
    {
        /// <summary>
        /// get a bag of search results and tries to map them to IPublishedContent, avoid this if you are expecting ultra high performance.
        /// </summary>
        /// <param name="items">items to map.</param>
        /// <returns>enumerable of published content ready to work with umbraco front end.</returns>
        public IEnumerable<IPublishedContent> GetContent(ISearchResults items)
        {
            var list = new List<IPublishedContent>();

            if (items != null || !items.Any())
            {
                //Umbraco.Web.PublishedCache.ContextualPublishedCache
                foreach (var result in items.OrderByDescending(x => x.Score))
                {
                    var doc = UmbracoContext.Current.ContentCache.GetById(result.Id);
                    if (doc == null) continue; //skip if this doesn't exist in the cache				

                    list.Add(doc);
                }
            }
            
            return list;
        }
    }
}
