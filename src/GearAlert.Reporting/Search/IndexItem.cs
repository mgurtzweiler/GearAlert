using System;
using Lucene.Net.Documents;

namespace GearAlert.Infrastructure.Search {

    public class IndexItem {
        public string AlertId { get; set; }
        public string FeedId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public DateTime Timestamp { get; set; }

        public Document GetDocument() {
            Document document = new Document();
            document.Add(new Field("Id", AlertId, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("FeedId", FeedId, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("Title", Title, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Summary", Summary, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Url", Url, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("Timestamp", Timestamp.ToUniversalTime().ToString("yyyyMMddHHmmss"), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            return document;
        }

        public string IdField {
            get { return "Id"; }
        }

        public string[] IndexedFields {
            get { return new[] { "Title", "Summary" }; }
        }
    }
}
