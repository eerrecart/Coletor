using Coletor;
using Coletor.Examine;
using Examine;
using Examine.LuceneEngine;
using Examine.LuceneEngine.Providers;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using umbraco.businesslogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using umbraco.interfaces;
using umbraco.NodeFactory;
using Umbraco.Core;
using Umbraco.Web;
using UmbracoExamine;

namespace Umbraco_Site_Extensions.examineExtensions
{
    public class ColetorExamineEvents : ApplicationEventHandler
    {
        

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UmbracoContentIndexer indexer = (UmbracoContentIndexer)ExamineManager.Instance.IndexProviderCollection[ConfigurationManager.Configuration.IndexerName];
            indexer.GatheringNodeData += ColetorExamineEvents_GatheringNodeData;
            indexer.DocumentWriting += ColetorExamineEvents_DocumentWriting;
        }

        void ColetorExamineEvents_GatheringNodeData(object sender, IndexingNodeDataEventArgs e)
        {
            if (e.Fields.ContainsKey(ConfigurationManager.Configuration.PathFieldName))
            {
                string path = e.Fields[ConfigurationManager.Configuration.PathFieldName].Replace(",", " ");

                if (!e.Fields.ContainsKey(ConfigurationManager.Configuration.SpacedPathFieldName))
                {
                    e.Fields.Add(ConfigurationManager.Configuration.SpacedPathFieldName, path);
                }
                else
                {
                    e.Fields[ConfigurationManager.Configuration.SpacedPathFieldName] = path;
                }
            }
        }

        void ColetorExamineEvents_DocumentWriting(object sender, DocumentWritingEventArgs e)
        {
            foreach (var fieldName in ConfigurationManager.Configuration.SortableFields)
            {
                if (e.Fields.Keys.Contains(fieldName))
                {
                    var sortableField = new Field( LuceneIndexer.SortedFieldNamePrefix + fieldName, e.Fields[fieldName], Field.Store.YES, Field.Index.NOT_ANALYZED);
                    e.Document.Add(sortableField);
                }    
            }

        }
    }
}