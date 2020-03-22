using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;
using StockAnalyzer.Parsing.Parser.Html.Contracts;

namespace StockAnalyzer.Parsing.Parser.Html
{
    class HtmlDocument: IHtmlDocument
    {
        private HtmlAgilityPack.HtmlWeb Web;
        private HtmlAgilityPack.HtmlDocument Document;

        public HtmlDocument(HtmlAgilityPack.HtmlWeb web)
        {
            this.Web = web;
        }

        public void Load(string uri)
        {
            this.Document = this.Web.Load(uri);
        }

        public IHtmlNode GetNodeBySelector(string selector)
        {
            return new HtmlNode(this.Document.DocumentNode.SelectSingleNode(selector));
        }

        /**
         *  GetElementWithClass and ClassListContains are derived from https://stackoverflow.com/questions/13771083/html-agility-pack-get-all-elements-by-class
         *  TL;DR: HtmlAgilityPack Selector does not support searching in multiple classes
         *  And Fizzle/CssSelectors crash when searching some class names like Yahoo finance's "Ta(End)" 
         */
        public IEnumerable<IHtmlNode> GetNodesByAttribute(String elementType, String attribute, String attributeName)
        {
            List<HtmlNode> nodesConverted = new List<HtmlNode>();

            IEnumerable<HtmlAgilityPack.HtmlNode> nodes = this.Document.DocumentNode.Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Element)
                .Where(e =>
                    e.Name == elementType &&
                    this.ClassListContains(e.GetAttributeValue(attribute, ""), attributeName, StringComparison.Ordinal)
                );

            nodes.ToList().ForEach(n => nodesConverted.Add(new HtmlNode(n)));
            return nodesConverted;
        }

        private Boolean ClassListContains(String haystack, String needle, StringComparison comparison)
        {
            if (String.Equals(haystack, needle, comparison)) return true;

            Int32 idx = 0;

            while (idx + needle.Length <= haystack.Length)
            {
                idx = haystack.IndexOf(needle, idx, comparison);
                if (idx == -1) return false;

                Int32 end = idx + needle.Length;

                // Needle must be enclosed in whitespace or be at the start/end of string
                Boolean validStart = idx == 0 || Char.IsWhiteSpace(haystack[idx - 1]);
                Boolean validEnd = end == haystack.Length || Char.IsWhiteSpace(haystack[end]);
                if (validStart && validEnd) return true;

                idx++;
            }

            return false;
        }
    }
}
