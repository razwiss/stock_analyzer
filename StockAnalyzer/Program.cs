
using System;
using System.Collections.Generic;

using System.Text;

namespace StockAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {

            Parsing.Parser.YahooFinance yahoo = new Parsing.Parser.YahooFinance(
                new Parsing.Parser.Html.HtmlDocument(new HtmlAgilityPack.HtmlWeb())
            ) { Ticker = "CTC-A.TO" };

            yahoo.RetrieveSummary();
        }
    }
}
