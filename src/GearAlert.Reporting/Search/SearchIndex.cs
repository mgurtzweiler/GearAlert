using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace GearAlert.Infrastructure.Search
{
    public class SearchIndex {

        public SearchIndex()
        {
            IndexDirectory = @"./Index";
        }

        public bool QueryMatchesSingleItem(IndexItem item, string searchQuery) {
            Directory directory = null;
            IndexWriter indexWriter = null;
            Analyzer analyzer = null;
            IndexReader reader = null;
            Searcher searcher = null;
            try {
                directory = new RAMDirectory();
                try {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
                    indexWriter.AddDocument(item.GetDocument());
                    indexWriter.Optimize();
                } finally {
                    if (indexWriter != null)
                        indexWriter.Close();
                }

                reader = IndexReader.Open(directory, true);
                searcher = new IndexSearcher(reader);

                MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                                                                         item.IndexedFields,
                                                                         analyzer);

                Query query = parser.Parse(searchQuery);

                TopScoreDocCollector collector = TopScoreDocCollector.create(1, true);

                searcher.Search(query, collector);

                ScoreDoc[] hits = collector.TopDocs().scoreDocs;
                return hits.Length > 0;
            } catch (Exception ex) {
                return false;
            } finally {
                if (analyzer != null)
                    analyzer.Close();
                if (reader != null)
                    reader.Clone();
                if (searcher != null)
                    searcher.Close();
                if (directory != null)
                    directory.Close();
            }
        }

        public IList<string> WhichQueriesMatchSingleItem(IndexItem item, IDictionary<string, string> queries) {
            Directory directory = null;
            IndexWriter indexWriter = null;
            Analyzer analyzer = null;
            IndexReader reader = null;
            Searcher searcher = null;
            try {
                directory = new RAMDirectory();
                try {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
                    indexWriter.AddDocument(item.GetDocument());
                    indexWriter.Optimize();
                } finally {
                    if (indexWriter != null)
                        indexWriter.Close();
                }

                reader = IndexReader.Open(directory, true);
                searcher = new IndexSearcher(reader);

                MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                                                                         item.IndexedFields,
                                                                         analyzer);
                var results = new List<string>();
                foreach (var query in queries) {
                    Query q = parser.Parse(query.Value);

                    TopScoreDocCollector collector = TopScoreDocCollector.create(1, true);

                    searcher.Search(q, collector);

                    ScoreDoc[] hits = collector.TopDocs().scoreDocs;
                    if (hits.Length > 0)
                        results.Add(query.Key);
                }

                return results;
            } catch (Exception ex) {
                return new List<string>();
            } finally {
                if (analyzer != null)
                    analyzer.Close();
                if (reader != null)
                    reader.Clone();
                if (searcher != null)
                    searcher.Close();
                if (directory != null)
                    directory.Close();
            }
        }

        public void RebuildIndex(IEnumerable<IndexItem> items) {
            FSDirectory directory = FSDirectory.Open(new System.IO.DirectoryInfo(IndexDirectory));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            IndexWriter indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            foreach (var item in items) {
                indexWriter.AddDocument(item.GetDocument());
            }
            indexWriter.Optimize();
            analyzer.Close();
            indexWriter.Close();
        }

        public IEnumerable<IndexItem> QueryIndex(string searchQuery) {
            try {
                FSDirectory directory = FSDirectory.Open(new System.IO.DirectoryInfo(IndexDirectory));
                IndexReader reader = IndexReader.Open(directory, true);
                Searcher searcher = new IndexSearcher(reader);
                Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                                                                         new[] { "Title", "Summary" },
                                                                         analyzer);
                Query query = parser.Parse(searchQuery);
                var collector = TopFieldCollector.create(new Sort(new SortField("Timestamp", SortField.LONG, true)), 10, true, true, true, true);
                searcher.Search(query, collector);
                ScoreDoc[] hits = collector.TopDocs().scoreDocs;
                List<IndexItem> items = new List<IndexItem>();
                foreach (ScoreDoc scoreDoc in hits) {
                    //Get the document that represents the search result.
                    Document document = searcher.Doc(scoreDoc.doc);
                    var item = new IndexItem
                                   {
                                       AlertId = document.Get("AlertId"),
                                       FeedId = document.Get("FeedId"),
                                       Summary = document.Get("Summary"),
                                       Title = document.Get("Title"),
                                       Url = document.Get("Url"),
                                       Timestamp = DateTime.Parse(document.Get("Timestamp"))
                                   };

                    //The same document can be returned multiple times within the search results.

                    if (!items.Contains(item)) {
                        items.Add(item);
                    }
                }
                //Now that we have the product Ids representing our search results, retrieve the products from the database.
                reader.Close();
                searcher.Close();
                analyzer.Close();
                return items;
            } catch (Exception ex) {
                throw new Exception(ex.Message, ex);
            }
        }

        public void AddItemToIndex(IndexItem item) {
            FSDirectory directory = FSDirectory.Open(new System.IO.DirectoryInfo(IndexDirectory));
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            var directoryInfo = new DirectoryInfo(IndexDirectory);
            var createNewIndex = false;
            if(directoryInfo.GetFiles().Length == 0)
            {
                createNewIndex = true;
            }

            IndexWriter indexWriter = new IndexWriter(directory, analyzer, createNewIndex, IndexWriter.MaxFieldLength.UNLIMITED);
            indexWriter.AddDocument(item.GetDocument());
            indexWriter.Optimize();
            analyzer.Close();
            indexWriter.Close();
        }

        public string IndexDirectory { get; set; }
    }
}