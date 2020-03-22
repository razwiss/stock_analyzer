using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace StockAnalyzer.Parsing.Mapper
{
    class StockMapper
    {
        protected static Dictionary<string, string> map;

        public Stock Map(ref Dictionary<string, string> parsedValues)
        {
            Stock stock = new Stock();

            foreach (KeyValuePair<string, string> entry in map)
            {
                PropertyInfo prop = stock.GetType().GetProperty(entry.Key, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(stock, parsedValues[entry.Value]);
                }
            }

            return stock;
        }
    }
}
