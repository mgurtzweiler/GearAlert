using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.IO;
using GearAlert.Web.Models.Pubsub;

namespace GearAlert.Web.Helpers {

    public class HubChallengeInputModelBinder : IModelBinder {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var model = new HubChallengeInputModel();

            model.HubChallenge = controllerContext.HttpContext.Request["hub.challenge"];

            return model;
        }

        #endregion
    }

    //public class HubCallbackInputModelBinder : IModelBinder {
    //    #region IModelBinder Members

    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
    //        using (XmlReader xmlReader = new XmlTextReader(controllerContext.HttpContext.Request.InputStream)) {
    //            try {
    //                var feed = SyndicationFeed.Load(xmlReader);
    //                var model = new HubCallbackInputModel();
    //                model.FeedUrl = feed.Id;
    //                model.FeedTitle = (feed.Title != null) ? feed.Title.Text : string.Empty;
    //                if (feed.Items != null && feed.Items.Count() > 0) {
    //                    var item = feed.Items.First();
    //                    model.ItemTitle = (item.Title != null) ? item.Title.Text : string.Empty;
    //                    model.ItemId = item.Id;
    //                    //model.ItemDescription = (item.Summary != null) ? item.Summary.Text : string.Empty;
    //                    model.ItemLink = (item.Links != null && item.Links.Count > 0) ? item.Links.First().Uri.ToString() : string.Empty;
    //                    ParseDescription((item.Summary != null) ? item.Summary.Text : string.Empty, model);
    //                } 
    //                return model;
    //            } catch {
    //                return null;
    //            }
    //        }
    //    }

    //    #endregion

    //    public void ParseDescription(string description, HubCallbackInputModel modelToPopulate) {
    //        if (string.IsNullOrEmpty(description))
    //            return;
    //        string afterDecode = processString(description);
    //        XDocument doc = XDocument.Parse(afterDecode);
    //        var imgElement = doc.Descendants("img").FirstOrDefault();
    //        if (imgElement != null && imgElement.Attribute("src") != null) {
    //            modelToPopulate.ItemImageUrl = imgElement.Attribute("src").Value;
    //        }
    //        modelToPopulate.ItemDescription = doc.Descendants("html").First().Value;
    //    }

    //    private string processString(string strInputHtml) {
    //        SgmlReader sgmlReader = new SgmlReader();
    //        sgmlReader.DocType = "HTML";
    //        StringReader sr = new StringReader(strInputHtml);
    //        sgmlReader.InputStream = sr;
    //        StringWriter sw = new StringWriter();
    //        XmlTextWriter w = new XmlTextWriter(sw);
    //        sgmlReader.Read();
    //        w.WriteStartDocument();
    //        while (!sgmlReader.EOF) {
    //            w.WriteNode(sgmlReader, true);
    //        }
    //        w.Flush();
    //        w.Close();
    //        return sw.ToString();
    //    }

   //}
}