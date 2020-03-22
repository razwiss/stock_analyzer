using StockAnalyzer.Parsing.Parser.Html.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAnalyzer.Parsing.Parser
{
    class YahooFinance: BaseParser
    {
        private static readonly string BASEURL = "https://finance.yahoo.com/quote/";
        public string Ticker { get; set; }

        private List<DataPoint> DataPoints;

        private IHtmlDocument Document;

        public YahooFinance(IHtmlDocument document)
        {
            this.Document = document;
            this.DataPoints = new List<DataPoint>();
        }

        public void RetrieveSummary()
        {
            this.Document.Load(BASEURL + this.Ticker);

            this.GetQuote();

            IEnumerable<IHtmlNode> nodes = this.Document.GetNodesByAttribute("td", "class", "Ta(end)");
            nodes.ToList().ForEach(C_GetDataPoint);

            //this.DataPoints.ToList().ForEach(n => Console.WriteLine(n.ToString()));

            Dictionary<string, string> dc = new Dictionary<string, string>();
            dc.Add("Ticker", this.Ticker);
            this.DataPoints.ForEach(n => dc.Add(n.Name, n.Value));

            Stock s = new Mapper.YahooFinanceMapper().Map(ref dc);

            Console.WriteLine(s.ToString());
        }

        private void GetQuote()
        {
            this.DataPoints.Add(new DataPoint(
               name: "COMPANY",
               value: this.Document.GetNodeBySelector("//h1").GetText()
           ));

            IHtmlNode quoteHeader = this.Document.GetNodesByAttribute("div", "id", "quote-market-notice").First();
            IHtmlNode[] items = quoteHeader.GetAncestor().GetNodesBySelector("span").ToArray();

            this.DataPoints.Add(new DataPoint(
                name: "PRICE",
                value: items[0].GetText()
            ));

            this.DataPoints.Add(new DataPoint(
                name: "VARIATION",
                value: items[1].GetText()
            ));

        }

        private void C_GetDataPoint(IHtmlNode node)
        {
            this.DataPoints.Add(new DataPoint(
                name: node.GetAttributeValue("data-test").Replace("-value", ""),
                value: node.GetFirstChild().GetText()
            ));           
        }
    }
}
