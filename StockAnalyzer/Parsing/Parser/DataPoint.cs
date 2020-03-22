using System;
using System.Collections.Generic;
using System.Text;

namespace StockAnalyzer.Parsing.Parser
{
   struct DataPoint
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public DataPoint(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => $"{Name}: {Value}";
    }
}
