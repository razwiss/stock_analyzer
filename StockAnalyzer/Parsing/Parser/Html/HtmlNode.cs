using StockAnalyzer.Parsing.Parser.Html.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAnalyzer.Parsing.Parser.Html
{
    class HtmlNode: IHtmlNode
    {
        private HtmlAgilityPack.HtmlNode Node;

        public HtmlNode(HtmlAgilityPack.HtmlNode node)
        {
            this.Node = node;
        }

        public string GetText()
        {
            return this.Node.InnerText;
        }

        public string GetAttributeValue(string attribute)
        {
            return this.Node.Attributes[attribute].Value;
        }

        public IHtmlNode GetAncestor()
        {
            return new HtmlNode(this.Node.Ancestors().First());
        }

        public IEnumerable<IHtmlNode> GetNodesBySelector(string selector)
        {
            List<IHtmlNode> nodesConverted = new List<IHtmlNode>();
            IEnumerable<HtmlAgilityPack.HtmlNode> nodes = this.Node.SelectNodes(selector);

            nodes.ToList().ForEach(n => nodesConverted.Add(new HtmlNode(n)));

            return nodesConverted;
        }

        public IHtmlNode GetFirstChild()
        {
            return new HtmlNode(this.Node.ChildNodes.First());
        }
    }
}
