using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalyzer.Parsing.Parser.Html.Contracts
{
    interface IHtmlDocument
    {
        public void Load(string uri);

        public IHtmlNode GetNodeBySelector(string selector);
        public IEnumerable<IHtmlNode> GetNodesByAttribute(String elementType, String attribute, String attributeName);
    }
}
