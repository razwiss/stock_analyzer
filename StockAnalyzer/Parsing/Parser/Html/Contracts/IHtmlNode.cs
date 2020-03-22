using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalyzer.Parsing.Parser.Html.Contracts
{
    interface IHtmlNode
    {
        public string GetText();
        public string GetAttributeValue(string attribute);
        public IHtmlNode GetAncestor();
        public IHtmlNode GetFirstChild();
        public IEnumerable<IHtmlNode> GetNodesBySelector(string selector);
    }
}
