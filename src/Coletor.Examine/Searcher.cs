using Examine;
using Examine.LuceneEngine.Providers;
using Examine.Providers;
using Examine.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Coletor.Examine
{
    public class Searcher : ISearcher
    {
        public Configuration Config()
        {
            return ConfigurationManager.Configuration;
        }

        public IEnumerable<IPublishedContent> Search(Criteria criteria, Options options = null)
        {
            throw new NotImplementedException();

            return new List<IPublishedContent>();
        }

        public ISearchResults ExamineSearch(Criteria criteria, Options options = null)
        {
            if (options == null)
                options = new Options();

            var luceneCriteria = GetDefaultSearchCriteria();
            
            var q = luceneCriteria.Field(Config().SpacedPathFieldName, "-1"); // FUGLY

            AppendParentNodeIdsFilter(ref q, criteria.ParentNodeIds);
            AppendDocumentTypesFilter(ref q, criteria.NodeTypeAlias);
            //AppendOrderFilter(ref q, criteria.OrderBy);

            var search = q.Compile();

            var results = GetSearchProvider().Search(search, options.Limit);

            return results;
        }

        protected IBooleanOperation AppendParentNodeIdsFilter(ref IBooleanOperation q, List<int> ids)
        {
            if (ids.Any())
            {
                var values = ids.Select(i => i.ToString()).ToArray();
                q = q.And().GroupedOr(new string[] { Config().SpacedPathFieldName }, values);

                foreach (var id in ids)
                    q = q.Not().Field("id", id.ToString());
            }

            return q;
        }

        protected IBooleanOperation AppendDocumentTypesFilter(ref IBooleanOperation q, List<String> docTypes )
        {
            if (docTypes.Any())
                q = q.And().GroupedOr(new string[] { "nodeTypeAlias" }, docTypes.ToArray());

            return q;
        }

        protected IBooleanOperation AppendOrderFilter(ref IBooleanOperation q, List<String> fields)
        {
            if (fields.Any())
                q = q.And().OrderBy(fields.ToArray());

            return q;
        }

        protected BaseLuceneSearcher GetSearchProvider()
        {
            return (BaseLuceneSearcher)ExamineManager.Instance.SearchProviderCollection["ExternalSearcher"];
        }

        protected ISearchCriteria GetDefaultSearchCriteria()
        {
            return GetDefaultSearchCriteria(GetSearchProvider());
        }

        protected ISearchCriteria GetDefaultSearchCriteria(BaseLuceneSearcher searcher)
        {
            return searcher.CreateSearchCriteria(UmbracoExamine.IndexTypes.Content);
        }
    }

    public class IndexedContent : IPublishedContent
    {

        public IEnumerable<IPublishedContent> Children
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CreateDate
        {
            get { throw new NotImplementedException(); }
        }

        public int CreatorId
        {
            get { throw new NotImplementedException(); }
        }

        public string CreatorName
        {
            get { throw new NotImplementedException(); }
        }

        public string DocumentTypeAlias
        {
            get { throw new NotImplementedException(); }
        }

        public int DocumentTypeId
        {
            get { throw new NotImplementedException(); }
        }

        public IPublishedContentProperty GetProperty(string alias)
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
        }

        public PublishedItemType ItemType
        {
            get { throw new NotImplementedException(); }
        }

        public int Level
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public IPublishedContent Parent
        {
            get { throw new NotImplementedException(); }
        }

        public string Path
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<IPublishedContentProperty> Properties
        {
            get { throw new NotImplementedException(); }
        }

        public int SortOrder
        {
            get { throw new NotImplementedException(); }
        }

        public int TemplateId
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime UpdateDate
        {
            get { throw new NotImplementedException(); }
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
        }

        public string UrlName
        {
            get { throw new NotImplementedException(); }
        }

        public Guid Version
        {
            get { throw new NotImplementedException(); }
        }

        public int WriterId
        {
            get { throw new NotImplementedException(); }
        }

        public string WriterName
        {
            get { throw new NotImplementedException(); }
        }

        public object this[string propertyAlias]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
