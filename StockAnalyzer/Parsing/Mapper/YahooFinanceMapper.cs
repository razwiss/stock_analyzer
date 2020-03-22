using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalyzer.Parsing.Mapper
{
    class YahooFinanceMapper: StockMapper
    {
        public YahooFinanceMapper()
        {
            map = new Dictionary<string, string>()
            {
                {"Ticker", "Ticker"},
                {"Name", "COMPANY"},
                {"Current", "PRICE"},
                {"Open", "OPEN"},
            };
        }
    }
}
