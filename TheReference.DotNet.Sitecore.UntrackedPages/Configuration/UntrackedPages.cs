using Sitecore.Analytics.Configuration;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml;

namespace TheReference.DotNet.Sitecore.UntrackedPages.Configuration
{
    public class UntrackedPageList
    {
        private List<string> untrackedPages;

        public UntrackedPageList(List<string> untrackedPages)
        {
            this.untrackedPages = untrackedPages;
        }

        public static UntrackedPageList GetUntrackedPages()
        {
            return new UntrackedPageList(GetUntrackedPageList());
        }

        public bool ContainsUntrackedPage(string page)
        {
            Assert.ArgumentNotNull(page, "page");
            return untrackedPages.Any(untrackedPage => new WildcardPattern(untrackedPage).IsMatch(page));
        }

        private static List<string> GetUntrackedPageList()
        {
            List<string> list = new List<string>();
            XmlNode configNode = Factory.GetConfigNode("tracking/untrackedPages");
            if (configNode != null)
            {
                foreach (XmlNode node in configNode.ChildNodes)
                {
                    if (node.Name == "add")
                    {
                        string str = XmlUtil.GetAttribute("path", node, true);
                        if (str.Contains("?"))
                        {
                            Log.Warn("Query strings are not supported for pages configured under tracking/untrackedPages. Query string was removed from: " + str, (object)typeof(AnalyticsSettings));
                            str = str.Remove(str.IndexOf("?", StringComparison.Ordinal));
                        }
                        if (!str.StartsWith("/"))
                        {
                            Log.Warn("Page path must begin with '/' for pages configured under tracking/untrackedPages. '/' was added to: " + str, (object)typeof(AnalyticsSettings));
                            str = "/" + str;
                        }
                        list.Add(str);
                    }
                }
            }
            return list;
        }
    }
}