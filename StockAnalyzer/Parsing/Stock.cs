using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalyzer
{
    class Stock
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Current { get; set; }
        public string Open { get; set; }

        public override string ToString() => $"{Ticker} -- {Name}: {Current} ({Open})";
    }
}
